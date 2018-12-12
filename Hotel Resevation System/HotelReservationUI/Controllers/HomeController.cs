using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelReservationUI.Models;
using HotelBookingFactory;
using System.ComponentModel.DataAnnotations;
using HotelReservationSystemModels;
using Microsoft.AspNetCore.Http;

namespace HotelReservationUI.Controllers
{
    public class Search
    { 
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public List<BookingSearchResult> Results { get; set; }
    }

    public class HomeController : BaseController
    {
        private readonly Booking _b;

        public HomeController(Booking b)
        {
            _b = b;
        }

        public IActionResult Index()
        {
            return View();
        }

        // POST: Search/rooms
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search([Bind("StartDate,EndDate")] Search request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    request.Results = _b.SearchAvialableRooms(request.StartDate, request.EndDate);
                    TempData["StartDate"] = request.StartDate;
                    TempData["EndDate"] = request.EndDate;
                }
                catch(BookingException ex)
                {
                    ModelState.AddModelError(ex.Source, ex.Message);
                }
            }
            return View(nameof(Index), request);
        }

        public IActionResult BookRoom(string type)
        {
            
            BookingDetails b = new BookingDetails();
            b.StartDate = Convert.ToDateTime(TempData["StartDate"]);
            b.EndDate = Convert.ToDateTime(TempData["EndDate"]);
            b.Type = (RoomType)Enum.Parse(typeof(RoomType), type);
            TempData["Type"] = type;
            return View(b);
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookRoom
            (IFormCollection formData)
        {
            BookingDetails details = new BookingDetails();
            if (ModelState.IsValid)
            {
                details.Name = Convert.ToString(formData["Name"]);
                details.EmailAddress = Convert.ToString(formData["EmailAddress"]);
                details.StartDate = Convert.ToDateTime(formData["StartDate"]);
                details.EndDate = Convert.ToDateTime(formData["EndDate"]);
                details.Type = (RoomType)Enum.Parse(typeof(RoomType), formData["Type"].ToString());
                var confirmationDetails = _b.BookRoom(details.Type, details.Name, details.EmailAddress, details.StartDate, details.EndDate);
                TempData.Clear();
                return View(nameof(Confirmation), confirmationDetails);
            }
            return View();
        }

        public IActionResult Confirmation()
        {
            return View();
        }

        // GET: Rooms/Edit/5
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
