using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebStore.App_Data.Model;
using WebStore.Controls;
using WebStore.Managers;

namespace WebStore.Administration
{
    public partial class OrderAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && User.Identity.IsAuthenticated && (User.IsInRole("Admin") || User.IsInRole("Salesman")))
            {
                AdminPanel.Visible = true;
            }
        }

        protected static HtmlString GetItemsForOrder(int orderID)
        {
            var result = string.Empty;
            var positions = OrderManager.GetPositions(orderID);
            result = positions.Aggregate(result, (current, orderPosition) => current + (orderPosition.Item.Name + " x" + orderPosition.ItemQuantity.ToString("G") + "<br/>"));
            return new HtmlString(result);
        }

        public static IEnumerable<Order> GetOrders()
        {
            return OrderManager.GetOrders();
        }

        protected static string GetTotal(int orderID)
        {
            return "$" + OrderManager.GetTotal(orderID).ToString("G");
        }

        public static IQueryable<OrderState> GetAllStates()
        {
            return OrderManager.GetStates();
        }

        protected static string GetStateIDForOrder(int itemID)
        {
            return OrderManager.GetStateIDForOrderID(itemID).ToString("G");
        }

        protected void Index_Changed(object sender, EventArgs e)
        {
            var dropList = (ListWithValue)sender;
            int orderID;
            int.TryParse(dropList.Value, out orderID);
            byte stateID;
            byte.TryParse(dropList.SelectedValue, out stateID);

            OrderManager.SetState(orderID, stateID);
            OrderList.DataBind();
        }

        protected void Comment_Changed(object sender, EventArgs e)
        {
            var commentTextBox = (ValueTextBox) sender;
            int orderID;
            int.TryParse(commentTextBox.Value, out orderID);

            OrderManager.SetComment(orderID, commentTextBox.Text);
        }
    }
}