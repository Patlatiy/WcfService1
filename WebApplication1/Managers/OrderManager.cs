using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.App_Data.Model;
using WebStore.Vasya;

namespace WebStore.Managers
{
    public static class OrderManager
    {
        public static Order CreateOrder(string userLogin, Dictionary<int, int> cart, PaymentMethod paymentMethod, string deliveryAddress)
        {
            var user = DbWorkerVasya.Instance.Users.First(usr => usr.Login == userLogin);
            if (user == null) return null;
            var order = new Order();
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
            order.OrderState = DbWorkerVasya.Instance.OrderStates.First(ost => ost.Name == "Issued");
            order.OrderPositions = orderPositions;
            order.DateIssued = DateTime.Now;
            order.PaymentMethod = paymentMethod;
            order.PaymentMethodID = paymentMethod.ID;
            order.DeliveryAddress = deliveryAddress;
            DbWorkerVasya.Instance.Orders.Add(order);
            DbWorkerVasya.Instance.SaveChanges();
            return order;
        }

        public static string GetLastOrderAddress(string userLogin)
        {
            Order order = null;
            foreach (var ordr in DbWorkerVasya.Instance.Orders)
            {
                if (ordr.User.Login == userLogin)
                {
                    order = ordr;
                }
            }
            return order == null ? string.Empty : order.DeliveryAddress;
        }
    }
}