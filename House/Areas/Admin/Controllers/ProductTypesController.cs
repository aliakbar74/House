using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using House.Data;
using House.Models;
using Microsoft.EntityFrameworkCore;

namespace House.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.ProductTypeses.ToList());
        }

        //get create action method
        public IActionResult Create()
        {
            return View();
        }

        //post create action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes product)
        {
            if (ModelState.IsValid)
            {
                _db.ProductTypeses.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        //Get Edit action resualt
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();
            var productType = await _db.ProductTypeses.FindAsync(id);
            if (productType == null) return NotFound();
            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductTypes product)
        {
            if (id != product.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _db.Update(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var product = await _db.ProductTypeses.FindAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, ProductTypes productType)
        {
            if (id != productType.Id)
            {
                return NotFound();
            }

            _db.ProductTypeses.Remove(productType);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();
            var product = await _db.ProductTypeses.FindAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}