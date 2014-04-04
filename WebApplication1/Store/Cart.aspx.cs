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

        /// <summary>
        /// Gets list items that are in cart from database
        /// </summary>
        /// <returns>List items that are in cart</returns>
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

        /// <summary>
        /// Calculates the sum for all items in cart
        /// </summary>
        /// <returns>Sum of all items in cart (string)</returns>
        protected string GetTotalItemPriceForAllItems()
        {
            return ItemManager.GetTotalItemPriceForAllItems((Dictionary<int, int>) Session["Cart"]);
        }

        /// <summary>
        /// Gets a total item cost for an item with specified ID
        /// </summary>
        /// <param name="itemID">Item ID</param>
        /// <returns>String that is showing calculations and total cost for item</returns>
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

        /// <summary>
        /// Updates cart tile and panels on the form
        /// </summary>
        private void UpdateUpdPnl()
        {
            var listView = (ListView) LoggedInPanel.FindControl("CartList");
            listView.DataBind();
            UpdateCartTile();
            UpdateLoggedInPanel();
            UpdPnl.Update();
        }

        /// <summary>
        /// Updates "logged in" panel
        /// </summary>
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

        /// <summary>
        /// RemoveButton click handler
        /// </summary>
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

        /// <summary>
        /// Removes item with specified ID from cart
        /// </summary>
        /// <param name="itemID">Item ID</param>
        /// <returns>True if successful, false if not</returns>
        private bool RemoveItemFromCart(int itemID)
        {
            return Session["Cart"] != null && ((Dictionary<int, int>) Session["Cart"]).Remove(itemID);
        }

        /// <summary>
        /// Updates cart tile
        /// </summary>
        private void UpdateCartTile()
        {
            var cartTile = ((CartTile) Master.FindControl("CartTile"));
            cartTile.UpdateShownItemCount();
        }

        /// <summary>
        /// Retrieves all payment methods from database
        /// </summary>
        /// <returns>List of payment methods</returns>
        public IQueryable<PaymentMethod> GetPaymentMethods()
        {
            return PaymentMethodManager.GetPaymentMethods();
        }

        /// <summary>
        /// Appends payment methods' folder names to image path so it can be used in img tag
        /// </summary>
        /// <param name="fileName">Stored file name for payment method</param>
        /// <returns></returns>
        protected static string ImagePath(string fileName)
        {
            return "/Images/PaymentMethods/" + fileName;
        }

        /// <summary>
        /// Completes purchase and redirects a user to Complete page
        /// </summary>
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

        /// <summary>
        /// Gets last order address for current user
        /// </summary>
        /// <returns>Last order address if it exists or empty string if it doesn't</returns>
        private string GetLastOrderAddress()
        {
            return User.Identity.IsAuthenticated ? OrderManager.GetLastOrderAddress(User.Identity.Name) : string.Empty;
        }

        /// <summary>
        /// DataBound event handler for cart list. Fills last order address text box
        /// </summary>
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