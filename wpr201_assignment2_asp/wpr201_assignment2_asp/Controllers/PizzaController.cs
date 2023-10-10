using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using wpr201_assignment2_asp.Models;
using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace wpr201_assignment2_asp.Controllers
{
    public class PizzaController : Controller
    {
        // Instanser av databaskontext och webbhost
        private readonly PizzaDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        // Konstruktor för PizzaController
        public PizzaController(PizzaDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        // Visa vyn för att skapa en pizza (kräver inloggning)
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // Visa alla pizzor
        public IActionResult Index()
        {
            var pizzas = _db.Pizzas.ToList();
            return View(pizzas);
        }

        // Visa vyn för att redigera pizza
        public IActionResult Edit(int id)
        {
            var pizza = _db.Pizzas.Find(id);
            if (pizza == null)
            {
                return NotFound();
            }
            return View(pizza);
        }

        // POST-metod för att skapa en pizza
        [HttpPost]
        public async Task<IActionResult> Create(Pizza pizza)
        {
            ModelState.Remove("UploadedImage"); // Kräv ej bild

            if (ModelState.IsValid)
            {
                if (pizza.UploadedImage != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await pizza.UploadedImage.CopyToAsync(memoryStream);
                        pizza.Image = memoryStream.ToArray();
                    }
                }

                _db.Pizzas.Add(pizza);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(pizza);
        }

        // POST-metod för att redigera en pizza
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pizza newPizza)
        {
            ModelState.Remove("UploadedImage"); // Kräv ej bild

            // Kontrollera att IDt finns
            if (id != newPizza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Hämta den existerande pizzan som matchar det angivna IDt
                    // AsNoTracking() gör att Entity Framework inte spårar objektet för ändringar (snabbare, mindre 
                    var existingPizza = _db.Pizzas.AsNoTracking().FirstOrDefault(p => p.Id == id);
                    if (existingPizza == null)
                    {
                        return NotFound();
                    }

                    // Använd existerande bild fortsatt
                    newPizza.Image = existingPizza.Image;

                    // Uppdatera pizzan i databasen
                    _db.Update(newPizza);

                    await _db.SaveChangesAsync();
                }

                // Vid fel, kolla om pizzan finns i databasen
                catch (DbUpdateConcurrencyException)
                {
                    if (!_db.Pizzas.Any(e => e.Id == newPizza.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Dirigerar användaren tillbaka till menyn
                return RedirectToAction(nameof(Index));
            }

            // Fortsätt redigera
            return View(newPizza);
        }


        // POST-metod för att radera en pizza
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
