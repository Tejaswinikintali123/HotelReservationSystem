using HotelReservationSystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationUI.Models
{
    public class CheckInModel
    {
        public int RoomNo { get; set; }
        public int RoomId { get; set; }
        public int BookingId { get; set; }

        public List<Room> Rooms { get; set; }
        public List<BookingDetails> Bookings { get; set; }
    }
}
