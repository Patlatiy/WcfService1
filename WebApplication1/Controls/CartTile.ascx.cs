using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebStore.Controls
{
    public partial class CartTile : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected string ItemsInOrder()
        {
            var cart = (Dictionary<int, int>) Session["Cart"];
            return cart == null ? "0" : cart.Sum(item => item.Value).ToString("G");
        }

        public void UpdateShownItemCount()
        {
            ItemCountPanel.Update();
        }
    }
}