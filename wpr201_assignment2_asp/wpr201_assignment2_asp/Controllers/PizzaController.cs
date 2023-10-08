using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using wpr201_assignment2_asp.Models;
using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace wpr201_assignment2_asp.Controllers
{
    public class PizzaController : Controller
    {
        private readonly PizzaDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PizzaController(PizzaDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // View pizzas
        public IActionResult Index()
        {
            var pizzas = _db.Pizzas.ToList();
            return View(pizzas);
        }

        public IActionResult Edit(int id)
        {
            var pizza = _db.Pizzas.Find(id);
            if (pizza == null)
            {
                return NotFound();
            }
            return View(pizza);
        }


        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create(Pizza pizza)
        {
            ModelState.Remove("UploadedImage"); 

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

        // EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pizza newPizza)
        {
            ModelState.Remove("UploadedImage"); 

            if (id != newPizza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Fetch the existing pizza
                    var existingPizza = _db.Pizzas.AsNoTracking().FirstOrDefault(p => p.Id == id);
                    if (existingPizza == null)
                    {
                        return NotFound();
                    }

                    // If a new image is not uploaded, retain the existing image
                    if (newPizza.UploadedImage == null)
                    {
                        newPizza.Image = existingPizza.Image;
                    }
                    else
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await newPizza.UploadedImage.CopyToAsync(memoryStream);
                            newPizza.Image = memoryStream.ToArray();
                        }
                    }

                    _db.Update(newPizza);
                    await _db.SaveChangesAsync();
                }
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
                return RedirectToAction(nameof(Index));
            }
            return View(newPizza);
        }





        // DELETE
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
