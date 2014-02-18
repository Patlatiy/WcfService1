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
            if (IsPostBack) return;
            if (User.Identity.IsAuthenticated)
            {
                LoggedInPanel.Visible = true;
                CartList.DataBind();
                //((TextBox) CartList.nam.FindControl("AddressTextBox")).Text = GetLastOrderAddress();
            }
            else
            {
                AnonymousPanel.Visible = true;
            }
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
            return ItemManager.GetTotalItemPriceForAllItems((Dictionary<int, int>) Session["Cart"]);
        }

        protected string GetTotalItemPrice(int itemID)
        {
            if (Session["Cart"] == null) return "$0";
            var cart = (Dictionary<int, int>) Session["Cart"];
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
            var listView = (ListView) LoggedInPanel.FindControl("CartList");
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
            return Session["Cart"] != null && ((Dictionary<int, int>) Session["Cart"]).Remove(itemID);
        }

        private void UpdateCartTile()
        {
            var cartTile = ((CartTile) Master.FindControl("CartTile"));
            cartTile.UpdateShownItemCount();
        }

        public IQueryable<PaymentMethod> GetPaymentMethods()
        {
            return PaymentMethodManager.GetPaymentMethods();
        }

        protected string ImagePath(string fileName)
        {
            return "/Images/PaymentMethods/" + fileName;
        }

        protected void CompletePurchase(object sender, EventArgs e)
        {
            var imgBtn = (ImageButton) sender;
            byte pmID;
            if (byte.TryParse(imgBtn.CommandName, out pmID))
            {
                Session["PaymentMethodID"] = pmID;
                var addressTextBox = ((TextBox)CartList.FindControl("AddressTextBox"));
                if (addressTextBox == null)
                {
                    Session["Address"] = "N/A";
                }
                else
                {
                    Session["Address"] = addressTextBox.Text;
                }
                Response.Redirect("Complete.aspx");
            }
        }

        protected string GetLastOrderAddress()
        {
            return User.Identity.IsAuthenticated ? OrderManager.GetLastOrderAddress(User.Identity.Name) : string.Empty;
        }

        protected void CartList_DataBound(object sender, EventArgs e)
        {
            var addressTextBox = (TextBox) CartList.FindControl("AddressTextBox");
            if (addressTextBox != null)
            {
                addressTextBox.Text = GetLastOrderAddress();
            }
        }
    }
}