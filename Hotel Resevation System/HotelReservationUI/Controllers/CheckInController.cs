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
            var model = new CheckInModel();
            model.Bookings = _b.GetTodayBookings();
            return View(model);
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
        public IActionResult Search(string bookingId)
        {
            var model = new CheckInModel();
            try
            {
                model.BookingId = Convert.ToInt32(bookingId);
                model.Rooms = _b.GetAvailableRoomsForCheckin(model.BookingId);
                return View(nameof(Index), model);
            }
            catch (BookingException ex)
            {
                ModelState.AddModelError(ex.Source, ex.Message);
            }
            return View(nameof(Index), model);
        }



        public IActionResult CheckInRoom(string bookingId, string roomId)
        {
            var model = new CheckInModel();
            model.BookingId = Convert.ToInt32(bookingId);
            
            model.RoomId = Convert.ToInt32(roomId);
            try
            {
                model.RoomNo = _b.CheckIn(model.BookingId, model.RoomId);
                model.Rooms = null;
            } 
            catch(BookingException ex)
            {
                ModelState.AddModelError(ex.Source, ex.Message);
            }
            
            return View(nameof(Index), model);
        }
    }
}
