using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebStore.App_Data.Model;
using WebStore.Vasya;

namespace WebStore.Managers
{
    public static class PaymentMethodManager
    {
        public static IQueryable<PaymentMethod> GetPaymentMethods()
        {
            return DbContext.Instance.PaymentMethods;
        }

        public static PaymentMethod GetPaymentMethodByID(byte id)
        {
            return DbContext.Instance.PaymentMethods.First(mthd => mthd.ID == id);
        }
    }
}