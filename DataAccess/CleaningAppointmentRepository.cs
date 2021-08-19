using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccess
{
    public class CleaningAppointmentRepository
    {

        // Connectionstring for sqlconnection
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

        public List<CleaningAppointment> GetAll()
        {
            List<CleaningAppointment> cleaningappointments = new();
            string sql = "SELECT CleaningAppointmentId, CustomerId, Company," +
                " HourPrice, AppointmentDesc FROM CleaningAppointments";

            // Uses connection string to connect to database and get data
            SqlConnection connection = new(connectionString);
            SqlCommand command = new(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // Reads through the data and puts it into a list
            while (reader.Read())
            {
                int cleaningappointmentid = (int)reader["CleaningAppointmentId"];
                int customerid = (int)reader["CustomerId"];
                bool company = (bool)reader["Company"];
                int hourprice = (int)reader["HourPrice"];
                string appointmentdesc = (string)reader["AppointmentDesc"];

                CleaningAppointment cleaningappointment = new()
                {
                    CleaningAppointmentId = cleaningappointmentid,
                    CustomerId = customerid,
                    Company = company,
                    HourPrice = hourprice,
                    AppointmentDesc = appointmentdesc
                };

                cleaningappointments.Add(cleaningappointment);
            }

            connection.Close();
            return cleaningappointments;
        }

        //Inserts a new Object/Row into the CleaningAppointments Table
        public void Add(CleaningAppointment cleaningappointments)
        {
            string sql = $"INSERT INTO CleaningAppointments " +
                  $"(CustomerId, Company, HourPrice, AppointmentDesc)" +
                  $"VALUES ('{cleaningappointments.CustomerId}', '{cleaningappointments.Company}'," +
                  $" '{cleaningappointments.HourPrice}', '{cleaningappointments.AppointmentDesc}')";

            SqlConnection connection = new(connectionString);
            SqlCommand command = new(sql, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}


