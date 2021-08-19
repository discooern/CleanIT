using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccess
{
    public class CleaningOrderRepository
    {
        private const string connectionString = @"
             Data Source=(localdb)\MSSQLLocalDB;
             Initial Catalog=CleanIT;
             Integrated Security=True;
             Connect Timeout=30;
             Encrypt=False;
             TrustServerCertificate=False;
             ApplicationIntent=ReadWrite;
             MultiSubnetFailover=False
             ";

        public List<CleaningOrder> GetAll()
        {
            List<CleaningOrder> cleaningorders = new();
            string sql = "SELECT OrderId, CleaningDate, Hours, Completion, Company FROM CleaningOrders";

            // Uses connection string to connect to database and get data
            SqlConnection connection = new(connectionString);
            SqlCommand command = new(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // Reads through the data and puts it into a list
            while (reader.Read())
            {
                int orderid = (int)reader["OrderId"];
                DateTime cleaningdate = (DateTime)reader["CleaningDate"];
                int hours = (int)reader["Hours"];
                bool completion = (bool)reader["Completion"];
                bool company = (bool)reader["Bool"];

                CleaningOrder cleaningorder = new()
                {
                    OrderId = orderid,
                    CleaningDate = cleaningdate,
                    Hours = hours,
                    Completion = completion,
                    Company = company
                };

                cleaningorders.Add(cleaningorder);
            }
            connection.Close();
            return cleaningorders;
        }

        //Inserts a new Object/Row into the CleaningOrders Table
        public void Add(CleaningOrder cleaningorder)
        {
            string sql = $"INSERT INTO CleaningOrders " +
                $"(CleaningDate, Hours, Completion, Company)" +
                $"VALUES ('{cleaningorder.CleaningDate}', '{cleaningorder.Hours}', '{cleaningorder.Completion}', '{cleaningorder.Company}')";

            SqlConnection connection = new(connectionString);
            SqlCommand command = new(sql, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
