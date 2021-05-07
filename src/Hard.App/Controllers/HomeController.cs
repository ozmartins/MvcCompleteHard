using Hard.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hard.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var errorModel = new ErrorViewModel() {  Code = id };

            if (id == 500)
            {
                errorModel.Title = "Internal server error";
                errorModel.Message = "An unexpected error occurred. Please, contact our support team.";
            }
            else if (id == 404)
            {
                errorModel.Title = "Not found";
                errorModel.Message = "The page you have requested does not exists.";
            }
            else if (id == 403)
            {
                errorModel.Title = "Forbiden";
                errorModel.Message = "You haven't permission to access this resource.";
            }
            else
            {
                errorModel.Code = id;
                errorModel.Title = "Unknown error.";
                errorModel.Message = "Unknown error.";
            }

            return View(errorModel);
        }
    }
}
