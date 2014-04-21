using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.App_Data.Model;
using WebStore.DbWorker;

namespace WebStore.Managers
{
    /// <summary>
    /// Item Manager is used to manipulate items and item categories
    /// </summary>
    public static class ItemManager
    {
        /// <summary>
        /// Calculates the sum for all items in cart
        /// </summary>
        /// <param name="cart">Cart with items</param>
        /// <returns>String with "$" sign appended</returns>
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

        /// <summary>
        /// Gets an item price from database
        /// </summary>
        /// <param name="itemID">Item ID</param>
        /// <returns>Item price if item exist or null if it doesn't</returns>
        public static decimal GetTotalItemPrice(int itemID)
        {
            var item = DbContext.Instance.Items.First(it => it.ID == itemID);
            return item == null ? 0 : item.Price;
        }

        /// <summary>
        /// Retrieves all items from database
        /// </summary>
        /// <returns>List of all items</returns>
        public static IQueryable<Item> GetItems()
        {
            return DbContext.Instance.Items.Where(item => item.IsActive == true);
        }

        /// <summary>
        /// Gets an item with given ID from database
        /// </summary>
        /// <param name="id">ID of the item</param>
        /// <returns></returns>
        public static Item GetItem(int id)
        {
            return DbContext.Instance.Items.FirstOrDefault(item => item.ID == id);
        }

        /// <summary>
        /// Sets a name of an item with given ID
        /// </summary>
        /// <param name="itemID">ID of the item</param>
        /// <param name="name">Name to set</param>
        /// <returns>True if successful, false if not</returns>
        public static bool SetName(int itemID, string name)
        {
            var item = GetItem(itemID);
            if (item == null) return false;

            item.Name = name;

            DbContext.Instance.SaveChanges();
            return true;
        }

        /// <summary>
        /// Sets an image path of an item with given ID
        /// </summary>
        /// <param name="itemID">ID of the item</param>
        /// <param name="imagepath">Image path to set</param>
        /// <returns>True if successful, false if not</returns>
        public static bool SetImage(int itemID, string imagepath)
        {
            var item = GetItem(itemID);
            if (item == null)
                return false;

            item.Image = imagepath;

            DbContext.Instance.SaveChanges();
            return true;
        }

        /// <summary>
        /// Return item image or "noimage.png" if item doesn't exist
        /// </summary>
        /// <param name="itemID">Item ID</param>
        public static string GetImage(int itemID)
        {
            var item = GetItem(itemID);
            return item == null ? "noimage.png" : item.Image;
        }

        /// <summary>
        /// Sets a description for an item with given ID
        /// </summary>
        /// <param name="itemID">ID of the item</param>
        /// <param name="desc">Description to set</param>
        /// <returns>True if successful, false if not</returns>
        public static bool SetDescription(int itemID, string desc)
        {
            var item = GetItem(itemID);
            if (item == null)
                return false;

            item.Description = desc;

            DbContext.Instance.SaveChanges();
            return true;
        }

        /// <summary>
        /// Sets a quantity of an item with given ID
        /// </summary>
        /// <param name="itemID">ID of the item</param>
        /// <param name="quantity">Quantity to set</param>
        /// <returns>True if successful, false if not</returns>
        public static bool SetQuantity(int itemID, int quantity)
        {
            var item = GetItem(itemID);
            if (item == null)
                return false;

            if (quantity < 0)
                quantity = 0;

            item.Quantity = quantity;

            DbContext.Instance.SaveChanges();
            return true;
        }

        /// <summary>
        /// Sets a price of an item with given ID
        /// </summary>
        /// <param name="itemID">ID of the item</param>
        /// <param name="price">Price to set</param>
        /// <returns>True if successful, false if not</returns>
        public static bool SetPrice(int itemID, decimal price)
        {
            var item = GetItem(itemID);
            if (item == null)
                return false;

            item.Price = price;

            DbContext.Instance.SaveChanges();
            return true;
        }

        /// <summary>
        /// Retrieves all item categories from database
        /// </summary>
        /// <returns>List of all item categories</returns>
        public static IQueryable<ItemCategory> GetCategories()
        {
            return DbContext.Instance.ItemCategories;
        }

        /// <summary>
        /// Retrieves an item category with given ID
        /// </summary>
        /// <param name="categoryID">ID of category to retrieve</param>
        /// <returns>Item category if it exists, null if it doesn't</returns>
        public static ItemCategory GetCategory(int categoryID)
        {
            if (categoryID == 0)
                return null;
            return DbContext.Instance.ItemCategories.First(cat => cat.ID == categoryID);
        }

        /// <summary>
        /// Retrieves an item category with given name
        /// </summary>
        /// <param name="categoryName">Name of the category to retrieve</param>
        /// <returns>Item category if it exists, null if it doesn't</returns>
        public static ItemCategory GetCategory(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
                return null;
            return DbContext.Instance.ItemCategories.FirstOrDefault(cat => cat.Name == categoryName);
        }

        /// <summary>
        /// Retrieves an item category of an item with given ID
        /// </summary>
        /// <param name="itemID">ID of the item</param>
        /// <returns>ite mcategory if it exists, null if it doesn't</returns>
        public static ItemCategory GetItemCategory(int itemID)
        {
            var item = DbContext.Instance.Items.FirstOrDefault(itm => itm.ID == itemID);
            return item == null ? null : item.ItemCategory;
        }

