using System;
using System.Linq;
using WebStore.App_Data.Model;
using WebStore.DbWorker;

namespace WebStore.Managers
{
    public static class UserManager
    {
        public static void SetShownName(string name, string shownname)
        {
            var dbContext = DbContext.Instance;
            dbContext.Users.First(usr => usr.Login == name).Name = shownname;
            dbContext.SaveChanges();
        }

        public static User GetUserByLogin(string login)
        {
            return (login == string.Empty) ? null : DbContext.Instance.Users.First(usr => usr.Login == login);
        }

        public static User GetUserByID(int id)
        {
            return DbContext.Instance.Users.First(usr => usr.ID == id);
        }

        public static string GetShownName(string login)
        {
            return (login == string.Empty) ? String.Empty : DbContext.Instance.Users.First(usr => usr.Login == login).Name; 
        }

        public static IQueryable<User> GetUsers()
        {
            return DbContext.Instance.Users;
        }

        public static void GrantRole(string login, string roleName)
        {
            var userRole = DbContext.Instance.UserRoles.First(role => role.Name == roleName);
            var user = DbContext.Instance.Users.First(usr => usr.Login == login);

            if (userRole == null || user == null) return;

            user.UserRole = userRole;
            user.RoleID = userRole.ID;
            DbContext.Instance.SaveChanges();
        }

        public static string GetUserRole(string login)
        {
            var user = DbContext.Instance.Users.First(usr => usr.Login == login);
            return user == null ? string.Empty : user.UserRole.Name;
        }

        public static byte GetUserRoleID(string login)
        {
            var user = DbContext.Instance.Users.First(usr => usr.Login == login);
            return user == null ? (byte) 0 : user.UserRole.ID;
        }

        public static IQueryable<UserRole> GetAllRoles()
        {
            return DbContext.Instance.UserRoles;
        }

        public static bool SetUserBlock(string login, bool blocked)
        {
            var user = UserManager.GetUserByLogin(login);
            if (user == null) return false;

            user.IsBlocked = blocked;
            DbContext.Instance.SaveChanges();
            return true;
        }

        public static bool EmailExists(string email)
        {
            var user = DbContext.Instance.Users.FirstOrDefault(usr => usr.Email == email);
            return user != null;
        }

        public static bool LoginExists(string login)
        {
            var user = DbContext.Instance.Users.FirstOrDefault(usr => usr.Login == login);
            return user != null;
        }
    }
}