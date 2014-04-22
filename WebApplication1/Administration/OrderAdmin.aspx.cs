using System;
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
            if (User.Identity.IsAuthenticated && (User.IsInRole("Admin") || User.IsInRole("Salesperson")))
            {
                AdminPanel.Visible = true;
            }
            else
            {
                Response.Redirect("/Error/404.aspx");
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            OrderList.DataBind();
        }

        /// <summary>
        /// Gets item for order with given order ID
        /// </summary>
        /// <param name="orderID">Order ID</param>
        /// <returns>HTML encoded string with all items for given order</returns>
        protected static HtmlString GetItemsForOrder(int orderID)
        {
            var result = string.Empty;
            var positions = OrderManager.GetPositions(orderID);
            result = positions.Aggregate(result, (current, orderPosition) => 
                current + (orderPosition.Item.Name + " x" + orderPosition.ItemQuantity.ToString("G") + "<br/>"));
            return new HtmlString(result);
        }

        /// <summary>
        /// Gets all orders from database or all orders in state set by FilterList
        /// </summary>
        /// <returns>IEnumerable list of orders</returns>
        public IQueryable<Order> GetOrders()
        {
            var filterList = (DropDownList) OrderList.FindControl("FilterList");
            IQueryable<Order> orders = filterList.SelectedIndex == 0 ? OrderManager.GetOrders() : OrderManager.GetOrdersInState(filterList.Text);
            
            return orders;
        }

        /// <summary>
        /// Calculates total cost for order with given ID
        /// </summary>
        /// <param name="orderID">Order ID</param>
        /// <returns></returns>
        protected static string GetTotal(int orderID)
        {
            return "$" + OrderManager.GetTotal(orderID).ToString("G");
        }

        /// <summary>
        /// Gets all possible order states
        /// </summary>
        /// <returns>IQueryable list of OrderState</returns>
        public static IQueryable<OrderState> GetAllStates()
        {
            return OrderManager.GetStates();
        }

        /// <summary>
        /// Gets state ID for order with given ID
        /// </summary>
        /// <param name="orderID">Order ID</param>
        /// <returns>string state id</returns>
        protected static string GetStateIDForOrder(int orderID)
        {
            return OrderManager.GetStateIDForOrderID(orderID).ToString("G");
        }

        /// <summary>
        /// Sets an order state when sender ListWithValue selected index changes
        /// </summary>
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
        }

        /// <summary>
        /// Sets an order comment when sender ValueTextBox changes
        /// </summary>
        protected void Comment_Changed(object sender, EventArgs e)
        {
            var commentTextBox = (ValueTextBox) sender;
            int orderID;
            int.TryParse(commentTextBox.Value, out orderID);

            OrderManager.SetComment(orderID, commentTextBox.Text);
        }

        /// <summary>
        /// Fills sender filter list with all order states plus "All states" item
        /// </summary>
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

        /// <summary>
        /// Refreshes order list when filter changes
        /// </summary>
        protected void OnFilterChange(object sender, EventArgs e)
        {
            var pager = (DataPager) OrderList.FindControl("Pager");
            var changesSaved = (Label)OrderList.FindControl("ChangesSavedLabel");

            pager.SetPageProperties(0, 10, true);
            changesSaved.Visible = false;

            OrderList.DataBind();
            pager.DataBind();
        }

        /// <summary>
        /// Saves changes
        /// </summary>
        protected void SaveChanges(object sender, EventArgs e)
        {
            var changesSaved = (Label) OrderList.FindControl("ChangesSavedLabel");

            changesSaved.Visible = true;  
        }

        /// <summary>
        /// Applies filter to OrderList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ApplyFilter(object sender, EventArgs e)
        {
            var lbSender = (LinkButton) sender;
            var direction = OrderList.SortDirection;

            if (OrderList.SortExpression == lbSender.CommandName)
            {
                direction = direction == SortDirection.Ascending
                ? SortDirection.Descending
                : SortDirection.Ascending;
            }
            
            OrderList.Sort(lbSender.CommandName, direction);
        }
    }
}