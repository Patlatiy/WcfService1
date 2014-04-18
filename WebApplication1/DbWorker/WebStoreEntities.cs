using WebStore.App_Data.Model;

namespace WebStore.DbWorker
{
    public class DbContext
    {
        private readonly WebStoreEntities _webStoreEntitiesContext;

        private DbContext()
        {
            _webStoreEntitiesContext = new WebStoreEntities();
        }

        private sealed class WebStoreEntitiesSingleton
        {
            private static readonly DbContext instance = new DbContext();

            public static DbContext Instance
            {
                get { return instance; }
            }
        }

        public static WebStoreEntities Instance
        {
            get { return WebStoreEntitiesSingleton.Instance._webStoreEntitiesContext; }
        }
    }
}