using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelReservationSystemModels
{
    public class CheckIn
    {
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        [ForeignKey("BookingDetails")]
        public int BookingId { get; set; }
        public virtual BookingDetails BookingDetails { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
       
    }
}
