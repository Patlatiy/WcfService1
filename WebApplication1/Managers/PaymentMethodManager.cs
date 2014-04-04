using System.Linq;
using WebStore.App_Data.Model;
using WebStore.DbWorker;

namespace WebStore.Managers
{
    /// <summary>
    /// Payment method manager is used to manipulate payment methods
    /// </summary>
    public static class PaymentMethodManager
    {
        /// <summary>
        /// Retrieves all payment methods from database
        /// </summary>
        /// <returns>List of payment methods</returns>
        public static IQueryable<PaymentMethod> GetPaymentMethods()
        {
            return DbContext.Instance.PaymentMethods;
        }

        /// <summary>
        /// Retrieves a payment method with given ID
        /// </summary>
        /// <param name="id">ID of the payment method</param>
        /// <returns>Payment method if it exists, null if it doesn't</returns>
        public static PaymentMethod GetPaymentMethodByID(byte id)
        {
            return DbContext.Instance.PaymentMethods.FirstOrDefault(mthd => mthd.ID == id);
        }
    }
}