using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WebStore.App_Data.Model;
using WebStore.Controls;
using WebStore.DbWorker;
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

        public IEnumerable<Order> GetOrders()
        {
            var filterList = (DropDownList) OrderList.FindControl("FilterList");
            var orders = filterList.SelectedIndex == 0 ? OrderManager.GetOrders() : OrderManager.GetOrdersInState(filterList.Text);
            
            return orders;
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

        protected void OrderState_Index_Changed(object sender, EventArgs e)
        {
            var dropList = (ListWithValue)sender;
            int orderID;
            int.TryParse(dropList.Value, out orderID);
            byte stateID;
            byte.TryParse(dropList.SelectedValue, out stateID);

            if (!OrderManager.SetState(orderID, stateID))
                return;

            dropList.Enabled = OrderManager.IsStateFinal(stateID);

            OrderList.DataBind();
        }

        protected void Comment_Changed(object sender, EventArgs e)
        {
            var commentTextBox = (ValueTextBox) sender;
            int orderID;
            int.TryParse(commentTextBox.Value, out orderID);

            OrderManager.SetComment(orderID, commentTextBox.Text);
        }

        protected void FillFilterList(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            var filterList = (DropDownList) sender;
            
            filterList.Items.Add("All states");
            foreach (var state in DbContext.Instance.OrderStates)
            {
                filterList.Items.Add(state.Name);
            }
        }

        protected void OnFilterChange(object sender, EventArgs e)
        {
            OrderList.DataBind();
        }
    }
}