using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccess
{
    public class PrivatePersonRepository
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

        public List<PrivatePerson> GetAll()
        {
            List<PrivatePerson> privateperson = new();
            string sql = "SELECT PrivatePersonId, Firstname, Lastname, Address, " +
                "Postalcode, Phone FROM PrivatePersons";

            // Uses connection string to connect to database and get data
            SqlConnection connection = new(connectionString);
            SqlCommand command = new(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // Reads through the data and puts it into a list
            while (reader.Read())
            {
                int privatepersonid = (int)reader["PrivatePersonId"];
                string firstname = (string)reader["Firstname"];
                string lastname = (string)reader["Lastname"];
                string address = (string)reader["Address"];
                int postalcode = (int)reader["Postalcode"];
                int phone = (int)reader["Phone"];

                PrivatePerson privatepersons = new()
                {
                    PrivatePersonId = privatepersonid,
                    Firstname = firstname,
                    Lastname = lastname,
                    Address = address,
                    Postalcode = postalcode,
                    Phone = phone
                };

                privateperson.Add(privatepersons);
            }

            connection.Close();
            return privateperson;
        }

        //Inserts a new Object/Row into the PrivatePersons Table
        public void Add(PrivatePerson privateperson)
        {
            string sql = $"INSERT INTO PrivatePersons " +
                $"(Firstname, Lastname, Address, Postalcode, Phone)" +
                $"VALUES ('{privateperson.Firstname}', '{privateperson.Lastname}', " +
                $"'{privateperson.Address}', '{privateperson.Postalcode}', '{privateperson.Phone}')";

            SqlConnection connection = new(connectionString);
            SqlCommand command = new(sql, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}

