// OrderController.cs

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wpr201_assignment2_asp.Models;
using System;
using System.Linq;

namespace wpr201_assignment2_asp.Controllers
{
    public class OrderController : Controller
    {
        private readonly PizzaDbContext _db;

        public OrderController(PizzaDbContext db)
        {
            _db = db;
        }

        public IActionResult Create(int? pizzaId)
        {
            // When a pizza ID is provided
            if (pizzaId.HasValue)
            {
                var pizza = _db.Pizzas.FirstOrDefault(p => p.Id == pizzaId.Value);
                if (pizza == null)
                {
                    return NotFound();
                }

                var order = new Order
                {
                    PizzaId = pizzaId.Value,
                    OrderDate = DateTime.Now,
                    Quantity = 1  // Set this as needed
                };

                _db.Orders.Add(order);
                _db.SaveChanges();

                return RedirectToAction("Index", "Pizza");
            }
            else // When no pizza ID is provided, show a form to create an order
            {
                ViewBag.Pizzas = _db.Pizzas.ToList();
                return View();
            }
        }

        public IActionResult Index()
        {
            ViewBag.Pizzas = _db.Pizzas.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult PlaceOrder(Order order)
        {
            _db.Orders.Add(order);
            _db.SaveChanges();
            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation()
        {
            return View();
        }

        [Authorize]
        public IActionResult ViewOrders()
        {
            var orders = _db.Orders.Include(o => o.Pizza).ToList();
            return View(orders);
        }

        [HttpPost]
        [Authorize]
        public IActionResult DeleteOrder(int id)
        {
            var order = _db.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            _db.Orders.Remove(order);
            _db.SaveChanges();
            return RedirectToAction("ViewOrders");
        }

    }
}
