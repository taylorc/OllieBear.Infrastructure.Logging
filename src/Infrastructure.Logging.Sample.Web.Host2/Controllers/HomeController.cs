using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Logging.Sample.Web.Host.Models;

namespace Infrastructure.Logging.Sample.Web.Host.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILog _logger;

        public HomeController(ILog logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.Info("Message Test Inherited Logger");

            return View();
        }

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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Throw()
        {
            throw new Exception("Limbo",
                new Exception("Lust",
                    new Exception("Gluttony",
                        new Exception("Greed", 
                            new Exception("Wrath",
                                new Exception("Heresy", 
                                    new Exception("Violence",
                                        new Exception("Fraud",
                                            new Exception("Treachery")))))))));
        }

    }
}
