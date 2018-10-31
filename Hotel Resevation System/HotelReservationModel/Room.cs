using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystemModels
{
    public class Room
    {
        #region properties
        public int ID { get; set; }
        public RoomType Type { get; set; }
        public double Price { get;  set; }
        #endregion
    }
}
