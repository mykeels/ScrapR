using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScrapR.Web.Api.Models;


namespace ScrapR.Web.Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult TrvStart()
        {
            return Json(Response<List<ScrapR.Models.TrvStart.Itinerary>>.Create(ScrapR.Models.TrvStart.Scrapper.Create().GetItineraries(), true));
        }
    }
}
