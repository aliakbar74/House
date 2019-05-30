using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using House.Data;
using House.Extensions;
using House.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace House.Areas.Customer.Controllers {
    [Area("Customer")]
    public class HomeController : Controller {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db) {
            _db = db;
        }

        public async Task<IActionResult> Index() {
            var products = await _db.Products.Include(m => m.ProductTypes).Include(m => m.SpecialTags).ToListAsync();
            return View(products);
        }

        public async Task<IActionResult> Details(int id) {
            var product = await _db.Products.Include(m => m.ProductTypes).Include(m => m.SpecialTags)
                .Where(m => m.Id == id).FirstOrDefaultAsync();
            return View(product);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public IActionResult DetailsPost(int id) {
            var lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart") ?? new List<int>();

            lstShoppingCart.Add(id);
            HttpContext.Session.Set("ssShoppingCart", lstShoppingCart);
            var lst = HttpContext.Session.Get<List<int>>("ssShoppingCart") ?? new List<int>();
            
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id) {
            var lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if(lstShoppingCart.Count>0)
                if (lstShoppingCart.Contains(id))
                    lstShoppingCart.Remove(id);
            
            HttpContext.Session.Set("ssShoppingCart", lstShoppingCart);

            return RedirectToAction("Index");
        }
        
        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}