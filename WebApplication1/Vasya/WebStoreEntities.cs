using System;
using WebStore.App_Data.Model;

namespace WebStore.Vasya
{
    public class DbWorkerVasya
    {
        private readonly WebStoreEntities _webStoreEntitiesContext;

        protected DbWorkerVasya()
        {
            _webStoreEntitiesContext = new WebStoreEntities();
        }

        protected sealed class WebStoreEntitiesSingleton
        {
            private static readonly DbWorkerVasya instance = new DbWorkerVasya();

            public static DbWorkerVasya Instance
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