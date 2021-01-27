using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Capstone.DAL
{
    public class CampgroundSqlDAO : ICampgroundDAO
    {
        //Private Variables 
        private string connectionString;
        //Constructor
        public CampgroundSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public IList<Campground> GetAllCampgrounds(int parkId)
        {
            IList<Campground> campgrounds = new List<Campground>();
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        string sql = "Select * from campground where park_id = @parkId";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@parkId", parkId);
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            Campground campground = RowToObject(rdr);
                            campgrounds.Add(campground);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    System.Console.WriteLine("An error occured." + ex);
                }
                return campgrounds;
            }
        }
        public Campground GetCampgroundById(int campgroundId)
        {
            Campground campground = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "Select * from campground where campground_id = @campgroundId";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@campgroundId", campgroundId);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        campground = RowToObject(rdr);
                    }

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Sorry there was an Error. Message {ex.Message}");
            }
            return campground;
        }
        public override string ToString()
        {
            return $"";
        }

        private static Campground RowToObject(SqlDataReader rdr)
        {
            Campground camp = new Campground();
            camp.Campground_ID = Convert.ToInt32(rdr["campground_id"]);
            camp.Park_ID = Convert.ToInt32(rdr["park_id"]);
            camp.Name = Convert.ToString(rdr["name"]);
            camp.Open_From_Month = Convert.ToInt32(rdr["open_from_mm"]);
            camp.Open_To_Month = Convert.ToInt32(rdr["open_to_mm"]);
            camp.Daily_Fee = Convert.ToDecimal(rdr["daily_fee"]);
            return camp;
        }
        
    }
}
