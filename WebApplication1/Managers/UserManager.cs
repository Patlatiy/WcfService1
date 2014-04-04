using System;
using System.Linq;
using WebStore.App_Data.Model;
using WebStore.DbWorker;

namespace WebStore.Managers
{
    /// <summary>
    /// User manager is used to manipulate users
    /// </summary>
    public static class UserManager
    {
        /// <summary>
        /// Sets shown name for a user with given login
        /// </summary>
        /// <param name="login">Login of a user</param>
        /// <param name="shownname">Shown name to set</param>
        /// <returns>True if successful, false if not</returns>
        public static bool SetShownName(string login, string shownname)
        {
            var user = DbContext.Instance.Users.FirstOrDefault(usr => usr.Login == login);
            if (user == null) return false;

            user.Name = shownname;
            DbContext.Instance.SaveChanges();
            return true;
        }

        /// <summary>
        /// Retrieves a user with given login
        /// </summary>
        /// <param name="login">Login of a user</param>
        /// <returns>User if he exists, null if he doesn't</returns>
        public static User GetUserByLogin(string login)
        {
            return (login == string.Empty) ? null : DbContext.Instance.Users.FirstOrDefault(usr => usr.Login == login);
        }

        /// <summary>
        /// Retrieves a user by his ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User if he exists, null if he doesn't</returns>
        public static User GetUserByID(int id)
        {
            return DbContext.Instance.Users.FirstOrDefault(usr => usr.ID == id);
        }

        /// <summary>
        /// Gets shown name for a user with specified login
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>Shown name if user exists, null if he doesn't</returns>
        public static string GetShownName(string login)
        {
            var user = DbContext.Instance.Users.FirstOrDefault(usr => usr.Login == login);
            return (login == string.Empty || user == null) ? String.Empty : user.Name;
        }

        /// <summary>
        /// Retrieves all users from the database
        /// </summary>
        /// <returns>List of all users</returns>
        public static IQueryable<User> GetUsers()
        {
            return DbContext.Instance.Users;
        }

        /// <summary>
        /// Grants a role with specified name to a user with specified login
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="roleName">Role name</param>
        public static void GrantRole(string login, string roleName)
        {
            var userRole = DbContext.Instance.UserRoles.FirstOrDefault(role => role.Name == roleName);
            var user = DbContext.Instance.Users.FirstOrDefault(usr => usr.Login == login);

            if (userRole == null || user == null) return;

            user.UserRole = userRole;
            user.RoleID = userRole.ID;
            DbContext.Instance.SaveChanges();
        }

        /// <summary>
        /// Gets a role name for a user with specified login
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>Role name if user exists, empty if he doesn't</returns>
        public static string GetUserRole(string login)
        {
            var user = DbContext.Instance.Users.FirstOrDefault(usr => usr.Login == login);
            return user == null ? string.Empty : user.UserRole.Name;
        }

        /// <summary>
        /// Gets user role ID for a user with specified login
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>User role ID if user exists, (byte) 0 if it doesn't</returns>
        public static byte GetUserRoleID(string login)
        {
            var user = DbContext.Instance.Users.FirstOrDefault(usr => usr.Login == login);
            return user == null ? (byte) 0 : user.UserRole.ID;
        }

        /// <summary>
        /// Retrieves all user roles from database
        /// </summary>
        /// <returns>List of all user roles</returns>
        public static IQueryable<UserRole> GetAllRoles()
        {
            return DbContext.Instance.UserRoles;
        }

        /// <summary>
        ///  Blocks or unblocks user with given login
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="blocked">True = block, false = unblock</param>
        /// <returns>True if successful, false if not</returns>
        public static bool SetUserBlock(string login, bool blocked)
        {
            var user = GetUserByLogin(login);
            if (user == null) return false;

            user.IsBlocked = blocked;
            DbContext.Instance.SaveChanges();
            return true;
        }

        /// <summary>
        /// Verifies if user with specified email address is already registered
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>True if user exists, false if not</returns>
        public static bool EmailExists(string email)
        {
            var user = DbContext.Instance.Users.FirstOrDefault(usr => usr.Email == email);
            return user != null;
        }

        /// <summary>
        /// Verifies if user with specified login is already registered
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>True if user exists, false if not</returns>
        public static bool LoginExists(string login)
        {
            var user = DbContext.Instance.Users.FirstOrDefault(usr => usr.Login == login);
            return user != null;
        }
    }
}