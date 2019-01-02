using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelBookingDBLayer;
using HotelReservationSystemModels;
using HotelBookingFactory;

namespace HotelReservationUI.Controllers
{
    public class BookingDetailsController : BaseController
    {
        private readonly Booking _b;

        public BookingDetailsController(Booking b)
        {
            _b = b;
        }

        // GET: BookingDetails
        public IActionResult Index()
        {
            var result = _b.GetBookingDetails(HttpContext.User.Identity.Name);
            return View(result);
        }

        public IActionResult Cancel(string id)
        {
            _b.CancelBooking(Convert.ToInt32(id));
            var result = _b.GetBookingDetails(HttpContext.User.Identity.Name);
            return View(nameof(Index), result);
        }

    }
}
