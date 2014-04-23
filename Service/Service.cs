
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.ServiceModel;
using System.ServiceProcess;
using System.Configuration;
using System.Configuration.Install;
using System.Threading;

namespace WebStore.Service
{
    // Define a service contract.
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public interface IWsService
    {
        [OperationContract]
        bool BlockUsers();
    }

    // Implement the ICalculator service contract in a service class.
    public class WsService : IWsService
    {
        // Implement the ICalculator methods.
        public bool BlockUsers()
        {
            return true;
        }
    }

    public class WsWindowsService : ServiceBase
    {
        public ServiceHost serviceHost = null;
        private Timer _daemon;
        private const string ConnectionString = "Server=localhost;User Id=wsuser;Password=BrainBench2012;";

        public WsWindowsService()
        {
            // Name the Windows Service
            ServiceName = "WCFWindowsServiceSample";
        }

        public static void Main()
        {
            ServiceBase.Run(new WsWindowsService());
        }

        // Start the Windows service.
        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            // Create a ServiceHost for the WsService type and 
            // provide the base address.
            serviceHost = new ServiceHost(typeof(WsService));

            // Open the ServiceHostBase to create listeners and start 
            // listening for messages.
            serviceHost.Open();

            _daemon = new Timer(new TimerCallback(CheckUsers));
            _daemon.Change(0, 3600000);
        }

        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }

            if (_daemon != null)
            {
                _daemon.Dispose();
            }
        }

        private static void CheckUsers(object state)
        {
            var connection = new SqlConnection(ConnectionString);
            try
            {

                var sqlCommand =
                    new SqlCommand(
                        "SELECT [ID], [LastActiveDateTime] FROM [WebStore].[WS].[User] WHERE [IsBlocked] = 0 AND [RoleID] = 5",
                        connection);
                var da = new SqlDataAdapter(sqlCommand);
                var ds = new DataSet();

                connection.Open();
                da.Fill(ds);

                var currentDate = DateTime.Now;
                var deadline = currentDate.AddDays(-90);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var userDate = (DateTime) row.ItemArray[1];

                    if (deadline > userDate)
                    {
                        var blockCommand =
                            new SqlCommand(
                                "UPDATE [Webstore].[WS].[User] SET [IsBlocked]=1 WHERE [ID]=" + row.ItemArray[0],
                                connection);
                        blockCommand.ExecuteNonQuery();
                    }
                }
                LogMessage("Check successful!");
            }
            catch (Exception exception)
            {
                LogMessage(exception.Message);
                if (exception.InnerException != null)
                    LogMessage("Inner Exception: " + exception.InnerException.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private static void BlockUser()
        {
            
        }

        private static void NotifyUser()
        {
            
        }

        private static void LogMessage(string message)
        {
            const string filename = "C:\\Temp\\log.txt";
            if (!File.Exists(filename))
                File.Create(filename);
            var writer = new StreamWriter(filename, true);
            writer.WriteLine(message);
            writer.Close();
        }
    }

    // Provide the ProjectInstaller class which allows 
    // the service to be installed by the Installutil.exe tool
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        public ProjectInstaller()
        {
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.ServiceName = "WebStoreService";
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
