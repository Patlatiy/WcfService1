﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using WebStore.App_Data.Model;
using WebStore.Controls;
using WebStore.Managers;

namespace WebStore.Administration
{
    public partial class ItemAdmin : System.Web.UI.Page
    {
        private string[] files;

        protected void Page_Load(object sender, EventArgs e)
        {
            //ItemList.DataBind();
            
            files = Directory.GetFiles(MapPath(@"~\Images\Items\"), "*.png");
            for (int x = 0; x < files.Length; x++)
                files[x] = Path.GetFileName(files[x]);
        }

        public static IEnumerable<Item> GetItems()
        {
            return ItemManager.GetItems();
        }

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

        protected void ImagePath_Changed(object sender, EventArgs e)
        {
            var senderTextBox = (ValueTextBox)sender;
            var text = senderTextBox.Text;
            if (string.IsNullOrEmpty(text))
                return;

            int itemID;
            if (!int.TryParse(senderTextBox.Value, out itemID))
                return;

            ItemManager.SetImage(itemID, text);
            ItemList.DataBind();
        }

        protected void PopulateList(object sender, EventArgs e)
        {
            var senderList = (DropDownList) sender;

            senderList.DataSource = files;
            senderList.DataBind();
        }

        protected void CopyList(object sender, EventArgs e)
        {
            var senderList = (ListWithValue)sender;

            
            foreach (var itm in FileList.Items)
            {
                senderList.Items.Add(itm.ToString());
            }
        }

        protected void ImageIndex_Changed(object sender, EventArgs e)
        {
            var senderList = (ListWithValue) sender;
            int itemID;
            if (!int.TryParse(senderList.Value, out itemID))
                return;
            var item = ItemManager.GetItem(itemID);
            item.Image = senderList.Text;

            ItemList.DataBind();
        }

        protected string GetFileNameForItem(int itemID)
        {
            return ItemManager.GetItem(itemID).Image;
        }

        protected void Description_Changed(object sender, EventArgs e)
        {
            var senderTextBox = (ValueTextBox) sender;
            int itemID;
            if (!int.TryParse(senderTextBox.Value, out itemID))
                return;

            ItemManager.SetDescription(itemID, senderTextBox.Text);
        }

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
    }
}