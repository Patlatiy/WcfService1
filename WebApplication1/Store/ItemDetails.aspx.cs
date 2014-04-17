using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebStore.App_Data.Model;
using WebStore.Controls;
using WebStore.DbWorker;
using WebStore.Managers;

namespace WebStore.Store
{
    public partial class ItemDetails : Page
    {
        private Item _currentItem;

        protected void Page_Load(object sender, EventArgs e)
        {
            var itemID = 0;
            var itemIDString = Request.QueryString["id"];
            Int32.TryParse(itemIDString, out itemID);
            _currentItem = DbContext.Instance.Items.FirstOrDefault(item => item.ID == itemID);

            if (_currentItem == null || _currentItem.IsActive == false)
            {
                Response.Redirect("/Error/404.aspx");
                return;
            }

            if (_currentItem.Quantity <= 0)
            {
                AddToCartButton.Visible = false;
                ItemCount.Visible = false;
            }
        }

        protected void AddToCartButton_OnClick(object sender, EventArgs e)
        {
            ItemAddedPanel.Visible = false;
            ErrorPanel.Visible = false;

            if (!IsItemCountValid())
            {
                ErrorPanel.Visible = true;
                return;
            }

            var cart = (Dictionary<int, int>)Session["Cart"] ?? new Dictionary<int, int>();
            int count = 1;
            int.TryParse(ItemCount.Text, out count);

            if (cart.ContainsKey(_currentItem.ID))
            {
                cart[_currentItem.ID] += count;
            }
            else
            {
                cart.Add(_currentItem.ID, count);
            }

            Session["Cart"] = cart;

            ItemAddedPanel.Visible = true;
            var cartTile = (CartTile) Master.FindControl("CartTile");
            if (cartTile != null) cartTile.UpdateShownItemCount();
        }

        /// <summary>
        /// Gets an image path for current image
        /// </summary>
        /// <returns>String - relative image path</returns>
        protected string GetImagePath()
        {
            return _currentItem == null ? "noimage.png" : _currentItem.Image;
        }

        /// <summary>
        /// Gets name of current item
        /// </summary>
        /// <returns>String - name of current item</returns>
        protected string GetItemName()
        {
            return _currentItem == null ? "Item not found" : _currentItem.Name;
        }

        /// <summary>
        /// Gets current item description
        /// </summary>
        /// <returns>String - current item description</returns>
        protected string GetItemDescription()
        {
            if (_currentItem == null) return "Sorry, we can't find such item in our store.";
            if (_currentItem.Description == null) return "This is a " + _currentItem.Name;
            return _currentItem.Description;
        }
        
        /// <summary>
        /// Gets current item quantity
        /// </summary>
        /// <returns>String - current item quantity or "No" if its 0</returns>
        protected string GetItemInStore()
        {
            if (_currentItem == null)
                return "No such item";
            return _currentItem.Quantity <= 0 ? "No" : _currentItem.Quantity.ToString("G");
        }

        /// <summary>
        /// Checks if entered item count is not greater than we have in store
        /// </summary>
        /// <returns>True if count is valid, false if not</returns>
        protected bool IsItemCountValid()
        {
            var cart = (Dictionary<int, int>) Session["Cart"];
            var cartItemQuantity = cart == null ? 0 : cart.ContainsKey(_currentItem.ID) ? cart[_currentItem.ID] : 0;
            int itemCount = 1;
            int.TryParse(ItemCount.Text, out itemCount);

            return itemCount + cartItemQuantity <= _currentItem.Quantity;
        }

        /// <summary>
        /// Gets "backid" part of query string
        /// </summary>
        /// <returns>"backid" part of query string</returns>
        protected string GetRequestBackId()
        {
            return Request.QueryString["backid"];
        }
    }
}