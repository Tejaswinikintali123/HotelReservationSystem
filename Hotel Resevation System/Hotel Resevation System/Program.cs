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
            foreach (var r in b.GetAvailableRooms(Roomtype.king))
            {
                Console.WriteLine("{0} - {1} - {2}", r.ID, r.Type, r.Price);
            }
            int roomNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(b.BookRoom(roomNo));
            roomNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(b.BookRoom(roomNo));
            roomNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(b.BookRoom(roomNo));
            foreach (var r in b.GetAvailableRooms(Roomtype.king))
            {
                Console.WriteLine("{0} - {1} - {2}", r.ID, r.Type, r.Price);
            }
        }

    }
}