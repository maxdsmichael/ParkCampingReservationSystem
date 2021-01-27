using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    class SiteSqlDAO : ISiteDAO
    {
        protected string connectionString;
        public SiteSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public IList<Site> GetAvailableSites(int campgroundId, DateTime arrivalDate, DateTime departureDate)
        {
            IList<Site> availableSites = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    Site site = null;
                    conn.Open();

                    string sql = @"SELECT TOP 5 * FROM site
                                            WHERE site.campground_id = @campgroundId
                                            AND site.site_id NOT IN(SELECT site_id
                                            FROM reservation
                                            WHERE from_date >= @departureDate AND to_date <= @arrivalDate);";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@campgroundId", campgroundId);
                    cmd.Parameters.AddWithValue("@departureDate", departureDate);
                    cmd.Parameters.AddWithValue("@arrivalDate", arrivalDate);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        site = RowToObject(rdr);
                        availableSites.Add(site);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Sorry an error occured {ex.Message}");
            }
            return availableSites;
        }
        public Site GetSiteById(int siteId)
        {
            Site site= null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "Select * from site where site_id = @siteId";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@siteId", siteId);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        site = RowToObject(rdr);
                    }

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Sorry there was an Error. Message {ex.Message}");
            }
            return site;
        }

        private static Site RowToObject(SqlDataReader rdr)
        {
            Site site = new Site();
            site.Site_ID = Convert.ToInt32(rdr["site_id"]);
            site.Campground_ID = Convert.ToInt32(rdr["campground_id"]);
            site.Site_Number = Convert.ToInt32(rdr["site_number"]);
            site.Max_Occupancy = Convert.ToInt32(rdr["max_occupancy"]);
            site.Accessible = Convert.ToBoolean(rdr["accessible"]);
            site.Max_Rv_Length = Convert.ToInt32(rdr["max_rv_length"]);
            site.Utilities = Convert.ToBoolean(rdr["utilities"]);
            return site;
        }
    }
}
