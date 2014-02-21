using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebStore.App_Data.Model;
using WebStore.Managers;

namespace WebStore.Administration
{
    public partial class OrderAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && User.IsInRole("Admin"))
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
            var dropList = (DropDownList)sender;
            
        }
    }
}