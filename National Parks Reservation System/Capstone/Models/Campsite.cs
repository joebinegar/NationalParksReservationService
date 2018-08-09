using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    class Campsite
    {
        public int SiteId { get; set; }
        public int CampgroundId { get; set; }
        public int SiteNumber { get; set; }
        public int MaxOccupancy { get; set; }
        public bool IsAccessible { get; set; }
        public int MaxRvLength { get; set; }
        public bool Utilities { get; set; }
        //add a campground object?

        public string IsAccessibleStr
        {
            get
            {
                return IsAccessible ? "Yes" : "No";
            }
        }

        public string MaxRvLengthStr
        {
            get
            {
                return MaxRvLength > 0 ? MaxRvLength.ToString(): "N/A";
            }
        }

        public string UtilitiesStr
        {
            get
            {
                return Utilities ? "Yes" : "No";
            }
        }
    }
}
