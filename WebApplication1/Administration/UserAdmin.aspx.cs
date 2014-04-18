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
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                AdminPanel.Visible = true;
            }
            else
            {
                Response.Redirect("/Error/404.aspx");
            }
        }

        /// <summary>
        /// Gets list of all users
        /// </summary>
        /// <returns>IQueryable list of users</returns>
        public IQueryable<User> GetUsers()
        {
            return UserManager.GetUsers();
        }

        /// <summary>
        /// Gets all user roles
        /// </summary>
        /// <returns>IQueryable list of user roles</returns>
        public IQueryable<UserRole> GetAllRoles()
        {
            return UserManager.GetAllRoles();
        }

        /// <summary>
        /// Gets role ID for user with given login
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>string role ID</returns>
        protected string GetRoleIDForUser(string login)
        {
            return UserManager.GetUserRoleID(login).ToString("G");
        }

        /// <summary>
        /// Grants selected role to a user when it is selected
        /// </summary>
        protected void Index_Changed(Object sender, EventArgs e)
        {
            var dropList = (ListWithValue) sender;
            UserManager.GrantRole(dropList.Value, dropList.SelectedItem.Text);
        }

        /// <summary>
        /// Blocks a user
        /// </summary>
        protected void BlockUser_Clicked(Object sender, EventArgs e)
        {
            var senderBtn = (Button) sender;
            bool userBlockedState;
            bool.TryParse(senderBtn.CommandName, out userBlockedState);

            UserManager.SetUserBlock(senderBtn.CommandArgument, !userBlockedState);

            senderBtn.Text = userBlockedState ? "Block" : "Unblock";
            senderBtn.CommandName = (!userBlockedState).ToString();
        }

        /// <summary>
        /// Sets "Block" or "Unblock" button text depending on current state of user
        /// </summary>
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

        /// <summary>
        /// Hides sender ListWithValue if it is assigned to current user
        /// </summary>
        protected void IsItMe(Object sender, EventArgs e)
        {
            var senderList = (ListWithValue) sender;
            if (Page.User.Identity.IsAuthenticated && Page.User.Identity.Name == senderList.Value)
            {
                senderList.Visible = false;
            }
        }
    }
}