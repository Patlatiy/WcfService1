using System;
using WebStore.Managers;

namespace WebStore.Administration
{
    public partial class NewCategoryPopup : System.Web.UI.Page
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

        protected void SubmitPage(object sender, EventArgs e)
        {
            ErrorLabel.Visible = false;

            if (ItemManager.CreateCategory(NameTextBox.Text, DescriptionTextBox.Text))
            {
                Response.Write("<script type=\"text/javascript\">window.opener.location=\"ItemCategoryAdmin.aspx\"; this.close();</script>");
            }
            else
            {
                ErrorLabel.Visible = true;
            }
        }
    }
}