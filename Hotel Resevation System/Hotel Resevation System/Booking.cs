using HotelResevationSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelResevationSystem
{
    /// <summary>
    /// Bookings class
    /// </summary>
    public class Booking
    {
        private Dictionary<int, BookingDetails> bookingList = new Dictionary<int , BookingDetails>();
        private Dictionary<int,Room> roomList = new Dictionary<int,Room>();
        private IEnumerable<KeyValuePair<RoomType, int>> groupByRoomList;

        /// <summary>
        /// Booking class constructor
        /// </summary>
        public Booking()
        {
            
        }
        private List<Room> GetAllRooms()
        {
            return roomList.Values.ToList();
        }
        #region methods

        /// <summary>
        /// method to book a room
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public BookingDetails BookRoom(RoomType type, string name, string email, DateTime startdate, DateTime enddate)
        {

            this.ValidateParameters(email, startdate, enddate);
            var details = new BookingDetails();
            details.Type = type;
            details.ID = bookingList.Values.Count == 0 ? 1 : bookingList.Values.Max(x => x.ID) + 1;
            details.Name = name;
            details.EmailAddress = email;
            details.StartDate = startdate;
            details.EndDate = enddate;
            bookingList.Add(details.ID, details);
            return details;

            
        } 
       public BookingDetails GetBookingDetails(int id)
        {
            if (this.bookingList.ContainsKey(id) == true)
                return this.bookingList[id];
            
              
            throw new BookingException("Booking ID does not exist");
        }

        /// <summary>
        /// method to search rooms
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<BookingSearchResult> SearchAvialableRooms(DateTime startDate, DateTime endDate)
        {
            List<BookingSearchResult> results = new List<BookingSearchResult>();
            
            var groupByBookingDetails = this.bookingList.Values.Where(x =>startDate>= x.StartDate.Date && endDate<= x.EndDate.Date ).GroupBy(x => x.Type)
                 .Select(y => new KeyValuePair<RoomType, int>(y.Key, y.Count()));
            foreach (var pair in this.groupByRoomList) {
                var result = new BookingSearchResult();
                result.Type = pair.Key;
                var bookDetails = groupByBookingDetails.FirstOrDefault(x => x.Key == pair.Key);
                result.AvailableRoomCount = pair.Value - bookDetails.Value;
                result.RoomPrice = this.roomList.Values.Where(x => x.Type == pair.Key).FirstOrDefault().Price;
                results.Add(result);
            }
            return results;
        }
        /// <summary>
        /// populate rooms with respective of nooffloors & noofroomsperfloor
        /// </summary>
        /// <param name="noOfFloors"></param>
        /// <param name="noOfRoomsPerFloor"></param>
        public void PopulateRooms(int noOfFloors, int noOfRoomsPerFloor)
        {
            for (int i = 1; i <= noOfRoomsPerFloor; i++)
            {
                var price = 0;
                var type = RoomType.King;
                switch (i)
                {
                    case 1:
                    case 5:
                        price = 200;
                        type = RoomType.King;
                        break;
                    case 2:
                    case 6:
                    case 9:
                        price = 100;
                        type = RoomType.Queen;
                        break;
                    case 3:
                    case 7:
                        price = 150;
                        type = RoomType.Twin;
                        break;
                    case 4:
                    case 10:
                    case 8:
                        price = 100;
                        type = RoomType.Single;
                        break;
                }
                for (int k = 100; k <= noOfFloors * 100; k += 100)
                {
                    var room = new Room();
                    room.Price = price;
                    room.Type = type;
                    room.ID = k + i;
                    roomList.Add(room.ID, room);
                }
            }
            this.groupByRoomList = this.roomList.Values.GroupBy(x => x.Type)
                .Select(y => new KeyValuePair<RoomType, int>(y.Key, y.Count()));
        }
        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private void ValidateParameters(string email,DateTime startdate,DateTime enddate)
        {
            if (enddate<=startdate)
            {
                throw new BookingException("Error code:100","Enddate should be greater than to startdate");
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email), "Email address is required");
            }
            if (!IsValidEmail(email))
            {
                throw new FormatException("Please enter a valid email address.");
            }
           
           
           
        }
        #endregion
    }
}



