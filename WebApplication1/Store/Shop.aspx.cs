using System;
using System.Collections.Generic;
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

            var categorylabel = (Label) categoryList.FindControl("CategoryLabel");

            categorylabel.Text = id == 0 ? "Showing all items" : ItemManager.GetCategoryDescription(id);
        }

        public IQueryable<Item> GetItems([QueryString("id")] int? categoryId)
        {
            IQueryable<Item> query = ItemManager.GetItems();
            if (categoryId.HasValue && categoryId > 0)
            {
                query = query.Where(p => p.CategoryID == categoryId);
            }
            return query;
        }

        public IQueryable<ItemCategory> GetCategories()
        {
            IQueryable<ItemCategory> query = DbContext.Instance.ItemCategories;
            return query;
        }

        protected string GetRequestId()
        {
            return Request.QueryString["id"];
        }
    }
}