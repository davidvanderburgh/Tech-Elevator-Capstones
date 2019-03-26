using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL;
using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
        private readonly IParkSqlDAL parkSqlDAL;
        private readonly ISurveyResultSqlDAL surveyResultSqlDAL;

        public SurveyController(IParkSqlDAL parkSqlDAL, ISurveyResultSqlDAL surveyResultSqlDAL)
        {
            this.parkSqlDAL = parkSqlDAL;
            this.surveyResultSqlDAL = surveyResultSqlDAL;
        }

        [HttpGet]
        public IActionResult Index()
        {
            SurveyViewModel model = new SurveyViewModel();
            model.AllSelectableParks = parkSqlDAL.GetAllParksSelectList();
            model.SurveyInProcess = new Survey();

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(SurveyViewModel model)
        {
            model.AllSelectableParks = parkSqlDAL.GetAllParksSelectList();

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            surveyResultSqlDAL.AddSurveyResult(model.SurveyInProcess);

            return RedirectToAction("Results", "Survey");
        }


        public IActionResult Results()
        {
            return View(surveyResultSqlDAL.GetVotesPerPark());
        }
    }
}