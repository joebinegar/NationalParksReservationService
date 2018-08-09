using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAL
{
    class ReservationSqlDAL
    {
        #region Member Variables

        private string _connectionString;

        #endregion

        #region Constructor

        public ReservationSqlDAL(string databaseconnectionString)
        {
            _connectionString = databaseconnectionString;
        }

        #endregion

        #region Methods

        public List<Reservation> GetAllReservations(Campsite site)
        {
            List<Reservation> allReservations = new List<Reservation>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                const string sqlAllReservations = "Select * from reservation where site_id = @site_id;";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlAllReservations;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@site_id", site.SiteId);
                
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Reservation reservation = GetReservationFromReader(reader);
                    allReservations.Add(reservation);
                }
            }
            return allReservations;
        }

        public int BookReservation(int siteId, DateTime arriveDate, DateTime departDate, string resName)
        {
            DateTime timeStamp = DateTime.Now;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                const string sqlMakeReservation = "Insert Into reservation (site_id, name, from_date, to_date, create_date)" +
                                                   "Values(@site_id, @name, @from_date, @to_date, @create_date);" +
                                                   "Select Cast(scope_identity() AS int);";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlMakeReservation;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@site_id", siteId);
                cmd.Parameters.AddWithValue("@name", resName);
                cmd.Parameters.AddWithValue("@from_date", arriveDate);
                cmd.Parameters.AddWithValue("@to_date", departDate);
                cmd.Parameters.AddWithValue("@create_date", timeStamp);

                int newResId = (int)cmd.ExecuteScalar();

                return newResId;
            }


                
        }

        public Reservation GetReservationFromReader (SqlDataReader reader)
        {
            Reservation item = new Reservation();

            item.ReservationId = Convert.ToInt32(reader["reservation_id"]);
            item.SiteId = Convert.ToInt32(reader["site_id"]);
            item.Name = Convert.ToString(reader["name"]);
            item.FromDate = Convert.ToDateTime(reader["from_date"]);
            item.ToDate = Convert.ToDateTime(reader["to_date"]);

            if (!reader.IsDBNull(reader.GetOrdinal("create_date")))
            {
                item.CreateDate = Convert.ToDateTime(reader["create_date"]);
            }

            return item;
        }

        #endregion
    }
}
