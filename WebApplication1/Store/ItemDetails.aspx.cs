﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Xml.Schema;
using WebStore.App_Data.Model;
using WebStore.Controls;
using WebStore.DbWorker;

namespace WebStore.Store
{
    public partial class ItemDetails : Page
    {
        private Item _currentItem;
        private int _allowedQuantity;

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
            else
            {
                var cart = (Dictionary<int, int>)Session["Cart"];
                var cartItemQuantity = cart == null ? 0 : cart.ContainsKey(_currentItem.ID) ? cart[_currentItem.ID] : 0;
                _allowedQuantity = (_currentItem.Quantity - cartItemQuantity);
                int count;
                int.TryParse(ItemCount.Text, out count);

                if (_allowedQuantity <= count)
                {
                    AddToCartButton.Visible = false;
                    ItemCount.Visible = false;
                }
                else
                {
                    var allowedQuantityString = _allowedQuantity.ToString("G");
                    var errorMessage = "<br/>Please enter a number between 1 and " + allowedQuantityString + ".";
                    RangeValidator1.MaximumValue = allowedQuantityString;
                    RangeValidator1.ErrorMessage = errorMessage;
                    RequiredFieldValidatorForItemCount.ErrorMessage = errorMessage;
                }
                
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
            int count;
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

            if (!string.IsNullOrEmpty(_currentItem.Description)) return _currentItem.Description;

            //If item has no description, return default description:
            var firstLetter = _currentItem.Name.Substring(0, 1).ToLower();
            var prefix = "This is a ";

            if (firstLetter == "a" ||
                firstLetter == "e" ||
                firstLetter == "i" ||
                firstLetter == "o" ||
                firstLetter == "u" ||
                firstLetter == "y")
            {
                prefix = "This is an ";
            }
            return prefix + _currentItem.Name;
        }
        
        /// <summary>
        /// Gets current item quantity
        /// </summary>
        /// <returns>String - "Yes" or "No"</returns>
        protected string GetItemInStore()
        {
            if (_currentItem == null)
                return "No such item";
            int count = 0;
            if (IsPostBack)
            {
                int.TryParse(ItemCount.Text, out count);
            }
            return _allowedQuantity <= count ? "No" : "Yes"; //_currentItem.Quantity.ToString("G");
        }

        /// <summary>
        /// Checks if entered item count is not greater than number of items that we have in store
        /// </summary>
        /// <returns>True if count is valid, false if not</returns>
        protected bool IsItemCountValid()
        {
            int itemCount = 1;
            int.TryParse(ItemCount.Text, out itemCount);

            return itemCount <= _allowedQuantity;
        }

        /// <summary>
        /// Gets "backid" part of query string
        /// </summary>
        /// <returns>"backid" part of query string</returns>
        protected string GetRequestBackId()
        {
            return Request.QueryString["backid"];
        }

        /// <summary>
        /// Get "backrow" part of query string
        /// </summary>
        /// <returns>"backrow" part of query string</returns>
        protected string GetRequestBackRow()
        {
            return Request.QueryString["backrow"];
        }
    }
}