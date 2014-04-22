using System;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;

namespace WebStoreService
{
    [ServiceContract]
    public interface IWebStoreService
    {
        [OperationContract]
        bool BlockInactiveUsers();
    }

    class WebStoreService : IWebStoreService
    {
        private const string ConnectionString =
            "Server=localhost;Database=WebStore;Trusted_Connection=True;";

        /// <summary>
        /// Blocks all users that were last active earlier that 3 months ago
        /// </summary>
        /// <returns>True if operation successfully finished (even if no user was blocked)</returns>
        public bool BlockInactiveUsers()
        {
            var connection = new SqlConnection(ConnectionString);
            try
            {
                var sqlCommand =
                    new SqlCommand(
                        "SELECT [ID], [LastActiveDateTime], [RoleID] " +
                        "FROM [WebStore].[WS].[User] " +
                        "WHERE [IsBlocked] = 0",
                        connection);
                var da = new SqlDataAdapter(sqlCommand);
                var ds = new DataSet();

                connection.Open();
                da.Fill(ds);

                var deadline = DateTime.Now.AddDays(-90);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var userDate = (DateTime) row.ItemArray[1];
                    var roleID = (byte) row.ItemArray[2];

                    if (roleID != 5)
                        continue;

                    if (deadline > userDate)
                    {
                        var blockCommand =
                            new SqlCommand(
                                "UPDATE [Webstore].[WS].[User] " +
                                "SET [IsBlocked] = 1 " +
                                "WHERE [ID] = " + row.ItemArray[0],
                                connection);
                        blockCommand.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return true;
        }
    }
}
