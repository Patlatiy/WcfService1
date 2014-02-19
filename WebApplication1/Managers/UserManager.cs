using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
                RoleID = (Byte)UserRoles.SimpleUser,
                UserRole = webStoreContext.UserRoles.First(role => role.ID == (byte)UserRoles.SimpleUser)
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
    }
}