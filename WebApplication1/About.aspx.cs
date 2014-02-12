using System;
using System.Web.Security;
using System.Web.UI;
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
            Roles.Provider.RemoveUsersFromRoles(new []{"Admin"}, new []{"Admin"});
        }
    }
}