using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebStore.App_Data.Model;
using WebStore.Controls;
using WebStore.Managers;
using WebStore.Vasya;

namespace WebStore.Cart
{
    public partial class Cart : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public IQueryable<Item> GetItems()
        {
            if (Session["Cart"] == null) return null;
            var cart = (Dictionary<int, int>) Session["Cart"];
            IQueryable<Item> query = ItemManager.GetItems();
            var itemList = new List<Item>();
            foreach (var query2 in cart.Keys.Select(val => query.Where(item => item.ID == val)))
            {
                itemList.AddRange(query2.ToList());
            }
            query = itemList.AsQueryable();
            return query;
        }

        protected string GetTotalItemPriceForAllItems()
        {
            return ItemManager.GetTotalItemPriceForAllItems((Dictionary<int,int>)Session["Cart"]);
        }
        
        protected string GetTotalItemPrice(int itemID)
        {
            if (Session["Cart"] == null) return "$0";
            var cart = (Dictionary<int, int>)Session["Cart"];
            var itemPrice = ItemManager.GetTotalItemPrice(itemID);
            if (cart[itemID] > 1)
            {
                return "$" + itemPrice.ToString("G") + " x " + cart[itemID].ToString("G") + " = $" +
                       (itemPrice*cart[itemID]).ToString("G");
            }
            return "$" + itemPrice.ToString("G");
        }

        private void UpdateUpdPnl()
        {
            var listView = (ListView) CartLoginView.FindControl("CartList");
            listView.DataBind();
            UpdateCartTile();
            UpdPnl.Update();
        }

        protected void RemoveButton_Click(object sender, EventArgs e)
        {
            var btn = (Button) sender;
            int itemID;
            if (btn.CommandName != "remove") return;
            if (!int.TryParse(btn.CommandArgument, out itemID)) return;
            RemoveItemFromCart(itemID);
            UpdateUpdPnl();
        }

        private bool RemoveItemFromCart(int itemID)
        {
            return Session["Cart"] != null && ((Dictionary<int, int>)Session["Cart"]).Remove(itemID);
        }

        private void UpdateCartTile()
        {
            var cartTile = ((CartTile) Master.FindControl("CartTile"));
            cartTile.UpdateShownItemCount();
        }
    }
}