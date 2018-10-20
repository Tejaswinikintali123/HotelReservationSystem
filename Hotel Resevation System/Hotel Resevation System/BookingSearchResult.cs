using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelResevationSystem
{/// <summary>
/// Created BookingSearchClass  
/// </summary>
    public class BookingSearchResult
    {
        #region properties
        public int AvailableRoomCount { get; set; }
        public double RoomPrice { get; set; }
        public RoomType Type { get; set; }
        #endregion
    }
}
