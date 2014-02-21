using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using WebStore.App_Data.Model;
using WebStore.Vasya;
using WebStore.Providers;

namespace WebStore.Managers
{
    public static class UserManager
    {
        public static void CreateUser(string login, string name, string password, string email)
        {
            var webStoreContext = DbContext.Instance;
            var newUser = new User
            {
                Login = login,
                Email = email,
                IsBlocked = false,
                LastActiveDateTime = null,
                Name = name,
                Orders = new List<Order>(),
                Password = password,
                RegistrationDateTime = DateTime.Now,
                RoleID = (Byte)UserRoles.User,
                UserRole = webStoreContext.UserRoles.First(role => role.ID == (byte)UserRoles.User)
            };

            webStoreContext.Users.Add(newUser);
            webStoreContext.SaveChanges();
        }

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

        public static string GetShownName(string login)
        {
            return (login == string.Empty) ? String.Empty : DbContext.Instance.Users.First(usr => usr.Login == login).Name; 
        }

        public static IQueryable<User> GetUsers()
        {
            return DbContext.Instance.Users;
        }

        public static void GrantRole(string login, string rolename)
        {
            var userRole = DbContext.Instance.UserRoles.First(role => role.Name == rolename);
            var user = DbContext.Instance.Users.First(usr => usr.Login == login);

            if (userRole == null || user == null) return;

            user.UserRole = userRole;
            user.RoleID = userRole.ID;
            DbContext.Instance.SaveChanges();
        }

        public static string GetUserRole(string login)
        {
            var user = DbContext.Instance.Users.First(usr => usr.Login == login);
            if (user == null) return string.Empty;
            return user.UserRole.Name;
        }

        public static byte GetUserRoleID(string login)
        {
            var user = DbContext.Instance.Users.First(usr => usr.Login == login);
            if (user == null) return 0;
            return user.UserRole.ID;
        }

        public static IQueryable<UserRole> GetAllRoles()
        {
            return DbContext.Instance.UserRoles;
        }

        public static bool SetUserBlock(string login, bool blocked)
        {
            var user = DbContext.Instance.Users.First(usr => usr.Login == login);
            if (user == null) return false;
            user.IsBlocked = blocked;
            DbContext.Instance.SaveChanges();
            return true;
        }
    }
}