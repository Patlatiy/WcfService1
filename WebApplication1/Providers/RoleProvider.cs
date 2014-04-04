using System;
using System.Linq;
using WebStore.App_Data.Model;
using WebStore.DbWorker;

namespace WebStore.Providers
{
    /// <summary>
    /// Temporary enum specifying which IDs roles does have in database
    /// </summary>
    public enum UserRoles { Admin = 2, Salesperson = 4, User = 5 }

    /// <summary>
    /// This role provider is a layer between ASP.NET role mechanisms and entity framework
    /// </summary>
    public class CustomRoleProvider : System.Web.Security.RoleProvider
    {
        private readonly WebStoreEntities _dbContext = DbContext.Instance;

        /// <summary>
        /// Verifies if user with specified login is in a role with specified name
        /// </summary>
        /// <param name="username">User login</param>
        /// <param name="roleName">Role name</param>
        /// <returns>True if user is in role, false if user doesn't exist or isn't in role</returns>
        public override bool IsUserInRole(string username, string roleName)
        {
            var user = _dbContext.Users.FirstOrDefault(usr => usr.Login == username);
            return (user != null) && user.UserRole.Name == roleName;
        }

        /// <summary>
        /// Get roles for user with specified login
        /// </summary>
        /// <param name="username">User login</param>
        /// <returns>Array of role names for user</returns>
        public override string[] GetRolesForUser(string username)
        {
            var user = _dbContext.Users.FirstOrDefault(usr => usr.Login == username);
            return user == null ? null : new[] { user.UserRole.Name };
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if role exists
        /// </summary>
        /// <param name="roleName">Role name</param>
        /// <returns>True if role exists, false if it doesn't</returns>
        public override bool RoleExists(string roleName)
        {
            return _dbContext.UserRoles.Select(role => role.Name == roleName).Count() != 0;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resets roles for users with specified logins
        /// </summary>
        /// <param name="usernames">User logins</param>
        /// <param name="roleNames">(not used)</param>
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            var userRole = _dbContext.UserRoles.FirstOrDefault(usrRole => usrRole.ID == (byte)UserRoles.User);
            foreach (var username in usernames)
            {
                var user = _dbContext.Users.FirstOrDefault(usr => usr.Login == username);
                if (user == null)
                    continue;

                user.UserRole = (userRole == null) ? null : user.UserRole = userRole;
                
                user.RoleID = (byte) UserRoles.User;
                _dbContext.SaveChanges();
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves all roles form the database
        /// </summary>
        /// <returns>Array of role names</returns>
        public override string[] GetAllRoles()
        {
            return _dbContext.UserRoles.Select(role => role.Name).ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}