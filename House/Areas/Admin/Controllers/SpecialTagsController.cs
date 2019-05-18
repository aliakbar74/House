using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using House.Data;
using House.Models;
using Microsoft.EntityFrameworkCore;

namespace House.Areas.Admin.Controllers {
    [Area("Admin")]
    public class SpecialTagsController : Controller {
        private readonly ApplicationDbContext _db;

        public SpecialTagsController(ApplicationDbContext db) {
            _db = db;
        }

        public IActionResult Index() {
            return View(_db.SpecialTagses.ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTags tag) {
            if (ModelState.IsValid) {
                _db.SpecialTagses.Add(tag);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(tag);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) return BadRequest();
            var tag = await _db.SpecialTagses.FindAsync(id);
            if (tag == null) return NotFound();
            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SpecialTags tag) {
            if (tag.Id != id) return NotFound();
            if (ModelState.IsValid) {
                _db.SpecialTagses.Update(tag);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        public async Task<IActionResult> Details(int? id) {
            if (id == null) return BadRequest();
            var tag = await _db.SpecialTagses.FindAsync(id);
            if (tag == null) return NotFound();
            return View(tag);
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id == null) return BadRequest();
            var tag = await _db.SpecialTagses.FindAsync(id);
            if (tag == null) return NotFound();
            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, SpecialTags specialTags) {
            if (id != specialTags.Id) return BadRequest();
            _db.SpecialTagses.Remove(specialTags);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}