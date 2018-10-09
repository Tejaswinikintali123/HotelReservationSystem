using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelResevationSystem
{
    class Room
    {
        
        public int ID { get; private set; }
        public string Type { get; set; }
        public double Price { get; private set; }

        public Room(int roomId,string roomType, double price)
        {
            ID = roomId;
            Type = roomType;
            this.Price = price;
            
        }
        


    }
}
