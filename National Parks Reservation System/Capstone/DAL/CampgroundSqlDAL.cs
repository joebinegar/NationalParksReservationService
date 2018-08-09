using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    class CampgroundSqlDAL
    {
        #region Member Variables

        private string _connectionString;

        #endregion

        #region Constructor

        public CampgroundSqlDAL(string databaseconnectionString)
        {
            _connectionString = databaseconnectionString;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Campground> GetAllCampgrounds(Park park)
        {
            List<Campground> allCampgrounds = new List<Campground>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                const string sqlAllCampgrounds = "Select Top(5) campground.campground_id, campground.park_id, name, campground.open_from_mm, campground.open_to_mm, campground.daily_fee " +
                                                "From campground where park_id = @park_id;";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlAllCampgrounds;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@park_id", park.ParkId);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Campground campground = PopulateCampgroundFromReader(reader);
                    allCampgrounds.Add(campground);
                }
            }
            return allCampgrounds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns> Creates and returns a campground object. Gets called by GetAllCampgrounds() above to read in a campground and add it to a list. </returns>
        public Campground PopulateCampgroundFromReader(SqlDataReader reader)
        {
            Campground campground = new Campground();
            campground.CampgroundId = Convert.ToInt32(reader["campground_id"]);
            campground.ParkId = Convert.ToInt32(reader["park_id"]);
            campground.Name = Convert.ToString(reader["name"]);
            campground.OpenFrom = Convert.ToInt32(reader["open_from_mm"]);
            campground.OpenTo = Convert.ToInt32(reader["open_to_mm"]);
            campground.DailyFee = Convert.ToInt32(reader["daily_fee"]);

            return campground;
        }

        #endregion


    }
}
