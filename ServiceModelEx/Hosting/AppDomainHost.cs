// © 2009 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Diagnostics;
using System.Reflection;
using System.Security;
using System.Security.Policy;
using System.Security.Permissions;
using System.Web.Security;
using System.Web;
using System.Collections.Specialized;

namespace ServiceModelEx
{
   [SecurityPermission(SecurityAction.Assert,ControlAppDomain = true)]
   [ReflectionPermission(SecurityAction.Assert,Unrestricted = true)]
   public class AppDomainHost : IDisposable
   {
      ServiceHostActivator m_ServiceHostActivator;

      public event EventHandler Closed  = delegate{};
      public event EventHandler Closing = delegate{};
      public event EventHandler Opened  = delegate{};
      public event EventHandler Opening = delegate{};

      public CommunicationState State 
      {
         get;private set;
      }

      public AppDomainHost(Type serviceType,params Uri[] baseAddresses) : this(serviceType,"AppDomain Host for " + serviceType + " " + Guid.NewGuid(),baseAddresses)
      {}

      public AppDomainHost(Type serviceType,string appDomainName,params Uri[] baseAddresses) : this(serviceType,new PermissionSet(PermissionState.Unrestricted),appDomainName,baseAddresses)
      {}

      public AppDomainHost(Type serviceType,PermissionSet permissions,params Uri[] baseAddresses) : this(serviceType,permissions,"AppDomain Host for " + serviceType + " " + Guid.NewGuid(),baseAddresses)
      {}
  
      public AppDomainHost(Type serviceType,StandardPermissionSet standardSet,string appDomainName,params Uri[] baseAddresses) : this(serviceType,CodeAccessSecurityHelper.PermissionSetFromStandardSet(standardSet),appDomainName,baseAddresses)
      {}
      public AppDomainHost(Type serviceType,string PermissionSetFileName,string appDomainName,params Uri[] baseAddresses) : this(serviceType,CodeAccessSecurityHelper.PermissionSetFromFile(PermissionSetFileName),appDomainName,baseAddresses)
      {}
      
      public AppDomainHost(Type serviceType,PermissionSet permissions,string appDomainName,params Uri[] baseAddresses) : this(serviceType,AppDomain.CreateDomain(appDomainName),permissions,baseAddresses)
      {}      
     
      protected AppDomainHost(Type serviceType,AppDomain appDomain,Uri[] baseAddresses) : this(serviceType,appDomain,new PermissionSet(PermissionState.Unrestricted),baseAddresses)
      {}       
      protected AppDomainHost(Type serviceType,AppDomain appDomain,PermissionSet permissions,Uri[] baseAddresses)
      {
         State = CommunicationState.Faulted;

         //Cannot grant service permissions the host does not have
         permissions.Demand();

         string assemblyName = Assembly.GetAssembly(typeof(ServiceHostActivator)).FullName;
         m_ServiceHostActivator = appDomain.CreateInstanceAndUnwrap(assemblyName,typeof(ServiceHostActivator).ToString()) as ServiceHostActivator;

         appDomain.SetPermissionsSet(permissions);

         m_ServiceHostActivator.CreateHost(serviceType,baseAddresses);

         State = CommunicationState.Created;
      }      
      public void Open()
      {
         if(State != CommunicationState.Created)
         {
            return;
         }
         try
         {
            Opening(this,EventArgs.Empty);

            //Permission required to read the providers application name and access config
            PermissionSet permissions = new PermissionSet(PermissionState.None);
            permissions.AddPermission(new AspNetHostingPermission(AspNetHostingPermissionLevel.Minimal));
            permissions.AddPermission(new FileIOPermission(PermissionState.Unrestricted));

            permissions.Assert();

            m_ServiceHostActivator.MembershipApplicationName = Membership.ApplicationName;
            if(Roles.Enabled)
            {
               m_ServiceHostActivator.RolesApplicationName = Roles.ApplicationName;
            }
            PermissionSet.RevertAssert();

            m_ServiceHostActivator.Open();

            State = CommunicationState.Opened;

            Opened(this,EventArgs.Empty);
         }
         catch
         {
            State = CommunicationState.Faulted;
         }
      }
      public void Close()
      {
         if(State != CommunicationState.Opened)
         {
            return;
         }
         try
         {
            Closing(this,EventArgs.Empty);

            m_ServiceHostActivator.Close();

            State = CommunicationState.Closed;

            Closed(this,EventArgs.Empty);
         }
         catch
         {
            State = CommunicationState.Faulted;
         }
      }
      public void Abort()
      {
         try
         {
            m_ServiceHostActivator.Abort();
         }
         finally
         {
            State = CommunicationState.Faulted;
         }
      }
      void IDisposable.Dispose()
      {
         Close();
      }
   }
}
