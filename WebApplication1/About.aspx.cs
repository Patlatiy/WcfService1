using System;
using System.Web.Security;
using System.Web.UI;
using WebStore.Managers;
using WebStore.Providers;

namespace WebStore
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string GetRoles()
        {
            var roles = Roles.Provider.GetAllRoles();

            var result = "<ul>";
            foreach (var role in roles)
            {
                result += "<li>" + role + "</li>";
            }
            result += "</ul>";

            return result;
        }

        protected void TestMethod(object sender, EventArgs e)
        {
            throw new Exception("You've broken all!");
        }

        protected void MakeMeAdmin(object sender, EventArgs e)
        {
            UserManager.GrantRole(User.Identity.Name, "Admin");
        }

        protected void MakeMeUser(object sender, EventArgs e)
        {
            UserManager.GrantRole(User.Identity.Name, "User");
        }

        protected void MakeMeSalesperson(object sender, EventArgs e)
        {
            UserManager.GrantRole(User.Identity.Name, "Salesperson");
        }

        protected string UserRole()
        {
            return UserManager.GetUserRole(User.Identity.Name);
        }
    }
}