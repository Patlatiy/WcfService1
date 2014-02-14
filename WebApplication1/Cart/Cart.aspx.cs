using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using WebStore.App_Data.Model;
using WebStore.Vasya;

namespace WebStore.Cart
{
    public partial class Cart : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public IQueryable<Item> GetItems()
        {
            if (Session["Cart"] == null) return null;
            var cart = (Dictionary<int, int>) Session["Cart"];
            IQueryable<Item> query = DbWorkerVasya.Instance.Items;
            var itemList = new List<Item>();
            foreach (var query2 in cart.Keys.Select(val => query.Where(item => item.ID == val)))
            {
                itemList.AddRange(query2.ToList());
            }
            query = itemList.AsQueryable();
            return query;
        }

        protected string GetTotalItemPriceForAllItems()
        {
            if (Session["Cart"] == null) return "$0";
            var cart = (Dictionary<int, int>)Session["Cart"];
            decimal total = 0;
            foreach (var itemID in cart.Keys)
            {
                var item = DbWorkerVasya.Instance.Items.First(it => it.ID == itemID);
                if (item == null) continue;
                if (cart[itemID] > 0)
                {
                    total += item.Price*cart[itemID];
                }
            }
            return "$" + total.ToString("G");
        }
        
        protected string GetTotalItemPrice(int itemID)
        {
            if (Session["Cart"] == null) return "$0";
            var cart = (Dictionary<int, int>)Session["Cart"];
            var item = DbWorkerVasya.Instance.Items.First(it => it.ID == itemID);
            if (item == null) return "$0";
            if (cart[itemID] > 1)
            {
                return "$" + item.Price.ToString("G") + " x " + cart[itemID].ToString("G") + " = $" +
                       (item.Price*cart[itemID]).ToString("G");
            }
            return "$" + item.Price.ToString("G");
        }
    }
}