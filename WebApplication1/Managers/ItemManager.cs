using System;
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
            return DbContext.Instance.Items.FirstOrDefault(item => item.ID == id);
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

        public static IQueryable<ItemCategory> GetCategories()
        {
            return DbContext.Instance.ItemCategories;
        }

        public static ItemCategory GetCategory(int categoryID)
        {
            if (categoryID == 0)
                return null;
            return DbContext.Instance.ItemCategories.First(cat => cat.ID == categoryID);
        }

        public static ItemCategory GetCategory(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
                return null;
            return DbContext.Instance.ItemCategories.FirstOrDefault(cat => cat.Name == categoryName);
        }

        public static ItemCategory GetItemCategory(int itemID)
        {
            var item = DbContext.Instance.Items.FirstOrDefault(itm => itm.ID == itemID);
            return item == null ? null : item.ItemCategory;
        }

        public static string GetItemCategoryName(int itemID)
        {
            var itemCategory = GetItemCategory(itemID);
            return itemCategory != null ? itemCategory.Name : string.Empty;
        }

        public static bool SetCategory(int itemID, int categoryID)
        {
            var item = GetItem(itemID);
            if (item == null)
                return false;
            var category = GetCategory(categoryID);

            item.ItemCategory = category;
            item.CategoryID = category != null ? (int?)category.ID : null;

            DbContext.Instance.SaveChanges();
            return true;
        }

        public static bool SetCategory(int itemID, string categoryName)
        {
            var category = GetCategory(categoryName);
            return category == null ? SetCategory(itemID, 0) : SetCategory(itemID, category.ID);
        }

        public static bool SetCategoryName(int categoryID, string categoryName)
        {
            var category = GetCategory(categoryID);
            if (category == null)
                return false;

            category.Name = categoryName;
            DbContext.Instance.SaveChanges();
            return true;
        }

        public static bool SetCategoryDescription(int categoryID, string categoryDescription)
        {
            var category = GetCategory(categoryID);
            if (category == null)
                return false;

            category.Description = categoryDescription;
            DbContext.Instance.SaveChanges();
            return true;
        }

        public static bool DeleteCategory(int categoryID)
        {
            var category = GetCategory(categoryID);
            if (category == null)
                return false;

            foreach (var item in category.Items)
            {
                item.CategoryID = null;
                item.ItemCategory = null;
            }

            DbContext.Instance.ItemCategories.Remove(category);
            DbContext.Instance.SaveChanges();
            return true;
        }

        public static bool CreateCategory(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            if (DbContext.Instance.ItemCategories.FirstOrDefault(cat => cat.Name == name) != null)
                return false;

            var category = DbContext.Instance.ItemCategories.Create();
            category.Name = name;
            if (!string.IsNullOrEmpty(description))
                category.Description = description;
            DbContext.Instance.ItemCategories.Add(category);

            DbContext.Instance.SaveChanges();
            return true;
        }

        public static bool CreateItem(string name, string description, string image, decimal price, int quantity, string category)
        {
            try
            {
                var newItem = DbContext.Instance.Items.Create();

                newItem.Name = name;
                newItem.Description = description;
                newItem.Image = image;
                newItem.Price = price;
                newItem.Quantity = quantity;
                newItem.ItemCategory = GetCategory(category);

                DbContext.Instance.Items.Add(newItem);
                DbContext.Instance.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool DeleteItem(int itemID)
        {
            var item = GetItem(itemID);
            if (item == null)
                return false;

            DbContext.Instance.Items.Remove(item);
            DbContext.Instance.SaveChanges();
            return true;
        }

        public static Item GetItem(string itemName)
        {
            return DbContext.Instance.Items.FirstOrDefault(item => item.Name == itemName);
        }

        public static bool DecreaseItemQuantity(List<OrderPosition> positions)
        {
            //First check if we have all the items
            foreach (var orderPosition in positions)
            {
                if (GetItem(orderPosition.ItemID).Quantity < orderPosition.ItemQuantity)
                    return false;
            }
            //Then decrease them all at once
            foreach (var orderPosition in positions)
            {
                GetItem(orderPosition.ItemID).Quantity -= orderPosition.ItemQuantity;
            }
            return true;
        }

        public static bool CheckCart(Dictionary<int, int> cart)
        {
            return cart.All(record => GetItem(record.Key).Quantity >= record.Value);
        }
    }
}