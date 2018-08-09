using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    class ParkSqlDAL
    {
        #region Member Variables

        private string _connectionString;

        #endregion

        #region Constructor

        public ParkSqlDAL(string databaseconnectionString)
        {
            _connectionString = databaseconnectionString;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Queries and reads in a park object from the database, add each park to a list and returns a list of all parks.
        /// </summary>
        /// <returns> List<Park>allParks </Park></returns>
        public List<Park> GetAllParks()
        {
            List<Park> allParks = new List<Park>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                const string sqlAllParks = "Select park.park_id, park.name, park.location, " +
                                           "park.establish_date, park.area, park.visitors, park.description " +
                                           "From park order by park.name;";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlAllParks;
                cmd.Connection = connection;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Park park = PopulateParkFromReader(reader);
                    allParks.Add(park);
                }
            }
            return allParks;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns> Creates and returns a park object. Gets called by GetAllParks() above to read in a park and add it to a list. </returns>
        public Park PopulateParkFromReader(SqlDataReader reader)
        {
            Park park = new Park();
            park.ParkId = Convert.ToInt32(reader["park_id"]);
            park.Name = Convert.ToString(reader["name"]);
            park.Location = Convert.ToString(reader["location"]);
            park.Establish_Date = Convert.ToDateTime(reader["establish_date"]);
            park.Area = Convert.ToInt32(reader["area"]);
            park.Visitors = Convert.ToInt32(reader["visitors"]);
            park.Description = Convert.ToString(reader["description"]);

            return park;
        }

        #endregion
    }
}
