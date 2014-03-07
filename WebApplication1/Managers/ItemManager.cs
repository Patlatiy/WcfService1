using System.Collections.Generic;
using System.Linq;
using WebStore.App_Data.Model;
using WebStore.DbWorker;

namespace WebStore.Managers
{
    public static class ItemManager
    {
        public static string GetTotalItemPriceForAllItems(Dictionary<int, int> cart)
        {
            if (cart == null) return "$0";

            decimal total = 0;
            foreach (var itemID in cart.Keys)
            {
                var item = DbContext.Instance.Items.First(it => it.ID == itemID);
                if (item == null) continue;
                if (cart[itemID] > 0)
                {
                    total += item.Price * cart[itemID];
                }
            }

            return "$" + total.ToString("G");
        }

        public static decimal GetTotalItemPrice(int itemID)
        {
            var item = DbContext.Instance.Items.First(it => it.ID == itemID);
            return item == null ? 0 : item.Price;
        }

        public static IQueryable<Item> GetItems()
        {
            return DbContext.Instance.Items;
        }

        public static Item GetItem(int id)
        {
            return DbContext.Instance.Items.First(item => item.ID == id);
        }

        public static bool SetName(int itemID, string name)
        {
            var item = GetItem(itemID);
            if (item == null) return false;

            item.Name = name;

            DbContext.Instance.SaveChanges();
            return true;
        }

        public static bool SetImage(int itemID, string imagepath)
        {
            var item = GetItem(itemID);
            if (item == null) 
                return false;

            item.Image = imagepath;

            DbContext.Instance.SaveChanges();
            return true;
        }

        public static bool SetDescription(int itemID, string desc)
        {
            var item = GetItem(itemID);
            if (item == null)
                return false;

            item.Description = desc;

            DbContext.Instance.SaveChanges();
            return true;
        }

        public static bool SetQuantity(int itemID, int quantity)
        {
            var item = GetItem(itemID);
            if (item == null)
                return false;

            item.Quantity = quantity;

            DbContext.Instance.SaveChanges();
            return true;
        }

        public static bool SetPrice(int itemID, decimal price)
        {
            var item = GetItem(itemID);
            if (item == null)
                return false;

            item.Price = price;

            DbContext.Instance.SaveChanges();
            return true;
        }
    }
}