// © 2008 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Security.Permissions;
using System.Security;
using System.Net;
using System.Messaging;
using System.Diagnostics;
using System.Transactions;
using System.ServiceModel.Description;
using System.Reflection;
using System.Web;

namespace ServiceModelEx
{
   public abstract class PartialTrustDuplexClientBase<T,C> : DuplexClientBase<T,C>,IDisposable where T : class
   {
      [PermissionSet(SecurityAction.Assert,Name = "FullTrust")]
      public PartialTrustDuplexClientBase(C callback) : base(callback)
      {}
      [PermissionSet(SecurityAction.Assert,Name = "FullTrust")]
      public PartialTrustDuplexClientBase(C callback,string endpointName) : base(callback,endpointName)
      {}
      [PermissionSet(SecurityAction.Assert,Name = "FullTrust")]
      public PartialTrustDuplexClientBase(C callback,Binding binding,EndpointAddress remoteAddress) : base(callback,binding,remoteAddress)
      {}

      protected object Invoke(string operation,params object[] args)
      {
         if(IsAsyncCall(operation))
         {
            DemandAsyncPermissions();
         }
         DemandSyncPermissions(operation);
         CodeAccessSecurityHelper.PermissionSetFromStandardSet(StandardPermissionSet.FullTrust).Assert();

         Type contract = typeof(T);
         MethodInfo methodInfo = contract.GetMethod(operation);
         return methodInfo.Invoke(Channel,args);
      }
      //Usefull only for clients that want full-brunt unasserted demands from WCF
      protected new T Channel
      {
         [PermissionSet(SecurityAction.Assert,Name = "FullTrust")]
         get
         {
            return base.Channel;
         }
      }
      
      [PermissionSet(SecurityAction.Assert,Name = "FullTrust")]
      new public void Close()
      {
         base.Close();
      }
      void IDisposable.Dispose()
      {
         Close();
      }
      protected virtual void DemandAsyncPermissions()
      {
         CodeAccessSecurityHelper.DemandAsyncPermissions();
      }
      protected virtual void DemandSyncPermissions(string operationName)
      {
         this.DemandClientPermissions(operationName);
      }
      bool IsAsyncCall(string operation)
      {
         if(operation.StartsWith("Begin"))
         {
            MethodInfo info = typeof(T).GetMethod(operation);
            object[] attributes = info.GetCustomAttributes(typeof(OperationContractAttribute),false);
            Debug.Assert(attributes.Length == 1);
            return (attributes[0] as OperationContractAttribute).AsyncPattern;
         }
         return false;
      }
   }
}