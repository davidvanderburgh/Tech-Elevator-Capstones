using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IParkSqlDAL parkSqlDAL;
        private readonly IWeatherSqlDAL weatherSqlDAL;

        public HomeController(IParkSqlDAL parkSqlDAL, IWeatherSqlDAL weatherSqlDAL)
        {
            this.parkSqlDAL = parkSqlDAL;
            this.weatherSqlDAL = weatherSqlDAL;
        }

        public IActionResult Index()
        {
            return View(parkSqlDAL.GetAllParks());
        }

        public IActionResult Detail(string id)
        {
            ParkDetailViewModel model = GetParkDetailViewModelFromParkID(id);

            return View(model);
        }

        public IActionResult ToggleTemp(string id)
        {
            ParkDetailViewModel model = GetParkDetailViewModelFromParkID(id);

            if (model.InFahrenheit)
            {
                //set session variable to false;
                HttpContext.Session.SetInt32("InFahrenheit", 0);
            }
            else
            {
                //set session variable to true;
                HttpContext.Session.SetInt32("InFahrenheit", 1);
            }
            return RedirectToAction("Detail", "Home", new { id = model.Park.ParkCode }, null);
        }

        public ParkDetailViewModel GetParkDetailViewModelFromParkID(string id)
        {
            ParkDetailViewModel model = new ParkDetailViewModel();

            Park park = parkSqlDAL.GetPark(id);
            List<DailyWeather> dailyWeather = weatherSqlDAL.GetWeatherForecast(id);

            model.Park = park;
            model.DailyWeather = dailyWeather;

            int? sessionInFahrenheit = HttpContext.Session.GetInt32("InFahrenheit");
            if (sessionInFahrenheit == null)
            {
                HttpContext.Session.SetInt32("InFahrenheit", 1);
                sessionInFahrenheit = 1;
            }

            if (sessionInFahrenheit == 0)
            {
                model.InFahrenheit = false;
            }
            else
            {
                model.InFahrenheit = true;
            }

            return model;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
