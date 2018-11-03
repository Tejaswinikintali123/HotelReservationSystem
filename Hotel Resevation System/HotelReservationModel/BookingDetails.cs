using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystemModels
{ /// <summary>
///  created BookingDetails class
/// created properties for Customer BookingDetails
/// <summary>
   public class BookingDetails
    {
        #region properties
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RoomType Type { get; set; }
        
        #endregion
    }
}
