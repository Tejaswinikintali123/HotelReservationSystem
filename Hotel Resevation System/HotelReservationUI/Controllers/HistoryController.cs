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
using System.ComponentModel.DataAnnotations;

namespace HotelReservationUI.Controllers
{
    public class SearchDate
    {
       
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        public List<BookingDetails> Results { get; set; }
    }
    public class HistoryController : BaseController
    {

        private readonly Booking _b;

        public HistoryController(Booking b)
        {
            _b = b;
        }
        public IActionResult SearchDate([Bind("Date")] SearchDate request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    request.Results = _b.GetHistory(request.StartDate);
                    TempData["Date"] = request.StartDate;
                   
                }
                catch (BookingException ex)
                {
                    ModelState.AddModelError(ex.Source, ex.Message);
                }
            }
            return View(nameof(Index), request);
        }

        public IActionResult Index()
        {
            var history = _b.GetHistory();
            return View(history);
        }
    }
}