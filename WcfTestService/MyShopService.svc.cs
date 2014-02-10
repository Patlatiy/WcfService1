using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfTestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class MyShopService : IService1
    {
        private WebStoreEntities context = new WebStoreEntities();

        public MyShopService()
        {

        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string CreateTestCategory()
        {
            string result = "";
            ItemCategory newic = new ItemCategory
            {
                Description = "Test Item Category Description",
                Name = "Test Item Category"
            };

            context.ItemCategories.Add(newic);
            context.SaveChanges();
            foreach (ItemCategory itemCategory in context.ItemCategories)
            {
                result += itemCategory.Name + ": " + itemCategory.Description;
            }
            return result;
        }
    }
}
