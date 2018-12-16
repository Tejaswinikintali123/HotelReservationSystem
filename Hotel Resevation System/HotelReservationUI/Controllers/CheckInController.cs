using HotelBookingFactory;
using HotelReservationSystemModels;
using HotelReservationUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationUI.Controllers
{
    public class CheckInController : BaseController
    {
        private readonly Booking _b;

        public CheckInController(Booking b)
        {
            _b = b;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchById([Bind("BookingId")] CheckInModel checkInModel)
        {
            try
            {
                checkInModel.Rooms = _b.GetAvailableRoomsForCheckin(checkInModel.BookingId);
                return View(nameof(Index), checkInModel);
            }
            catch (BookingException ex)
            {
                ModelState.AddModelError(ex.Source, ex.Message);
            }
            return View(nameof(Index), checkInModel);
        }



        public IActionResult CheckInRoom(string bookingId, string roomId)
        {
            var model = new CheckInModel();
            model.BookingId = Convert.ToInt32(bookingId);
            model.Rooms = null;
            model.RoomId = Convert.ToInt32(roomId);

            model.RoomNo = _b.CheckIn(model.BookingId, model.RoomId);
            return View(nameof(Index), model);
        }
    }
}
