using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelResevationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var b = new Booking();
            b.PopulateRooms(3, 5);
            while(true)
            {
                var avRooms = b.SearchAvialableRooms(DateTime.Now.AddDays(1).Date, DateTime.Now.AddDays(2).Date);
                foreach (var result in avRooms)
                {
                    Console.WriteLine("{0} - {1} - {2}", result.Type, result.AvailableRoomCount, result.RoomPrice);
                }
                Console.Write("Enter Room Type : ");
                var roomType = (RoomType)Enum.Parse(typeof(RoomType), Console.ReadLine());
                var kingRoomData = avRooms.Where(x => x.Type == roomType).FirstOrDefault();
                if (kingRoomData.AvailableRoomCount > 0)
                {
                    var booking = b.BookRoom(roomType, "Pradeep", "kvlnpradeep@gmail.com", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
                    Console.WriteLine("Booking Id: {0}", booking.ID);
                    var details = b.GetBookingDetails(booking.ID);
                    Console.WriteLine("First Name: {0}", details.Name);
                    Console.WriteLine("Start Date: {0}", details.StartDate.ToShortDateString());
                    Console.WriteLine("End Date: {0}", details.EndDate.ToShortDateString());
                    Console.WriteLine("Room Type: {0}", details.Type);
                } else
                {
                    Console.WriteLine("No rooms available for this type " + roomType);
                }
            }
            
        }

    }
}