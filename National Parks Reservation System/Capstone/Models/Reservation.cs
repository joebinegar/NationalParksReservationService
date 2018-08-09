using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    class Reservation
    {
        #region Properties

        public int ReservationId { get; set; }
        public int SiteId { get; set;}
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime? CreateDate { get; set; } = null;

        #endregion

    }
}
