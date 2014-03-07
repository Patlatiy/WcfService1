using System;
using System.Linq;
using System.Web.UI.WebControls;
using WebStore.App_Data.Model;
using WebStore.Controls;
using WebStore.Managers;

namespace WebStore.Administration
{
    public partial class UserAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && User.Identity.IsAuthenticated && User.IsInRole("Admin"))
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
            var dropList = (ListWithValue) sender;
            UserManager.GrantRole(dropList.Value, dropList.SelectedItem.Text);
        }

        protected void BlockUser_Clicked(Object sender, EventArgs e)
        {
            var senderBtn = (Button) sender;
            bool userBlockedState;
            bool.TryParse(senderBtn.CommandName, out userBlockedState);
            UserManager.SetUserBlock(senderBtn.CommandArgument, !userBlockedState);
            senderBtn.Text = userBlockedState ? "Block" : "Unblock";
            senderBtn.CommandName = (!userBlockedState).ToString();
        }

        protected void CorrectButtonName(Object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var senderBtn = (Button)sender;
            switch (senderBtn.Text)
            {
                case "True":
                    senderBtn.Text = "Unblock";
                    break;
                case "False":
                    senderBtn.Text = "Block";
                    break;
            }
            if (Page.User.Identity.IsAuthenticated && Page.User.Identity.Name == senderBtn.CommandArgument)
            {
                senderBtn.Visible = false;
            }
        }
    }
}