using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using WebStore.App_Data.Model;
using WebStore.Vasya;

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
            _currentItem = DbWorkerVasya.Instance.Items.FirstOrDefault(item => item.ID == itemID);
            if (_currentItem == null)
            {
                AddToCartButton.Visible = false;
                ItemCount.Visible = false;

            }
        }

        protected void AddToCartButton_OnClick(object sender, EventArgs e)
        {
            var cart = (Dictionary<int, int>)Session["Cart"] ?? new Dictionary<int, int>();
            int count = 1;
            Int32.TryParse(ItemCount.Text, out count);
            if (cart.ContainsKey(_currentItem.ID))
            {
                cart[_currentItem.ID] += count;
            }
            else
            {
                cart.Add(_currentItem.ID, count);
            }
            Session["Cart"] = cart;
            LabelAdd.Visible = true;
        }

        protected string GetImagePath()
        {
            
            return _currentItem == null ? "noimage.png" : _currentItem.Image;
        }

        protected string GetItemName()
        {
            return _currentItem == null ? "Item not found" : _currentItem.Name;
        }

        protected string GetItemDescription()
        {
            if (_currentItem == null) return "Sorry, we can't find such item in our store.";
            if (_currentItem.Description == null) return "This is a " + _currentItem.Name;
            return _currentItem.Description;
        }
        
        protected string GetItemInStore()
        {
            return _currentItem == null ? "No such item" : _currentItem.Quantity.ToString("G");
        }
    }
}