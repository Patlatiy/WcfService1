using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebStore.App_Data.Model;
using WebStore.Controls;
using WebStore.Managers;

namespace WebStore.Store
{
    public partial class Cart : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //Some items could be deleted from database by admin in runtime, so we'll check it out:
            if (Session["Cart"] != null)
            {
                var cart = (Dictionary<int, int>)Session["Cart"];
                var hasChanged = false;
                foreach (var key in cart.Keys.Where(key => ItemManager.GetItem(key) == null))
                {
                    cart.Remove(key);
                    hasChanged = true;
                }
                if (hasChanged)
                    Session["Cart"] = cart;
            }

            if (IsPostBack) return;

            UpdateLoggedInPanel();
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
            UpdateLoggedInPanel();
            UpdPnl.Update();
        }

        private void UpdateLoggedInPanel()
        {
            var cart = (Dictionary<int, int>)Session["Cart"];
            if (User.Identity.IsAuthenticated && cart != null && cart.Count > 0)
            {
                LoggedInPanel.Visible = true;
                CartList.DataBind();
            }
            else if (cart == null || cart.Count == 0)
            {
                LoggedInPanel.Visible = false;
            }
            else
            {
                AnonymousPanel.Visible = true;
            }
        }

        protected void RemoveButton_Click(object sender, EventArgs e)
        {
            var btn = (Button) sender;
            int itemID;
            if (btn.CommandName != "remove") return;
            if (!int.TryParse(btn.CommandArgument, out itemID)) return;
            if (RemoveItemFromCart(itemID))
            {
                UpdateUpdPnl();
            }
                
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

        protected static string ImagePath(string fileName)
        {
            return "/Images/PaymentMethods/" + fileName;
        }

        protected void CompletePurchase(object sender, EventArgs e)
        {
            var cart = (Dictionary<int, int>) Session["Cart"];
            if (cart == null || cart.Count == 0)
            {
                Response.Redirect("Cart.aspx");
                return;
            }
            if (!ItemManager.CheckCart(cart))
            {
                Response.Redirect("NoItems.aspx");
                return;
            }
            var imgBtn = (ImageButton) sender;
            byte pmID;
            if (!byte.TryParse(imgBtn.CommandName, out pmID)) return;

            Session["PaymentMethodID"] = pmID;
            var addressTextBox = ((TextBox)LoggedInPanel.FindControl("AddressTextBox"));
            if (addressTextBox == null)
            {
                Session["Address"] = "N/A";
            }
            else
            {
                Session["Address"] = addressTextBox.Text;
            }
            var commentTextBox = ((TextBox) LoggedInPanel.FindControl("CommentTextBox"));
            if (commentTextBox != null)
            {
                Session["Comment"] = commentTextBox.Text;
            }

            Response.Redirect("Complete.aspx");
        }

        private string GetLastOrderAddress()
        {
            return User.Identity.IsAuthenticated ? OrderManager.GetLastOrderAddress(User.Identity.Name) : string.Empty;
        }

        protected void CartList_DataBound(object sender, EventArgs e)
        {
            var addressTextBox = (TextBox) LoggedInPanel.FindControl("AddressTextBox");
            if (addressTextBox != null)
            {
                addressTextBox.Text = GetLastOrderAddress();
            }
        }
    }
}