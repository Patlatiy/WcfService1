﻿using System;
using System.IO;
using System.Web.UI.WebControls;
using WebStore.Managers;

namespace WebStore.Administration
{
    public partial class NewItemPopup : System.Web.UI.Page
    {
        /// <summary>
        /// Array of item image filenames
        /// </summary>
        private string[] files;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated && (User.IsInRole("Admin") || User.IsInRole("Salesperson")))
            {
                AdminPanel.Visible = true;
            }
            else
            {
                Response.Redirect("/Error/404.aspx");
            }

            if (IsPostBack)
                return;

            files = Directory.GetFiles(MapPath(@"~\Images\Items\"), "*.png");
            for (int x = 0; x < files.Length; x++)
                files[x] = Path.GetFileName(files[x]);

            FileList.Attributes.Add("onChange", "return changepicture();");
            FileList.Attributes.Add("id", "FileList");
        }

        protected void SubmitPage(object sender, EventArgs e)
        {
            ErrorLabel.Visible = false;
            var itemName = NameTextBox.Text;
            var itemDescription = DescriptionTextBox.Text;
            var itemImage = FileList.SelectedItem.Value;
            decimal itemPrice = 0;
            decimal.TryParse(PriceTextBox.Text, out itemPrice);
            int itemQuantity = 0;
            int.TryParse(QuantityTextBox.Text, out itemQuantity);
            var itemCategory = CategoryList.Text;

            if (ItemManager.CreateItem(itemName, itemDescription, itemImage, itemPrice, itemQuantity, itemCategory))
            {
                Response.Write("<script type=\"text/javascript\">window.opener.location=\"ItemAdmin.aspx\"; this.close();</script>");
            }
            else
            {
                ErrorLabel.Visible = true;
            }
        }

        /// <summary>
        /// Populates sender DropDownList with item image filenames and selects "noimage.png"
        /// </summary>
        protected void FillDropDownWithImagePaths(object sender, EventArgs e)
        {
            var senderList = (DropDownList)sender;

            senderList.DataSource = files;
            senderList.DataBind();

            var noImageItem = senderList.Items.FindByText("noimage.png");
            if (noImageItem != null && !IsPostBack)
                noImageItem.Selected = true;
        }

        /// <summary>
        /// Fills sender DropDownList with categories
        /// </summary>
        protected void FillDropDownWithCategories(object sender, EventArgs e)
        {
            var senderDropDown = (DropDownList)sender;
            var categories = ItemManager.GetCategories();

            senderDropDown.Items.Clear();
            senderDropDown.Items.Add(string.Empty);

            foreach (var itemCategory in categories)
            {
                senderDropDown.Items.Add(itemCategory.Name);
            }
        }
    }
}