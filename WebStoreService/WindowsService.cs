using System.ServiceModel;
using System.ServiceProcess;

namespace WebStoreService
{
    public class WsWindowsService : ServiceBase
    {
        public ServiceHost serviceHost = null;
        public WsWindowsService()
        {
            // Name the Windows Service
            ServiceName = "WsWindowsService";
        }

        public static void Main()
        {
            Run(new WsWindowsService());
        }

        // Start the Windows service.
        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            // Create a ServiceHost for the CalculatorService type and 
            // provide the base address.
            serviceHost = new ServiceHost(typeof(CalculatorService));

            // Open the ServiceHostBase to create listeners and start 
            // listening for messages.
            serviceHost.Open();
        }

        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
