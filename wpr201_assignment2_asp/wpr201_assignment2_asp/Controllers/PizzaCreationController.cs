using Microsoft.AspNetCore.Mvc;
using wpr201_assignment2_asp.Models;
using System.Linq;

namespace wpr201_assignment2_asp.Controllers
{
    public class PizzaCreationController : Controller
    {
        private readonly PizzaDbContext _db;

        public PizzaCreationController(PizzaDbContext db)
        {
            _db = db;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                _db.Pizzas.Add(pizza);
                _db.SaveChanges();
                return RedirectToAction("Index", "Pizza");
            }
            return View(pizza);
        }
    }
}
