using System;
using System.Collections.Generic;
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
        }

        protected void AddToCartButton_OnClick(object sender, EventArgs e)
        {
            
        }

        protected string GetImagePath()
        {
            
            return _currentItem == null ? "noimage.png" : _currentItem.Image;
        }

        protected string GetItemName()
        {
            return _currentItem == null ? "No item selected" : _currentItem.Name;
        }

        protected string GetItemDescription()
        {
            if (_currentItem == null) return "No item selected";
            if (_currentItem.Description == null) return "This is a " + _currentItem.Name;
            return _currentItem.Description;
        }
        
        protected string GetItemInStore()
        {
            return _currentItem == null ? "0" : _currentItem.Quantity.ToString("G");
        }
    }
}