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

            //101, 105,110 -King
            //102, 106, 109 - queen
            Console.WriteLine("Enter No Of Floors: ");
            int noOfFloors = Convert.ToInt32(Console.ReadLine());
            List<Room> roomList = new List<Room>();
            for(int i=1; i <= 10; i++)
            {
                var price = 0;
                var type = "";
                switch(i)
                {
                    case 1:
                    case 5:
                    case 10:
                        price = 200;
                        type = "King";
                        break;
                    case 2:
                    case 6:
                    case 9:
                        price = 100;
                        type = "Queen";
                        break;
                    case 3:
                    case 7:
                    case 8:
                        price = 150;
                        type = "Double Queen";
                        break;
                }
                for (int k = 100; k <= noOfFloors * 100; k += 100)
                {
                    var room = new Room(k + i, type, price);
                    roomList.Add(room);
                }
            }
            Console.WriteLine($"totalrooms:{roomList.Count}");
          
            foreach (var room in sortedRooms)
            {
                Console.WriteLine($"roomid:{room.ID},roomtype:{room.Type},price:{room.Price}");
            }
            
            Console.ReadLine();
        }
    }
}
