using WebStore.App_Data.Model;

namespace WebStore.SingletonSample
{
    public class WebStoreEntitiesContextSingleton
    {
        private WebStoreEntities _webStoreEntitiesContext;

        protected WebStoreEntitiesContextSingleton()
        {
            _webStoreEntitiesContext = new WebStoreEntities();
        }

        private sealed class WebStoreEntitiesSingleton
        {
            private static readonly WebStoreEntitiesContextSingleton instance = new WebStoreEntitiesContextSingleton();

            public static WebStoreEntitiesContextSingleton Instance
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