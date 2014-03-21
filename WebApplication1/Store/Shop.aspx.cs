using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using WebStore.App_Data.Model;
using WebStore.DbWorker;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using WebStore.Managers;

namespace WebStore.Store
{
    public partial class Shop : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<Item> GetItems([QueryString("id")] int? categoryId)
        {
            IQueryable<Item> query = DbContext.Instance.Items;
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
    }
}