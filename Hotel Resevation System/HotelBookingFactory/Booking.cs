using HotelBookingDBLayer;
using HotelReservationSystemModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingFactory
{
    /// <summary>
    /// Bookings class
    /// </summary>
    public class Booking
    {
       
        private BookingDbContext db = new BookingDbContext();
        private IEnumerable<KeyValuePair<RoomType, int>> groupByRoomList;

        /// <summary>
        /// Booking class constructor
        /// </summary>
        public Booking()
        {
            FetchRoomList();
        }

        private void FetchRoomList()
        {
            this.groupByRoomList = this.db.Rooms.GroupBy(x => x.Type)
                .Select(y => new KeyValuePair<RoomType, int>(y.Key, y.Count()));
        }

        public List<Room> GetAllRooms()
        {
            return db.Rooms.ToList();
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
  
            details.Name = name;
            details.EmailAddress = email;
            details.StartDate = startdate;
            details.EndDate = enddate;
            var detailsResult= db.BookingDetailsofRoom.Add(details);
            db.SaveChanges();
            return detailsResult.Entity;
        } 

        public List<CheckIn> GetAvailableRoomsForCheckOut()
        {
           return db.CheckIn.Where(x => x.CheckOutTime == null).Include(x=>x.Room).Include(x=>x.BookingDetails).ToList();
            
        }
        public List<Room> GetAvailableRoomsForCheckin(int bookingId)
        {
            var existingCheckin = db.CheckIn.Where(x => x.BookingId == bookingId).FirstOrDefault();
            if (existingCheckin != null)
                throw new BookingException("Already CheckedIn");
            var rooms = new List<Room>();
            var details = this.GetBookingDetails(bookingId);

            var allRooms = this.db.Rooms.Where(x => x.Type == details.Type);
            
            foreach (var room in allRooms)
            {
                var checkInRoomCount = this.db.CheckIn.Where(x => x.RoomId == room.ID && x.CheckOutTime == null).Count();
                if (checkInRoomCount == 0)
                {
                    rooms.Add(room);
                }
            }
            return rooms;
        }
        public List<BookingDetails> GetTodayBookings()
        {
            var bookings = new List<BookingDetails>();
            bookings = this.db.BookingDetailsofRoom.Where(x => x.StartDate.Date == DateTime.Now.Date && x.Status==BookingStatus.Active).ToList();
            return bookings;
        }
        public List<BookingDetails> GetHistory()
        {
            var bookings = new List<BookingDetails>();
            bookings = this.db.BookingDetailsofRoom.Where(x =>( x.Status == BookingStatus.Cancelled) ||( x.Status == BookingStatus.Completed)).ToList();
            return bookings;
        }
        public List<BookingDetails> GetHistory(DateTime startdate, DateTime enddate)
        {
            var bookings = new List<BookingDetails>();
            bookings = this.db.BookingDetailsofRoom.Where(x => (x.Status == BookingStatus.Cancelled) || (x.Status == BookingStatus.Completed)).ToList();
            return bookings;
        }
        public List<BookingDetails> GetHistory(DateTime startdate)
        {
            var bookings = new List<BookingDetails>();
            bookings = this.db.BookingDetailsofRoom.Where(x=>(x.Status == BookingStatus.Completed)).ToList();
            return bookings;
        }

        public int CheckIn(int bookingId,int roomId)
        {
            var details = this.GetBookingDetails(bookingId);
            if (details.Status == BookingStatus.Cancelled)
            {
                throw new BookingException("Booking already Cancelled.");
            }
            var room = this.db.Rooms.Where(x => x.ID == roomId).FirstOrDefault();
            
            var checkIn = new CheckIn();
            checkIn.BookingId = bookingId;
            checkIn.RoomId = room.ID;
            checkIn.CheckInTime = DateTime.Now;
            db.CheckIn.Add(checkIn);
            details.Status = BookingStatus.CheckedIn;
            db.BookingDetailsofRoom.Update(details);
            db.SaveChanges();
            return room.RoomNo;

        }

        public int CheckIn(int bookingId)
        {
            var details = this.GetBookingDetails(bookingId);
            
            var allRooms = this.db.Rooms.Where(x => x.Type == details.Type);
            Room room = allRooms.FirstOrDefault();
            foreach(var r in allRooms)
            {
                var checkInRoomCount = this.db.CheckIn.Where(x => x.RoomId == room.ID && x.CheckOutTime == null).Count();
                if(checkInRoomCount == 0)
                {
                    room = r;
                    break;
                }
            }
            var checkIn = new CheckIn();
            checkIn.BookingId = bookingId;
            checkIn.RoomId = room.ID;
            checkIn.CheckInTime = DateTime.Now;
            db.CheckIn.Add(checkIn);
            details.Status = BookingStatus.CheckedIn;
            db.BookingDetailsofRoom.Update(details);
            db.SaveChanges();
            return room.RoomNo;

        }
        public void CheckOut(int roomNo)
        {
            var room = this.db.Rooms.Where(x => x.RoomNo == roomNo).FirstOrDefault();
            var checkin = this.db.CheckIn.Where(x => x.RoomId == room.ID && x.CheckOutTime == null).FirstOrDefault();
            checkin.CheckOutTime = DateTime.Now;
            var details = this.GetBookingDetails(checkin.BookingId);
            details.Status = BookingStatus.Completed;
            db.BookingDetailsofRoom.Update(details);
            db.SaveChanges();
        }
        public BookingDetails GetBookingDetails(int id)
        {
           var existingDetails= this.db.BookingDetailsofRoom.Where(x => x.ID == id).FirstOrDefault();
            if (existingDetails != null)
                return existingDetails;
            
              
            throw new BookingException($"Booking ID {id} does not exist");
        }

        public IEnumerable<BookingDetails> GetBookingDetails(string emailAddress)
        {
            var result = this.db.BookingDetailsofRoom.Where(x => x.EmailAddress.ToLower() == emailAddress.ToLower());
            foreach(var booking in result)
            {
                //if(booking.IsCancelled != true)
                //{
                //    var checkIn = db.CheckIn.Where(x => x.BookingId == booking.ID).FirstOrDefault();
                //    if(checkIn != null)
                //    {
                //        booking.IsCheckedIn = checkIn.CheckOutTime == null;
                //        booking.IsCheckedOut = checkIn.CheckOutTime != null;
                //    }
                //}
            }
            return result;
        }

        /// <summary>
        /// method to search rooms
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<BookingSearchResult> SearchAvialableRooms(DateTime startDate, DateTime endDate)
        {
            this.ValidateDates(startDate, endDate);
            List<BookingSearchResult> results = new List<BookingSearchResult>();
            
            var groupByBookingDetails = this.db.BookingDetailsofRoom.Where(x =>startDate>= x.StartDate.Date && endDate<= x.EndDate.Date && x.Status == BookingStatus.Active).GroupBy(x => x.Type)
                 .Select(y => new KeyValuePair<RoomType, int>(y.Key, y.Count()));
            foreach (var pair in this.groupByRoomList) {
                var result = new BookingSearchResult();
                result.Type = pair.Key;
                var bookDetails = groupByBookingDetails.FirstOrDefault(x => x.Key == pair.Key);
                result.AvailableRoomCount = pair.Value - bookDetails.Value;
                result.RoomPrice = this.db.Rooms.Where(x => x.Type == pair.Key).FirstOrDefault().Price;
                results.Add(result);
            }
            return results;
        }

        public void CancelBooking(int bookingId)
        {
            var booking = this.db.BookingDetailsofRoom.Where(x => x.ID == bookingId).FirstOrDefault();
            booking.Status = BookingStatus.Cancelled;
            db.BookingDetailsofRoom.Update(booking);
            db.SaveChanges();
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
                    room.RoomNo = k + i;
                   db.Rooms.Add(room);

                }
                db.SaveChanges();
            }
            FetchRoomList();
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
            this.ValidateDates(startdate, enddate);
        }
        private void ValidateDates(DateTime startdate, DateTime enddate)
        {
            if (enddate <= startdate)
            {
                throw new BookingException("Error code:100", "Enddate should be greater than to startdate");
            }
        }
        #endregion
    }
}



