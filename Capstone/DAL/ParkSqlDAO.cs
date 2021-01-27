using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ParkSqlDAO : IParkDAO
    {
        private string connectionString;
        public ParkSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<Park> GetAllParks()
        {
            IList<Park> parks = new List<Park>();
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        string sql = "SELECT * FROM Park ORDER BY name ASC";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            Park park = RowToObject(rdr);
                            parks.Add(park);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("An error occured." + ex);
                }
                return parks;
            }

        }

        public Park GetParkById(int parkId)
        {
            Park park = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "Select * from park where park_id = @parkId";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkId", parkId);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        park = RowToObject(rdr);
                    }
                    
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Sorry there was an Error. Message {ex.Message}");
            }
            return park;
        }

        private static Park RowToObject(SqlDataReader rdr)
        {
            Park park = new Park();
            park.Area = Convert.ToInt32(rdr["Area"]);
            park.Description = Convert.ToString(rdr["Description"]);
            park.Establish_Date = Convert.ToDateTime(rdr["Establish_Date"]);
            park.Location = Convert.ToString(rdr["Location"]);
            park.Name = Convert.ToString(rdr["name"]);
            park.Park_Id = Convert.ToInt32(rdr["park_id"]);
            park.Visitors = Convert.ToInt32(rdr["visitors"]);
            return park;
        }
    }
}