// © 2009 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web.Security;
using System;

namespace ServiceModelEx
{
   class ClientMembershipProvider : MembershipProvider
   {
      public override string ApplicationName
      {
         get
         {
            throw new NotSupportedException();
         }
         set
         {
            throw new NotSupportedException();
         }
      }
      public override bool EnablePasswordReset
      {
         get
         {
            throw new NotSupportedException();
         }
      }
      public override bool EnablePasswordRetrieval
      {
         get
         {
            throw new NotSupportedException();
         }
      }
      public override int MaxInvalidPasswordAttempts
      {
         get
         {
            throw new NotSupportedException();
         }
      }
      public override int MinRequiredNonAlphanumericCharacters
      {
         get
         {
            throw new NotSupportedException();
         }
      }
      public override int MinRequiredPasswordLength
      {
         get
         {
            throw new NotSupportedException();
         }
      }
      public override int PasswordAttemptWindow
      {
         get
         {
            throw new NotSupportedException();
         }
      }
      public override MembershipPasswordFormat PasswordFormat
      {
         get
         {
            throw new NotSupportedException();
         }
      }
      public override string PasswordStrengthRegularExpression
      {
         get
         {
            throw new NotSupportedException();
         }
      }
      public override bool RequiresQuestionAndAnswer
      {
         get
         {
            throw new NotSupportedException();
         }
      }
      public override bool RequiresUniqueEmail
      {
         get
         {
            throw new NotSupportedException();
         }
      }
      public override bool ChangePassword(string username,string oldPassword,string newPassword)
      {
         throw new NotSupportedException();
      }

      public override bool ChangePasswordQuestionAndAnswer(string username,string password,string newPasswordQuestion,string newPasswordAnswer)
      {
         throw new NotSupportedException();
      }
      public override MembershipUser CreateUser(string username,string password,string email,string passwordQuestion,string passwordAnswer,bool isApproved,object providerUserKey,out MembershipCreateStatus status)
      {
         throw new NotSupportedException();
      }

      public override bool DeleteUser(string username,bool deleteAllRelatedData)
      {
         throw new NotSupportedException();
      }

      public override MembershipUserCollection FindUsersByEmail(string emailToMatch,int pageIndex,int pageSize,out int totalRecords)
      {
         throw new NotSupportedException();
      }
      public override MembershipUserCollection FindUsersByName(string usernameToMatch,int pageIndex,int pageSize,out int totalRecords)
      {
         throw new NotSupportedException();
      }
      public override MembershipUserCollection GetAllUsers(int pageIndex,int pageSize,out int totalRecords)
      {
         throw new NotSupportedException();
      }
      public override int GetNumberOfUsersOnline()
      {
         throw new NotSupportedException();
      }
      public override string GetPassword(string username,string answer)
      {
         throw new NotSupportedException();
      }      
      public override MembershipUser GetUser(object providerUserKey,bool userIsOnline)
      {
         throw new NotSupportedException();
      }
      public override MembershipUser GetUser(string username,bool userIsOnline)
      {
         throw new NotSupportedException();
      }
      public override string GetUserNameByEmail(string email)
      {
         throw new NotSupportedException();
      }
      public override string ResetPassword(string username,string answer)
      {
         throw new NotSupportedException();
      }
      public override bool UnlockUser(string userName)
      {
         throw new NotSupportedException();
      }
      public override void UpdateUser(MembershipUser user)
      {
         throw new NotSupportedException();
      }
      public override bool ValidateUser(string username,string password)
      {
         throw new NotSupportedException();
      }
   }
}
