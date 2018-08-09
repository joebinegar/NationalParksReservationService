  using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    class CampSiteSqlDAL
    {
        #region Member Variables

        private string _connectionString;

        #endregion

        #region Constructor

        public CampSiteSqlDAL(string databaseconnectionString)
        {
            _connectionString = databaseconnectionString;
        }

        #endregion

        #region Methods

       public List<Campsite> GetCampgroundSites(int campgroundId, DateTime arriveDate, DateTime departDate)
        {
            List<Campsite> output = new List<Campsite>();
            int arriveMonth = arriveDate.Month;
            int departMonth = departDate.Month;

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                const string sqlGetCampgroundSites = "select top 5 site.* from site " +
                                                     "join campground on site.campground_id = campground.campground_id " +
                                                     "where site.campground_id = @campground_id  " +
                                                     "and site_id not in (select site_id from reservation " +
                                                     "where @from_date <= to_date " +
                                                     "and @to_date >= from_date);";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlGetCampgroundSites;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@campground_id", campgroundId);
                cmd.Parameters.AddWithValue("@from_date", arriveDate.ToShortDateString());
                cmd.Parameters.AddWithValue("@to_date", departDate.ToShortDateString());

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Campsite site = PopulateSitesFromReader(reader);
                    output.Add(site);
                }
            }

            return output;
        }

        public Campsite PopulateSitesFromReader(SqlDataReader reader)
        {
            Campsite item = new Campsite();

            item.SiteId = Convert.ToInt32(reader["site_id"]);
            item.CampgroundId = Convert.ToInt32(reader["campground_id"]);
            item.SiteNumber= Convert.ToInt32(reader["site_number"]);
            item.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
            item.IsAccessible = Convert.ToBoolean(reader["accessible"]);
            item.MaxRvLength= Convert.ToInt32(reader["max_rv_length"]);
            item.Utilities = Convert.ToBoolean(reader["utilities"]);

            return item;
        }

        #endregion
    }
}
