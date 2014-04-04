using System;
using System.Linq;
using System.Web.Security;
using WebStore.App_Data.Model;
using WebStore.DbWorker;

namespace WebStore.Providers
{
    /// <summary>
    /// This membership provider is a layer between ASP.NET membership mechanisms and entity framework
    /// </summary>
    public class CustomMembershipProvider : MembershipProvider
    {
        private readonly WebStoreEntities _dbContext = DbContext.Instance;
        /// <summary>
        /// Create a new user with specified parameters
        /// </summary>
        /// <param name="username">User login</param>
        /// <param name="password">Password</param>
        /// <param name="email">Email</param>
        /// <param name="passwordQuestion">(not used)</param>
        /// <param name="passwordAnswer">(not used)</param>
        /// <param name="isApproved">(not used)</param>
        /// <param name="providerUserKey">(not used)</param>
        /// <param name="status">(not used)</param>
        /// <returns>Created MembershipUser object</returns>
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

        /// <summary>
        /// Changes a password for a user with specified login
        /// </summary>
        /// <param name="username">User login</param>
        /// <param name="oldPassword">Old password</param>
        /// <param name="newPassword">New password</param>
        /// <returns>True if successful, false if not</returns>
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

        /// <summary>
        /// Updates a user with new information provided in MembershipUser object
        /// </summary>
        /// <param name="user">MembershipUser object, containing updated information</param>
        public override void UpdateUser(MembershipUser user)
        {
            var dbUser = _dbContext.Users.FirstOrDefault(usr => usr.Login == user.UserName);
            if (dbUser == null) return;

            dbUser.Name = user.UserName;
            dbUser.Login = user.Comment;
            dbUser.Password = user.GetPassword();
            dbUser.IsBlocked = user.IsLockedOut;
            dbUser.LastActiveDateTime = user.LastActivityDate;
            dbUser.RegistrationDateTime = user.CreationDate;
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Validates if a user with given credentials does exist
        /// </summary>
        /// <param name="username">User login</param>
        /// <param name="password">Password</param>
        /// <returns>True if credentials are valid, false if they aren't or user is blocked</returns>
        public override bool ValidateUser(string username, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(usr => usr.Login == username && usr.Password == password);
            return user != null && !user.IsBlocked;
        }

        /// <summary>
        /// Unlocks a user with given login
        /// </summary>
        /// <param name="userName">User login</param>
        /// <returns>True if successful, false if not</returns>
        public override bool UnlockUser(string userName)
        {
            var user = _dbContext.Users.FirstOrDefault(usr => usr.Login == userName);
            if (user == null) 
                return false;

            user.IsBlocked = false;

            _dbContext.SaveChanges();
            return true;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a MembershipUser object containing information about user with specified login
        /// </summary>
        /// <param name="username">User login</param>
        /// <param name="userIsOnline">(not used)</param>
        /// <returns>A MembershipUser object containing information about a user or null if a user doesn't exist</returns>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = DbContext.Instance.Users.FirstOrDefault(usr => usr.Login == username);
            if (user == null)
                return null;
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

        /// <summary>
        /// Gets user login for a user with specified email address
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>User login, null if user doesn't exist</returns>
        public override string GetUserNameByEmail(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(usr => usr.Email == email);
            return user == null ? null : user.Login;
        }

        /// <summary>
        /// Deletes a user with specified login from database
        /// </summary>
        /// <param name="username">User login</param>
        /// <param name="deleteAllRelatedData">(not used)</param>
        /// <returns>True if successful, false if not</returns>
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            var user = _dbContext.Users.FirstOrDefault(usr => usr.Login == username);
            if (user == null)
                return false;

            _dbContext.Users.Remove(user);

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

        /// <summary>
        /// Determines whether registration requires setting up secret question and answer
        /// </summary>
        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override string ApplicationName { get; set; }

        /// <summary>
        /// Maximum login attempts with invalid password
        /// </summary>
        public override int MaxInvalidPasswordAttempts
        {
            get { return 10; }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Determines whether registration requires a unique email from user
        /// </summary>
        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Minimum required password length
        /// </summary>
        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        /// <summary>
        /// Minimum required non-alphanumeric characters in password
        /// </summary>
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 0; }
        }

        /// <summary>
        /// Password strength RegEx
        /// </summary>
        public override string PasswordStrengthRegularExpression
        {
            get { return @"^.*(?=.{6,})(?=.*\d).*$"; }
        }
    }
}