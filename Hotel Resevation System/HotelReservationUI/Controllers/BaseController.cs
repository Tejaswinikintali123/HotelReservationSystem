using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationUI.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewData["IsAdmin"] = HttpContext.User.Identity.Name == "admin@test.com";
            ViewData["IsLoggedInUser"] = !string.IsNullOrEmpty(HttpContext.User.Identity.Name);
        }
    }
}
