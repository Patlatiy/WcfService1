using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebStore.App_Data.Model;
using WebStore.Controls;
using WebStore.Managers;

namespace WebStore.Store
{
    public partial class Complete : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var paymentMethodID = (byte)Session["PaymentMethodID"];
                var paymentMethod = PaymentMethodManager.GetPaymentMethodByID(paymentMethodID);

                var cart = (Dictionary<int, int>) Session["Cart"];
                if (cart == null) throw new Exception("Session error");
                var address = (string)Session["Address"] ?? "N/A";
                var comment = (string)Session["Comment"] ?? string.Empty;
                if (OrderManager.CreateOrder(User.Identity.Name, cart, paymentMethod, address, comment))
                {
                    Session["Cart"] = null;

                    ThankYouLabel.Text = "Thank you for using our store. You've paid by " + paymentMethod.Name + ".";
                    LoggedInPanel.Visible = true;
                }
                else
                {
                    ThankYouLabel.Text =
                        "There was an error processing your request. It is possible that we don't have so many items that you've ordered.";
                    LoggedInPanel.Visible = true;
                }
                
            }
            catch (Exception)
            {
                AnonymousPanel.Visible = true;
            }
            
        }
    }
}