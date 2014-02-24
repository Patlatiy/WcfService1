using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using WebStore.App_Data.Model;
using WebStore.Vasya;

namespace WebStore.Managers
{
    public static class OrderManager
    {
        public static Order CreateOrder(string userLogin, Dictionary<int, int> cart, PaymentMethod paymentMethod, string deliveryAddress, string comment)
        {
            var user = DbContext.Instance.Users.First(usr => usr.Login == userLogin);

            if (user == null) 
                return null;

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

            return order;
        }

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

        public static List<OrderPosition> GetPositions(int orderID)
        {
            return DbContext.Instance.OrderPositions.Where(op => op.OrderID == orderID).ToList();
        }

        public static IQueryable<Order> GetOrders()
        {
            return DbContext.Instance.Orders;
        }

        public static IQueryable<OrderState> GetStates()
        {
            return DbContext.Instance.OrderStates;
        }

        public static decimal GetTotal(int orderID)
        {
            var order = DbContext.Instance.Orders.First(ordr => ordr.ID == orderID);
            return order.OrderPositions.Sum(position => position.Item.Price * position.ItemQuantity);
        }

        public static int GetStateIDForOrderID(int orderID)
        {
            return DbContext.Instance.Orders.First(order => order.ID == orderID).StateID;
        }

        public static void SetState(int orderID, byte stateID)
        {
            var order = GetOrderByID(orderID);
            var state = GetStateByID(stateID);

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
        }

        public static Order GetOrderByID(int orderID)
        {
            return DbContext.Instance.Orders.First(ordr => ordr.ID == orderID);
        }

        public static OrderState GetStateByID(byte stateID)
        {
            return DbContext.Instance.OrderStates.First(os => os.ID == stateID);
        }
    }
}