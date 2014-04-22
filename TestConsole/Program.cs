using System;
using System.Data;
using System.Data.SqlClient;

namespace TestConsole
{
    class Program
    {
        private const string ConnectionString = "Server=localhost;Database=WebStore;Trusted_Connection=True;";

        static void Main(string[] args)
        {

            var connection = new SqlConnection(ConnectionString);
            var sqlCommand = new SqlCommand("SELECT [ID], [LastActiveDateTime], [RoleID] FROM [WebStore].[WS].[User] WHERE [IsBlocked] = 0", connection);
            var da = new SqlDataAdapter(sqlCommand);
            var ds = new DataSet();

            connection.Open();
            da.Fill(ds);

            var currentDate = DateTime.Now;
            var deadline = currentDate.AddDays(-90);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var userDate = (DateTime) row.ItemArray[1];
                var roleID = (byte) row.ItemArray[2];

                if (roleID != 5)
                    continue;

                if (deadline > userDate)
                {
                    var blockCommand =
                        new SqlCommand("UPDATE [Webstore].[WS].[User] SET [IsBlocked]=1 WHERE [ID]=" + row.ItemArray[0], connection);
                    
                    Console.Write(userDate);
                    Console.WriteLine(" deleted!");
                    Console.WriteLine(blockCommand.ExecuteNonQuery() + " row affected.");
                }
            }

            Console.ReadLine();
            connection.Close();
        }
    }
}
