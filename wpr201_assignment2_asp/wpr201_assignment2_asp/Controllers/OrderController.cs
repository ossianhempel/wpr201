using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wpr201_assignment2_asp.Models;
using System.Linq;

namespace wpr201_assignment2_asp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly PizzaDbContext _db;

        public OrderController(PizzaDbContext db)
        {
            _db = db;
        }

        public IActionResult Create(int pizzaId)
        {
            var pizza = _db.Pizzas.FirstOrDefault(p => p.Id == pizzaId);
            if (pizza == null)
            {
                return NotFound();
            }

            var order = new Order
            {
                PizzaId = pizzaId,
                OrderDate = DateTime.Now,
                Quantity = 1  // Set this as needed
            };

            _db.Orders.Add(order);
            _db.SaveChanges();

            return RedirectToAction("Index", "Pizza");
        }
    }
}

