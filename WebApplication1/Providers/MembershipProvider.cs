using System;
using System.Linq;
using System.Web.Security;
using WebStore.App_Data.Model;
using WebStore.Vasya;

namespace WebStore.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
            bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            var newUser = new User()
            {
                Email = email,
                IsBlocked = false,
                LastActiveDateTime = DateTime.Now,
                RegistrationDateTime = DateTime.Now,
                Password = password,
                Login = username,
                Name = string.Empty,
                RoleID = (byte)UserRoles.Admin
            };

            var webStoreContext = DbWorkerVasya.Instance;
            webStoreContext.Users.Add(newUser);
            try
            {
                webStoreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.InnerException.Message);
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
            //throw new NotImplementedException();
            var webStoreContext = DbWorkerVasya.Instance;
            var user = webStoreContext.Users.First(usr => usr.Login == username);
            if (user.Password == oldPassword)
            {
                user.Password = newPassword;
                return true;
            }
            else
            {
                return false;
            }
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
            get { return 10; }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
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