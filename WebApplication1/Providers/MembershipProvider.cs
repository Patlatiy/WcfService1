using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using WebStore.App_Data.Model;
using WebStore.SingletonSample;

namespace WebStore.Providers
{
    public class CustomMembershipProvider : System.Web.Security.MembershipProvider
    {

        public void CreateUser(PocoModels.User user)
        {
            var newUser = new User()
            {
                Email = user.Email,
                IsBlocked = user.IsBlocked,
                LastActiveDateTime = null,
                RegistrationDateTime = user.RegistrationDateTime,
                Login = user.Login,
                Password = user.Password,
                RoleID = user.RoleId
            };

            var webStoreContext = WebStoreEntitiesContextSingleton.Instance;
            webStoreContext.Users.Add(newUser);
            try
            {
                webStoreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Shit happens");
            }
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
            bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            var newUser = new User()
            {
                Email = email,
                IsBlocked = false,
                LastActiveDateTime = DateTime.Now,
                RegistrationDateTime = DateTime.Now,
                Name = username,
                Login = username,
                Password = password,
                RoleID = (byte)UserRoles.Admin
            };

            var webStoreContext = WebStoreEntitiesContextSingleton.Instance;
            webStoreContext.Users.Add(newUser);
            try
            {
                webStoreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Shit happens");
            }
            
            status = MembershipCreateStatus.Success;
            return new MembershipUser(
                providerName: "CustomMembershipProvider",
                name: newUser.Name,
                providerUserKey: newUser.Email,
                email: newUser.Email,
                passwordQuestion: string.Empty,
                comment: string.Empty,
                isApproved: true,
                isLockedOut: newUser.IsBlocked,
                creationDate: newUser.RegistrationDateTime,
                lastLoginDate: DateTime.Now,
                lastActivityDate: DateTime.Now,
                lastPasswordChangedDate: DateTime.Now,
                lastLockoutDate: DateTime.Now
                );
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
            }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
            //var webStoreContext = WebStoreEntitiesContextSingleton.Instance;
            
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}