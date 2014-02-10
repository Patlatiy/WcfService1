using WebStore.EntityDataModel;

namespace WebStore.SingletonSample
{
    public class WebStoreEntitiesContextSingleton
    {
        private WebStoreEntitiesContext _webStoreEntitiesContext;

        protected WebStoreEntitiesContextSingleton()
        {
            _webStoreEntitiesContext = new WebStoreEntitiesContext();
        }

        private sealed class WebStoreEntitiesSingleton
        {
            private static readonly WebStoreEntitiesContextSingleton instance = new WebStoreEntitiesContextSingleton();

            public static WebStoreEntitiesContextSingleton Instance
            {
                get { return instance; }
            }
        }

        public static WebStoreEntitiesContext Instance
        {
            get { return WebStoreEntitiesSingleton.Instance._webStoreEntitiesContext; }
        }

    }
}