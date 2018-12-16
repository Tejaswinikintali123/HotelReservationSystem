using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystemModels
{
   public class BookingException:Exception
    {
        public BookingException():base("end date should be greater than start date")
        {

        }
        public BookingException(string errorcode,string message):base(string.Format("{0}-{1}", errorcode,message))
        {

        }
        public BookingException(string message):base(message)
        {

        }
    }
   
}
