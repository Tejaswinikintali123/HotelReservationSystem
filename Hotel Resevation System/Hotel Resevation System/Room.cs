using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelResevationSystem
{
    public enum Roomtype
    {
        king,
        Queen,
        Twin,
        single
    }
    public class Room
    {
        public int ID { get; set; }
        public Roomtype Type { get; set; }
        public double Price { get;  set; }
        public bool IsBooked { get; set; }
    }
}
