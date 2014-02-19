using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebStore.App_Data.Model;
using WebStore.Vasya;

namespace WebStore.Providers
{
    public enum UserRoles { Admin = 2, Salesperson = 4, SimpleUser = 5 }

    public class CustomRoleProvider : System.Web.Security.RoleProvider
    {
        private readonly WebStoreEntities _dbContext = DbContext.Instance;

        public override bool IsUserInRole(string username, string roleName)
        {
            var user = _dbContext.Users.First(usr => usr.Login == username);
            return user.UserRole.Name == roleName;
        }

        public override string[] GetRolesForUser(string username)
        {
            return new[] { _dbContext.Users.First(usr => usr.Login == username).UserRole.Name };
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            return _dbContext.UserRoles.Select(role => role.Name == roleName).Count() != 0;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            foreach (var username in usernames)
            {
                var user = _dbContext.Users.First(usr => usr.Login == username);
                user.UserRole = _dbContext.UserRoles.First(usrRole => usrRole.ID == (byte)UserRoles.SimpleUser);
                user.RoleID = (byte) UserRoles.SimpleUser;
                _dbContext.SaveChanges();
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

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