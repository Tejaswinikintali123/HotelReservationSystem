using HotelBookingFactory;
using HotelReservationUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationUI.Controllers
{
    public class CheckOutController : BaseController
    {
        private readonly Booking _b;

        public CheckOutController(Booking b)
        {
            _b = b;
        }

        public IActionResult Index()
        {
            var checkIns=_b.GetAvailableRoomsForCheckOut();
            return View(checkIns);
        }
        public IActionResult CheckOut(string id)
        {
          _b.CheckOut(Convert.ToInt32(id));
            var checkIns = _b.GetAvailableRoomsForCheckOut();
            return View(nameof(Index), checkIns);
        }
    }
}
