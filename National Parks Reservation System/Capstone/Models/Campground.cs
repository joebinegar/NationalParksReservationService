using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    class Campground
    {

        #region Properties

        private Dictionary<int, string> _months = new Dictionary<int, string>
        {
            {1, "January" },
            {2, "February" },
            {3, "March" },
            {4, "April" },
            {5, "May" },
            {6, "June" },
            {7, "July" },
            {8, "August" },
            {9, "September" },
            {10, "October" },
            {11, "November" },
            {12, "December" },

        };
        public int CampgroundId { get; set; }
        public int ParkId { get; set; }
        public string Name { get; set; }
        public int OpenFrom { get; set; }
        public int OpenTo { get; set; }
        public decimal DailyFee { get; set; }

        public string OpenFromMonthStr
        {
            get
            {
                return _months[OpenFrom];
            }
        }

        public string OpenToMonthStr
        {
            get
            {
                return _months[OpenTo];
            }
        }

        
        #endregion
    }
}
