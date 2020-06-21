// © 2009 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System.ServiceModel;
using System.ServiceModel.Channels;

namespace ServiceModelEx
{
   [ServiceContract(Name = "AuthenticationService")]
   interface IAuthenticationService 
   {  
      [OperationContract]
      bool ValidateUser(string username,string password,string customCredential);

      [OperationContract]
      bool Login(string username,string password,string customCredential,bool isPersistent);

      [OperationContract]
      bool IsLoggedIn();

      [OperationContract]
      void Logout();
   }
   class AuthenticationServiceClient : ClientBase<IAuthenticationService>,IAuthenticationService
   {
      public AuthenticationServiceClient() 
      {}

      public AuthenticationServiceClient(string endpointName) : base(endpointName) 
      {}

      public AuthenticationServiceClient(Binding binding,EndpointAddress remoteAddress) : base(binding,remoteAddress) 
      {}

      public bool ValidateUser(string username,string password,string customCredential) 
      {
         return Channel.ValidateUser(username,password,customCredential);
      }

      public bool Login(string username,string password,string customCredential,bool isPersistent) 
      {
         return Channel.Login(username,password,customCredential,isPersistent);
      }

      public bool IsLoggedIn() 
      {
         return Channel.IsLoggedIn();
      }

      public void Logout() 
      {
         Channel.Logout();
      }
   }
}
