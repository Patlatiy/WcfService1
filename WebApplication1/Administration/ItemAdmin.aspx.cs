using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebStore.App_Data.Model;
using WebStore.Controls;
using WebStore.Managers;

namespace WebStore.Administration
{
    public partial class ItemAdmin : System.Web.UI.Page
    {
        /// <summary>
        /// Array containing file names of item images
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
            files = Directory.GetFiles(MapPath(@"~\Images\Items\"), "*.png");
            for (int x = 0; x < files.Length; x++)
                files[x] = Path.GetFileName(files[x]);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Pager.SetPageProperties(0, 5, true);
            Pager.DataBind();
            ItemList.DataBind();
        }

        /// <summary>
        /// Gets items that fit selected category or all items if the category is not selected
        /// </summary>
        /// <returns>Enumerable list of items</returns>
        public IQueryable<Item> GetItems()
        {
            IQueryable<Item> items = FilterList.SelectedIndex == 0
                ? ItemManager.GetItems()
                : ItemManager.GetItemsInCategory(FilterList.Text);

            return items;
        }

        /// <summary>
        /// Runs when name ValueTextBox changes
        /// </summary>
        protected void Name_Changed(object sender, EventArgs e)
        {
            var senderTextBox = (ValueTextBox)sender;
            var text = senderTextBox.Text;
            if (string.IsNullOrEmpty(text))
                return;

            int itemID;
            if (!int.TryParse(senderTextBox.Value, out itemID))
                return;

            ItemManager.SetName(itemID, text);
        }

        /// <summary>
        /// Populates sender DropDownList with filenames of item images
        /// </summary>
        protected void PopulateList(object sender, EventArgs e)
        {
            var senderList = (DropDownList) sender;

            senderList.DataSource = files;
            senderList.DataBind();
        }

        /// <summary>
        /// Copies all items from FileList to sender ListWithValue
        /// Probably should rethink this
        /// </summary>
        protected void CopyList(object sender, EventArgs e)
        {
            var senderList = (ListWithValue)sender;
            senderList.Items.Clear();

            foreach (var itm in FileList.Items)
            {
                senderList.Items.Add(itm.ToString());
            }

            int itemID;
            int.TryParse(senderList.Value, out itemID);
            var item = ItemManager.GetItem(itemID);
            var selectedItem = senderList.Items.FindByText(item.Image);
            selectedItem.Selected = true;
        }

        /// <summary>
        /// Changes item image in the database according to selected item in ListWithValue sender
        /// </summary>
        protected void ImageIndex_Changed(object sender, EventArgs e)
        {
            var senderList = (ListWithValue) sender;
            int itemID;
            if (!int.TryParse(senderList.Value, out itemID))
                return;
            ItemManager.SetImage(itemID, senderList.Text);
        }

        /// <summary>
        /// Gets image filename for an item with given itemID
        /// </summary>
        /// <param name="itemID">ID of the item</param>
        /// <returns>string, item image</returns>
        protected string GetFileNameForItem(int itemID)
        {
            return ItemManager.GetImage(itemID);
        }

        /// <summary>
        /// Sets a description for item when "description" ValueTextBox changes
        /// </summary>
        protected void Description_Changed(object sender, EventArgs e)
        {
            var senderTextBox = (ValueTextBox) sender;
            int itemID;
            if (!int.TryParse(senderTextBox.Value, out itemID))
                return;

            ItemManager.SetDescription(itemID, senderTextBox.Text);
        }

        /// <summary>
        /// Sets an item quantity when "quantity" ValueTextBox changes
        /// </summary>
        protected void Quantity_Changed(object sender, EventArgs e)
        {
            var senderTextBox = (ValueTextBox) sender;
            int itemID;
            if (!int.TryParse(senderTextBox.Value, out itemID))
                return;
            int itemQuant;
            if (!int.TryParse(senderTextBox.Text, out itemQuant))
                return;

            ItemManager.SetQuantity(itemID, itemQuant);
        }

        /// <summary>
        /// Sets an item price when "price" ValueTextBox changes
        /// </summary>
        protected void Price_Changed(object sender, EventArgs e)
        {
            var senderTextBox = (ValueTextBox) sender;
            int itemID;
            if (!int.TryParse(senderTextBox.Value, out itemID))
                return;
            decimal itemPrice;
            if (!decimal.TryParse(senderTextBox.Text, out itemPrice))
                return;
            itemPrice = decimal.Round(itemPrice, 2);
            senderTextBox.Text = itemPrice.ToString("F");

            ItemManager.SetPrice(itemID, itemPrice);
        }

        /// <summary>
        /// Shows "changes saved" labels
        /// </summary>
        protected void SaveChanges(object sender, EventArgs e)
        {
            ChangesSavedLabel1.Visible = true;
        }

        /// <summary>
        /// Sets an item category when "Category" ListWithValue changes
        /// </summary>
        protected void Category_Changed(object sender, EventArgs e)
        {
            var senderList = (ListWithValue) sender;
            int itemID;
            if (!int.TryParse(senderList.Value, out itemID))
                return;
            var categoryName = senderList.Text;
            if (string.IsNullOrEmpty(categoryName))
                categoryName = null;

            ItemManager.SetCategory(itemID, categoryName);
        }

        /// <summary>
        /// Fills sender ListWithValue with all existing categories and sets selected one depending on sender value which is taken as itemID
        /// </summary>
        protected void FillDropDownWithCategories(object sender, EventArgs e)
        {
            var senderDropDown = (ListWithValue) sender;
            int itemID;
            int.TryParse(senderDropDown.Value, out itemID);
            senderDropDown.Items.Clear();
            senderDropDown.Items.Add(string.Empty);
            var categories = ItemManager.GetCategories();

            foreach (var itemCategory in categories)
            {
                senderDropDown.Items.Add(itemCategory.Name);
            }
            senderDropDown.SelectedValue = ItemManager.GetItemCategoryName(itemID);
        }

        /// <summary>
        /// Deletes item
        /// </summary>
        protected void DeleteItem(object sender, EventArgs e)
        {
            var senderButton = (Button) sender;
            int itemID;
            if (!int.TryParse(senderButton.CommandName, out itemID))
                return;

            ItemManager.DeleteItem(itemID);
        }

        /// <summary>
        /// Fills sender filter list with all existing categories plus "All categories" item
        /// </summary>
        protected void FillFilterList(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            var filterList = (DropDownList)sender;

            filterList.Items.Add("All categories");
            foreach (var cat in ItemManager.GetCategories())
            {
                filterList.Items.Add(cat.Name);
            }
        }
    }
}
