using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Demostracion.Models;
using Microsoft.AspNetCore.Http;

namespace Demostracion.Controllers
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
            /*
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Nombre")))
            {
                HttpContext.Session.SetString("Nombre", "The Doctor");
                HttpContext.Session.SetInt32("Edad", 773);
            }

            HttpContext.Session.SetInt32("Edad", HttpContext.Session.GetInt32("Edad").Value + 1);
            var name = HttpContext.Session.GetString("Nombre");
            var age = HttpContext.Session.GetInt32("Edad");
            ViewBag.name = name;
            ViewBag.age = age;*/
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
