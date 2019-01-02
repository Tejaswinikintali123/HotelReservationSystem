using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public RoomType Type { get; set; }
        public BookingStatus Status { get; set; }
        #endregion
    }
}
