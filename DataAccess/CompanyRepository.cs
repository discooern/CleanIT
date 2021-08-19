using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccess{
    public class CompanyRepository
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

        public List<Company> GetAll()
        {
            List<Company> companies = new();
            string sql = "SELECT CompanyId, CompanyName, SeNumber," +
                " Phone FROM Companies";

            // Uses connection string to connect to database and get data
            SqlConnection connection = new(connectionString);
            SqlCommand command = new(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // Reads through the data and puts it into a list
            while (reader.Read())
            {
                int companyid = (int)reader["CompanyId"];
                string companyname = (string)reader["CompanyName"];
                int senumber = (int)reader["SeNumber"];
                int phone = (int)reader["Phone"];

                Company company = new()
                {
                    CompanyId = companyid,
                    CompanyName = companyname,
                    SeNumber = senumber,
                    Phone = phone,
                };

                companies.Add(company);
            }

            connection.Close();
            return companies;
        }

        //Inserts a new Object/Row into the Companies Table
        public void Add(Company companies)
        {
            string sql = $"INSERT INTO Companies " +
                  $"(CompanyName, SeNumber, Phone)" +
                  $"VALUES ('{companies.CompanyName}', '{companies.SeNumber}'," +
                  $" '{companies.Phone}')";

            SqlConnection connection = new(connectionString);
            SqlCommand command = new(sql, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}

