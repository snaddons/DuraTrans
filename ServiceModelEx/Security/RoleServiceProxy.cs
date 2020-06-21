// © 2009 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System.ServiceModel;

using System.ServiceModel.Channels;

namespace ServiceModelEx
{
   [ServiceContract(Name = "RoleService")]
   public interface IRoleService 
   {
      [OperationContract]
      string[] GetRolesForCurrentUser();

      [OperationContract]
      bool IsCurrentUserInRole(string role);
   }

   class RoleServiceClient : ClientBase<IRoleService>,IRoleService
   {
      public RoleServiceClient() 
      {}

      public RoleServiceClient(string endpointName) : base(endpointName) 
      {}

      public RoleServiceClient(Binding binding,EndpointAddress remoteAddress) : base(binding,remoteAddress) 
      {}
      
      public string[] GetRolesForCurrentUser() 
      {
         return Channel.GetRolesForCurrentUser();
      }

      public bool IsCurrentUserInRole(string role) 
      {
         return Channel.IsCurrentUserInRole(role);
      }
   }
}
