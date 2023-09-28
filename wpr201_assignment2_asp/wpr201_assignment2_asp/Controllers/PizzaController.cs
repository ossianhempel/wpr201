using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wpr201_assignment2_asp.Models;


namespace wpr201_assignment2_asp.Controllers
{
    public class PizzaController : Controller
    {
        private readonly PizzaDbContext _db; // Use readonly field
        public PizzaController(PizzaDbContext db) // Constructor injection
        {
            _db = db;
        }
        // GET: Pizza
        public IActionResult Index()
        {
            var pizzas = _db.Pizzas.ToList();
            return View(pizzas);
        }
    }
}