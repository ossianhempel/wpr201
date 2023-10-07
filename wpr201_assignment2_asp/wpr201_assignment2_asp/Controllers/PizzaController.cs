using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
                return RedirectToAction("Index");
            }
            // Add this line to debug
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(pizza);
        }


        // Action som hämtar pizzorna från databasen med en GET-request
        public IActionResult Index()
        {
            var pizzas = _db.Pizzas.ToList();

            return View(pizzas);
        }
        // GET: Pizza/Edit/5
        public IActionResult Edit(int id)
        {
            var pizza = _db.Pizzas.Find(id);
            if (pizza == null)
            {
                return NotFound();
            }
            return View(pizza);
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Pizza pizza)
        {
            if (id != pizza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(pizza);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(pizza);
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var pizza = _db.Pizzas.Find(id);
            if (pizza == null)
            {
                return NotFound();
            }

            _db.Pizzas.Remove(pizza);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

    }

}