        /// <summary>
        /// Retrieves a name of a category of an item with given ID
        /// </summary>
        /// <param name="itemID">ID of the item</param>
        /// <returns>Name of the category if it exists, empty string if it doesn't</returns>
        public static string GetItemCategoryName(int itemID)
        {
            var itemCategory = GetItemCategory(itemID);
            return itemCategory != null ? itemCategory.Name : string.Empty;
        }

        /// <summary>
        /// Sets a category for the item with given ID
        /// </summary>
        /// <param name="itemID">ID of the item</param>
        /// <param name="categoryID">ID of the category to set</param>
        /// <returns>True if successful, false if not</returns>
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

        /// <summary>
        /// Sets a category of the item with given ID
        /// </summary>
        /// <param name="itemID">ID of the item</param>
        /// <param name="categoryName">Name of the category to set</param>
        /// <returns>True if successful, false if not</returns>
        public static bool SetCategory(int itemID, string categoryName)
        {
            var category = GetCategory(categoryName);
            return category == null ? SetCategory(itemID, 0) : SetCategory(itemID, category.ID);
        }

        /// <summary>
        /// Sets a name for the category with given ID
        /// </summary>
        /// <param name="categoryID">ID of the category</param>
        /// <param name="categoryName">Name to set</param>
        /// <returns>True if successful, false if not</returns>
        public static bool SetCategoryName(int categoryID, string categoryName)
        {
            var category = GetCategory(categoryID);
            if (category == null)
                return false;

            category.Name = categoryName;
            DbContext.Instance.SaveChanges();
            return true;
        }

        /// <summary>
        /// Sets a description for the category with given ID
        /// </summary>
        /// <param name="categoryID">Id of the category</param>
        /// <param name="categoryDescription">Description to set</param>
        /// <returns>True if successful, false if not</returns>
        public static bool SetCategoryDescription(int categoryID, string categoryDescription)
        {
            var category = GetCategory(categoryID);
            if (category == null)
                return false;

            category.Description = categoryDescription;
            DbContext.Instance.SaveChanges();
            return true;
        }

        /// <summary>
        /// Deletes a category with given ID
        /// </summary>
        /// <param name="categoryID">ID of the category to delete</param>
        /// <returns>True if successful, false if not</returns>
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

        /// <summary>
        /// Creates a new category with given name and description
        /// </summary>
        /// <param name="name">Name of the new category</param>
        /// <param name="description">Description of the new category</param>
        /// <returns>True if successful, false if not</returns>
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

        /// <summary>
        /// Creates an item category
        /// </summary>
        /// <param name="name">Name of the item</param>
        /// <param name="description">Description of the item</param>
        /// <param name="image">Image path of the item</param>
        /// <param name="price">Price of the item</param>
        /// <param name="quantity">Quantity of the item</param>
        /// <param name="category">Category name of the item</param>
        /// <returns>True if successful, false if not</returns>
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
                newItem.IsActive = true;

                DbContext.Instance.Items.Add(newItem);
                DbContext.Instance.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes an item with given ID
        /// </summary>
        /// <param name="itemID">ID of the item to delete</param>
        /// <returns>True if successful, false if not</returns>
        public static bool DeleteItem(int itemID)
        {
            var item = GetItem(itemID);
            if (item == null)
                return false;

            item.IsActive = false;
            DbContext.Instance.SaveChanges();
            return true;
        }

        /// <summary>
        /// Retrieves an item with given name
        /// </summary>
        /// <param name="itemName">Name of the item</param>
        /// <returns></returns>
        public static Item GetItem(string itemName)
        {
            return DbContext.Instance.Items.FirstOrDefault(item => item.Name == itemName);
        }

        /// <summary>
        /// Decreases an item quantity in store on a given amount
        /// </summary>
        /// <param name="positions">Positions from order</param>
        /// <returns>True if successful, false if not</returns>
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

        /// <summary>
        /// Checks if we have all the items from cart in store in needed amount
        /// </summary>
        /// <param name="cart">Cart, generally stored in session</param>
        /// <returns>True if we have all the items, false if we don't</returns>
        public static bool CheckCart(Dictionary<int, int> cart)
        {
            return cart.All(record => GetItem(record.Key).Quantity >= record.Value);
        }

        /// <summary>
        /// Retrieves a description of a category with given ID
        /// </summary>
        /// <param name="categoryID">ID of the category</param>
        /// <returns>Category description if it exists, "No category" string if it doesn't</returns>
        public static string GetCategoryDescription(byte categoryID)
        {
            var category = DbContext.Instance.ItemCategories.FirstOrDefault(cat => cat.ID == categoryID);
            return category == null ? "No category" : category.Description;
        }

        /// <summary>
        /// Retrieves all items that are in a category with specified name
        /// </summary>
        /// <param name="categoryName">Category name</param>
        /// <returns>List of items</returns>
        public static IQueryable<Item> GetItemsInCategory(string categoryName)
        {
            var category = DbContext.Instance.ItemCategories.FirstOrDefault(cat => cat.Name == categoryName);
            return category == null ? null : category.Items.AsQueryable().Where(item => item.IsActive);
        }

        /// <summary>
        /// Checks whether item is active or deleted
        /// </summary>
        /// <param name="itemID">Item ID</param>
        /// <returns>True if item is active, false if it's not or doesn't exist</returns>
        public static bool ItemIsActive(int itemID)
        {
            var item = DbContext.Instance.Items.FirstOrDefault(itm => itm.ID == itemID);
            return item != null && item.IsActive;
        }
    }
}