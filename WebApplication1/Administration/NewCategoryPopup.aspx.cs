using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebStore.Managers;

namespace WebStore.Administration
{
    public partial class NewCategoryPopup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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