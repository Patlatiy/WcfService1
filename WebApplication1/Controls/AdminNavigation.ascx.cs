using System;
using System.Web.UI;

namespace WebStore.Controls
{
    public partial class AdminNavigation : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string UserAdminVisibility()
        {
            return Page.User.IsInRole("Admin") ? string.Empty : "none";
        }

        protected string OrderAdminVisibility()
        {
            return Page.User.IsInRole("Salesperson") || Page.User.IsInRole("Admin") ? string.Empty : "none";
        }

        protected string ItemAdminVisibility()
        {
            return Page.User.IsInRole("Salesperson") || Page.User.IsInRole("Admin") ? string.Empty : "none";
        }

        protected string CategoryAdminVisibility()
        {
            return Page.User.IsInRole("Salesperson") || Page.User.IsInRole("Admin") ? string.Empty : "none";
        }
    }
}
