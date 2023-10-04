using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wpr201_assignment2_asp.Models;

namespace wpr201_assignment2_asp.Controllers
{
    // PizzaController ärver från "grundklassen" Controller från ASP.NET MVC
    public class PizzaController : Controller
    {
        // Readonly-fält från PizzaDbContext då vi endast vill läsa data 
        private readonly PizzaDbContext _db;

        // Konstruktorns får PizzaDbContexts funktion genom dependency injection
        public PizzaController(PizzaDbContext db)
        {
            _db = db; // Initialiserar databasen
        }

        // Action som hämtar pizzorna från databasen med en GET-request
        public IActionResult Index()
        {
            var pizzas = _db.Pizzas.ToList();

            return View(pizzas);
        }
    }
}
