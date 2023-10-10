
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
        // Skapa instans av databaskontext
        private readonly PizzaDbContext _db;

        // Konstruktor för OrderController 
        public OrderController(PizzaDbContext db)
        {
            _db = db;
        }

        // Metod för att skapa beställning
        public IActionResult Create(int? pizzaId)
        {
            if (pizzaId.HasValue) // Om pizza-id redan finns
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
                    Quantity = 1  
                };

                _db.Orders.Add(order);
                _db.SaveChanges();

                return RedirectToAction("Index", "Pizza");
            }
            else // Om ingen pizza-ID tillhandahålls, visa ett formulär
            {
                ViewBag.Pizzas = _db.Pizzas.ToList();
                return View();
            }
        }

        // POST-metod för att beställa
        [HttpPost]
        public IActionResult PlaceOrder(Order order)
        {
            order.OrderDate = DateTime.Now; // Sätter nuvarande tid som beställningstid
            _db.Orders.Add(order);
            _db.SaveChanges();
            return RedirectToAction("Confirmation");
        }

        // Metod för att visa en bekräftelsesida
        public IActionResult Confirmation()
        {
            return View();
        }

        // Visa alla beställningar om inloggad
        [Authorize]
        public IActionResult ViewOrders()
        {
            var orders = _db.Orders.Include(o => o.Pizza).ToList();
            return View(orders);
        }

        // POST-metod för att ta bort beställning (kräver inloggning)
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
