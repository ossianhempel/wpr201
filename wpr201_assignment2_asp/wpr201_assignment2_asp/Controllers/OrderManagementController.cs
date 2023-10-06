// OrderManagementController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using wpr201_assignment2_asp.Models;
using System.Linq;

namespace wpr201_assignment2_asp.Controllers
{
    public class OrderManagementController : Controller
    {
        private readonly PizzaDbContext _db;

        public OrderManagementController(PizzaDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            ViewBag.Pizzas = _db.Pizzas.ToList();
            return View();
        }

        // This handles the GET request and shows the form to the user.
        public IActionResult Create()
        {
            ViewBag.Pizzas = _db.Pizzas.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult PlaceOrder(Order order)
        {
            // Add the order to the database
            _db.Orders.Add(order);

            // Save changes to the database
            _db.SaveChanges();

            return RedirectToAction("Confirmation"); // Redirect to Confirmation page 
        }

        public IActionResult Confirmation()
        {
            return View();
        }


        // Apply the [Authorize] attribute to this action only
        [Authorize]
        public IActionResult ViewOrders()
        {
            // Fetch all orders including their associated pizzas from the database.
            var orders = _db.Orders.Include(o => o.Pizza).ToList();
            return View(orders);
        }
    }
}
