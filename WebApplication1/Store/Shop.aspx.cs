using System;
using System.Linq;
using System.Web.UI;
using WebStore.App_Data.Model;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using WebStore.DbWorker;
using WebStore.Managers;

namespace WebStore.Store
{
    public partial class Shop : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            byte id;
            byte.TryParse(Request.QueryString["id"], out id);

            int startRow;
            int.TryParse(Request.QueryString["startrow"], out startRow);

            var categorylabel = (Label) categoryList.FindControl("CategoryLabel");

            categorylabel.Text = id == 0 ? "Showing all items" : ItemManager.GetCategoryDescription(id);

            Pager.SetPageProperties(startRow, 12, true);
        }

        /// <summary>
        /// Gets all items from current category
        /// </summary>
        /// <param name="categoryId">Category ID</param>
        /// <returns>Queryable list of items</returns>
        public IQueryable<Item> GetItems([QueryString("id")] int? categoryId)
        {
            IQueryable<Item> query = ItemManager.GetItems();
            if (categoryId.HasValue && categoryId > 0)
            {
                query = query.Where(p => p.CategoryID == categoryId);
            }
            return query;
        }

        /// <summary>
        /// Gets all categories from database
        /// </summary>
        /// <returns>Queryable list of categories</returns>
        public IQueryable<ItemCategory> GetCategories()
        {
            IQueryable<ItemCategory> query = DbContext.Instance.ItemCategories;
            return query;
        }

        /// <summary>
        /// An auxiliary function used to get "id" part of current query string
        /// </summary>
        /// <returns>String, id from current query string</returns>
        protected string GetRequestId()
        {
            return Request.QueryString["id"];
        }
    }
}