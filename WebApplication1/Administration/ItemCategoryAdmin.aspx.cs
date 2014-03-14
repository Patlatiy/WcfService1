using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WebStore.App_Data.Model;
using WebStore.Controls;
using WebStore.Managers;

namespace WebStore.Administration
{
    public partial class ItemCategoryAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            AdminPanel.Visible = Page.User.IsInRole("Admin") || Page.User.IsInRole("Salesperson"); //TODO: Use role provider!
        }

        public static IEnumerable<ItemCategory> GetCategories()
        {
            return ItemManager.GetCategories();
        }

        protected void Name_Changed(object sender, EventArgs e)
        {
            var senderTextBox = (ValueTextBox) sender;
            int categoryID;
            if (!int.TryParse(senderTextBox.Value, out categoryID))
                return;
            var categoryName = senderTextBox.Text;
            if (string.IsNullOrEmpty(categoryName))
                return;

            ItemManager.SetCategoryName(categoryID, categoryName);
        }

        protected void Description_Changed(object sender, EventArgs e)
        {
            var senderTextBox = (ValueTextBox)sender;
            int categoryID;
            if (!int.TryParse(senderTextBox.Value, out categoryID))
                return;
            var categoryDescription = senderTextBox.Text;
            if (string.IsNullOrEmpty(categoryDescription))
                return;

            ItemManager.SetCategoryDescription(categoryID, categoryDescription);
        }

        protected void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var senderButton = (Button) sender;
            int categoryID;
            if (!int.TryParse(senderButton.CommandName, out categoryID))
                return;

            ItemManager.DeleteCategory(categoryID);
            Page.Response.Redirect(Page.Request.Url.ToString(), false);
        }
    }
}