using Microsoft.AspNetCore.Mvc;
using wpr201_assignment2_asp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace wpr201_assignment2_asp.Controllers
{
    public class PizzaManagementController : Controller
    {
        private readonly PizzaDbContext _db;

        public PizzaManagementController(PizzaDbContext db)
        {
            _db = db;
        }

        [Authorize]
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
