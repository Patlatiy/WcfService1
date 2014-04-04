using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using WebStore.App_Data.Model;
using WebStore.DbWorker;

namespace WebStore.Managers
{
    /// <summary>
    /// Order manager is used to manipulate orders and order positions
    /// </summary>
    public static class OrderManager
    {
        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="userLogin">Login of the user who issues the order</param>
        /// <param name="cart">Cart from the session. It will be transformed to the order positions and then cleared.</param>
        /// <param name="paymentMethod">Chosen payment method</param>
        /// <param name="deliveryAddress">Delivery address</param>
        /// <param name="comment">Comment</param>
        /// <returns>True if successful, false if not</returns>
        public static bool CreateOrder(string userLogin, Dictionary<int, int> cart, PaymentMethod paymentMethod, string deliveryAddress, string comment)
        {
            var user = DbContext.Instance.Users.First(usr => usr.Login == userLogin);

            if (user == null) 
                return false;

            var order = DbContext.Instance.Orders.Create();
            var orderPositions = (from orderPos in cart 
                                  let item = ItemManager.GetItem(orderPos.Key)
                                  select new OrderPosition
                                  {
                                      Order = order, 
                                      OrderID = order.ID, 
                                      Item = item, 
                                      ItemID = item.ID, 
                                      ItemQuantity = orderPos.Value
                                  }).ToList();
            if (!ItemManager.DecreaseItemQuantity(orderPositions))
                return false;

            order.User = user;
            order.UserID = user.ID;
            order.OrderState = DbContext.Instance.OrderStates.First(ost => ost.Name == "Issued");
            order.OrderPositions = orderPositions;
            order.DateIssued = DateTime.Now;
            order.PaymentMethod = paymentMethod;
            order.PaymentMethodID = paymentMethod.ID;
            order.DeliveryAddress = deliveryAddress;
            order.Comment = comment;

            
            DbContext.Instance.Orders.Add(order);
            DbContext.Instance.SaveChanges();

            return true;
        }

        /// <summary>
        /// Retrieves last order address for a user with specified login
        /// </summary>
        /// <param name="userLogin">Login of the user</param>
        /// <returns>Last order address if it exists, empty string if it doesn't</returns>
        public static string GetLastOrderAddress(string userLogin)
        {
            Order order = null;
            foreach (var ordr in DbContext.Instance.Orders)
            {
                if (ordr.User.Login == userLogin)
                {
                    order = ordr;
                }
            }
            return order == null ? string.Empty : order.DeliveryAddress;
        }

        /// <summary>
        /// Retrieves all positions for an order with given ID
        /// </summary>
        /// <param name="orderID">Order ID</param>
        /// <returns>list of order positions</returns>
        public static List<OrderPosition> GetPositions(int orderID)
        {
            return DbContext.Instance.OrderPositions.Where(op => op.OrderID == orderID).ToList();
        }

        /// <summary>
        /// Gets all orders from database
        /// </summary>
        /// <returns>List of all orders</returns>
        public static IQueryable<Order> GetOrders()
        {
            return DbContext.Instance.Orders;
        }

        /// <summary>
        /// Retrieves all possible order states from the database
        /// </summary>
        /// <returns>List of order states</returns>
        public static IQueryable<OrderState> GetStates()
        {
            return DbContext.Instance.OrderStates;
        }

        /// <summary>
        /// Gets total cost for all order positions of an order with given ID
        /// </summary>
        /// <param name="orderID">ID of an order</param>
        /// <returns>Total cost of all positions (decimal)</returns>
        public static decimal GetTotal(int orderID)
        {
            var order = DbContext.Instance.Orders.First(ordr => ordr.ID == orderID);
            return order.OrderPositions.Sum(position => position.Item.Price * position.ItemQuantity);
        }

        /// <summary>
        /// Gets a state ID of an order with given ID
        /// </summary>
        /// <param name="orderID">ID of an order</param>
        /// <returns>State ID</returns>
        public static int GetStateIDForOrderID(int orderID)
        {
            var order = DbContext.Instance.Orders.First(ordr => ordr.ID == orderID);
            return order == null ? 0 : order.StateID;
        }

        /// <summary>
        /// Sets an order with given ID to a state with given ID
        /// </summary>
        /// <param name="orderID">Order ID</param>
        /// <param name="stateID">State ID</param>
        /// <returns>True if successful, false if not</returns>
        public static bool SetState(int orderID, byte stateID)
        {
            var order = GetOrderByID(orderID);
            var state = GetStateByID(stateID);
            if (order == null || state == null)
                return false;

            order.OrderState = state;
            order.StateID = stateID;
            if (state.Name == "Delivered")
            {
                order.DateEnded = DateTime.Now;
            }
            else
            {
                order.DateEnded = null;
            }
                
            DbContext.Instance.SaveChanges();
            return true;
        }

        /// <summary>
        /// Gets an order with given ID
        /// </summary>
        /// <param name="orderID">ID of an order</param>
        /// <returns>Order if it exists, null if it doesn't</returns>
        public static Order GetOrderByID(int orderID)
        {
            return DbContext.Instance.Orders.FirstOrDefault(ordr => ordr.ID == orderID);
        }

        /// <summary>
        /// Gets a state with given ID
        /// </summary>
        /// <param name="stateID">ID of a state</param>
        /// <returns>Order state</returns>
        public static OrderState GetStateByID(byte stateID)
        {
            return DbContext.Instance.OrderStates.FirstOrDefault(os => os.ID == stateID);
        }

        /// <summary>
        /// Set a comment for an order with given ID
        /// </summary>
        /// <param name="orderID">ID of an order</param>
        /// <param name="comment">Comment to set</param>
        /// <returns>True if successful, false if not</returns>
        public static bool SetComment(int orderID, string comment)
        {
            var order = DbContext.Instance.Orders.FirstOrDefault(ordr => ordr.ID == orderID);
            if (order == null) return false;
            order.Comment = comment;
            DbContext.Instance.SaveChanges();
            return true;
        }

        /// <summary>
        /// Determines whether the state with given ID is final (cannot be changed)
        /// </summary>
        /// <param name="stateID">ID of the state</param>
        /// <returns>True if it's final, false if it's not</returns>
        public static bool IsStateFinal(byte stateID)
        {
            var orderState = DbContext.Instance.OrderStates.FirstOrDefault(os => os.ID == stateID);
            if (orderState == null)
                return false;
            return orderState.Name == "Delivered";
        }

        /// <summary>
        /// Retrieves a list of orders that are in a state with specified name
        /// </summary>
        /// <param name="stateName">Name of a state</param>
        /// <returns>List of orders</returns>
        public static IEnumerable<Order> GetOrdersInState(string stateName)
        {
            return DbContext.Instance.Orders.Where(order => order.OrderState.Name == stateName);
        }

        /// <summary>
        /// Strange and redundant method that decides if the state with given ID is 'delivered' state
        /// </summary>
        /// <param name="stateID">ID of the state</param>
        /// <returns>True if state is delivered and false if it's not</returns>
        public static bool IsStateDelivered(byte stateID)
        {
            var orderState = DbContext.Instance.OrderStates.FirstOrDefault(os => os.ID == stateID);
            if (orderState == null)
                return false;
            return orderState.Name == "Delivered";
        }
    }
}