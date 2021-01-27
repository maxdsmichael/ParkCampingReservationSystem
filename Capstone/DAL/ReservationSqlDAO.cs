using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    
    public class ReservationSqlDAO : IReservationDAO
    {
        public int reservationId;
        private string connectionString;
        public ReservationSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public int CreateReservation(int site_id, string name, DateTime from_date, DateTime to_date, DateTime creation_date)
        {
            
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT reservation (site_id, name, from_date, to_date, create_date) " +
                             "VALUES(@siteId, @nameForReservation, @from_date, @to_date, @createDate)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@siteId", site_id);
                    cmd.Parameters.AddWithValue("@nameForReservation", name);
                    cmd.Parameters.AddWithValue("@from_date", from_date);
                    cmd.Parameters.AddWithValue("@to_date", to_date);
                    cmd.Parameters.AddWithValue("@createDate", creation_date);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("select max(reservation_id) from reservation", conn);
                    reservationId = Convert.ToInt32(cmd.ExecuteScalar());
                    Console.WriteLine($"The reservation has been made successfully! Your reservation ID is: {reservationId}");
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occured.");
                Console.WriteLine(ex);
                throw;
            }
            return reservationId;
        }
    }
}
