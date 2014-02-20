using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebStore.App_Data.Model;
using WebStore.Managers;

namespace WebStore.Administration
{
    public partial class UserAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && User.IsInRole("Admin"))
            {
                AdminPanel.Visible = true;
            }
        }

        public IQueryable<App_Data.Model.User> GetUsers()
        {
            return UserManager.GetUsers();
        }

        public IQueryable<UserRole> GetAllRoles()
        {
            return UserManager.GetAllRoles();
        }

        protected string GetRoleIDForUser(string login)
        {
            return UserManager.GetUserRoleID(login).ToString("G");
        }

        protected void Index_Changed(Object sender, EventArgs e)
        {
            var dropList = (DropDownList) sender;
            UserManager.GrantRole(Page.User.Identity.Name, dropList.SelectedItem.Text);
        }
    }
}