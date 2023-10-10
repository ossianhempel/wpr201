using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using wpr201_assignment2_asp.Models;

namespace wpr201_assignment2_asp.Controllers
{
    // Definierar HomeController-klassen, ärver från Controller-klassen
    public class HomeController : Controller
    {
        // Skapa instans av ILogger för att aktivera loggning
        private readonly ILogger<HomeController> _logger;

        // Konstruktor för HomeController
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Metod för startsidan
        public IActionResult Index()
        {
            return View(); 
        }

        // Metod för att hantera fel
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
