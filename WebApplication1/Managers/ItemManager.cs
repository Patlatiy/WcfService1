using System.Collections.Generic;
using System.Linq;
using WebStore.App_Data.Model;
using WebStore.Vasya;

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
                    total += item.Price*cart[itemID];
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
}
}