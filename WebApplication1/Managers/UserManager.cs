using System;
using System.Collections.Generic;
using WebStore.PocoModels;
using WebStore.Providers;

namespace WebStore.Managers
{
    public class UserManager
    {
        public enum UserRoles {Admin = 2, Salesperson = 4, User = 5}
        public static WebStore.PocoModels.User CreateUser(string login, string name, string password, string email)
        {
            var prov = new CustomMembershipProvider();
            
            return new WebStore.PocoModels.User()
            {
                Email = email,
                IsBlocked = false,
                LastActiveDateTime = null,
                Name = name,
                Orders = new List<Order>(),
                Password = password,
                RegistrationDateTime = DateTime.Now,
                RoleId = (Byte)UserRoles.User
            };
        }
    }
}