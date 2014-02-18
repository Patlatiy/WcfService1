using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.App_Data.Model;
using WebStore.Vasya;
using WebStore.Providers;

namespace WebStore.Managers
{
    public static class UserManager
    {
        public static void CreateUser(string login, string name, string password, string email)
        {
            var webStoreContext = DbWorkerVasya.Instance;
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

        /// <summary>
        /// Sets login for user with specified name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="shownname"></param>
        public static void SetShownName(string name, string shownname)
        {
            var dbContext = DbWorkerVasya.Instance;
            dbContext.Users.First(usr => usr.Login == name).Name = shownname;
            dbContext.SaveChanges();
        }

        public static User GetUserByLogin(string login)
        {
            return DbWorkerVasya.Instance.Users.First(usr => usr.Login == login);
        }
    }
}