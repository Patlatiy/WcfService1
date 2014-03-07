using System;
using System.Linq;
using System.Web.Security;
using WebStore.App_Data.Model;
using WebStore.DbWorker;

namespace WebStore.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        private readonly WebStoreEntities _dbContext = DbContext.Instance;

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
            bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            var newUser = DbContext.Instance.Users.Create();
            newUser.Email = email;
            newUser.IsBlocked = false;
            newUser.LastActiveDateTime = DateTime.Now;
            newUser.RegistrationDateTime = DateTime.Now;
            newUser.Password = password;
            newUser.Login = username;
            newUser.Name = string.Empty;
            newUser.UserRole = DbContext.Instance.UserRoles.First(role => role.Name == "User");
            newUser.RoleID = newUser.UserRole.ID;

            var webStoreContext = DbContext.Instance;

            webStoreContext.Users.Add(newUser);
            webStoreContext.SaveChanges();

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
            var user = _dbContext.Users.First(usr => usr.Login == username);
            if (user.Password != oldPassword) return false;
            user.Password = newPassword;
            _dbContext.SaveChanges();
            return true;
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            var dbUser = _dbContext.Users.First(usr => usr.Login == user.UserName);
            dbUser.Name = user.UserName;
            dbUser.Login = user.Comment;
            dbUser.Password = user.GetPassword();
            dbUser.IsBlocked = user.IsLockedOut;
            dbUser.LastActiveDateTime = user.LastActivityDate;
            dbUser.RegistrationDateTime = user.CreationDate;
            _dbContext.SaveChanges();
        }

        public override bool ValidateUser(string username, string password)
        {
            var user = DbContext.Instance.Users.FirstOrDefault(usr => usr.Login == username && usr.Password == password);
            return user != null && !user.IsBlocked;
        }

        public override bool UnlockUser(string userName)
        {
            _dbContext.Users.First(usr => usr.Login == userName).IsBlocked = false;
            _dbContext.SaveChanges();
            return true;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = DbContext.Instance.Users.First(usr => usr.Login == username);
            return new MembershipUser(
                providerName: "CustomMembershipProvider",
                name: user.Login,
                providerUserKey: user.Login,
                email: user.Email,
                passwordQuestion: string.Empty,
                comment: user.Name,
                isApproved: true,
                isLockedOut: user.IsBlocked,
                creationDate: user.RegistrationDateTime,
                lastLoginDate: user.LastActiveDateTime == null ? DateTime.MinValue : (DateTime)user.LastActiveDateTime,
                lastActivityDate: user.LastActiveDateTime == null ? DateTime.MinValue : (DateTime)user.LastActiveDateTime,
                lastPasswordChangedDate: DateTime.MinValue,
                lastLockoutDate: DateTime.MinValue);
        }

        public override string GetUserNameByEmail(string email)
        {
            return _dbContext.Users.First(usr => usr.Email == email).Login;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            _dbContext.Users.Remove(_dbContext.Users.First(usr => usr.Login == username));
            _dbContext.SaveChanges();
            return true;
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
            get { return 0; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return @"^.*(?=.{6,})(?=.*\d).*$"; }
        }
    }
}