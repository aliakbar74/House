using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using House.Data;
using House.Models;
using House.Models.ViewModel;
using House.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace House.Controllers {
    [Area("Admin")]
    public class ProductsController : Controller {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _hostingEnvironment;

        [BindProperty] public ProductsViewModel ProductsVm { get; set; }

        public ProductsController(ApplicationDbContext db, IHostingEnvironment hostingEnvironment) {
            _db = db;
            _hostingEnvironment = hostingEnvironment;

            ProductsVm = new ProductsViewModel {
                ProductTypes = _db.ProductTypeses.ToList(),
                SpecialTags = _db.SpecialTagses.ToList(),
                Products = new Products()
            };
        }

        public async Task<IActionResult> Index() {
            var products = _db.Products.Include(m => m.ProductTypes).Include(m => m.SpecialTags);
            return View(await products.ToListAsync());
        }

        public IActionResult Create() {
            return View(ProductsVm);
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreatePost() {
            if (!ModelState.IsValid)
                return View(ProductsVm);

            _db.Products.Add(ProductsVm.Products);
            await _db.SaveChangesAsync();

            var webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var productFromDb = _db.Products.Find(ProductsVm.Products.Id);

            if (files.Count != 0) {
                var uploads = Path.Combine(webRootPath, SD.ProductImageFolder);
                var extension = Path.GetExtension(files[0].FileName);
                using (var fs = new FileStream(Path.Combine(uploads, ProductsVm.Products.Id + extension),
                    FileMode.Create)) {
                    files[0].CopyTo(fs);
                }

                productFromDb.Image = @"\" + SD.ProductImageFolder + @"\" + ProductsVm.Products.Id + extension;
            }
            else {
                var uploads = Path.Combine(webRootPath, SD.ProductImageFolder + @"\" + SD.DefaultProductImage);
                System.IO.File.Copy(uploads,
                    webRootPath + @"\" + SD.ProductImageFolder + @"\" + ProductsVm.Products.Id + ".png");
                productFromDb.Image = @"\" + webRootPath + @"\" + SD.ProductImageFolder + @"\" +
                                      ProductsVm.Products.Id + ".png";
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) return BadRequest();
            ProductsVm.Products = await _db.Products.Include(m => m.ProductTypes).Include(m => m.SpecialTags)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ProductsVm.Products == null)
                return NotFound();
            return View(ProductsVm);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id) {
            if (!ModelState.IsValid) return View(ProductsVm);
            if (id == null) return BadRequest();

            var webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var productFromDb = _db.Products.FirstOrDefault(p => p.Id == id);
            if (productFromDb == null) return NotFound();
            if (files.Count> 0 && files[0] != null) {
                var uploads = Path.Combine(webRootPath, SD.ProductImageFolder);
                var extensionOld = Path.GetExtension(files[0].FileName);
                var extensionNew = Path.GetExtension(productFromDb.Image);
                if (System.IO.File.Exists(
                    Path.Combine(webRootPath, uploads + ProductsVm.Products.Id + extensionOld))) {
                    System.IO.File.Delete(
                        Path.Combine(webRootPath, uploads + ProductsVm.Products.Id + extensionOld));
                }

                using (var fs = new FileStream(Path.Combine(uploads, ProductsVm.Products.Id + extensionNew),
                    FileMode.Create)) {
                    files[0].CopyTo(fs);
                }

                ProductsVm.Products.Image =
                    @"\" + SD.ProductImageFolder + @"\" + ProductsVm.Products.Id + extensionNew;

                if (ProductsVm.Products.Image != null) {
                    productFromDb.Image = ProductsVm.Products.Image;
                }

            }
                productFromDb.Name = ProductsVm.Products.Name;
                productFromDb.Price = ProductsVm.Products.Price;
                productFromDb.Available = ProductsVm.Products.Available;
                productFromDb.ShadeColor = ProductsVm.Products.ShadeColor;
                productFromDb.ProductTypesId = ProductsVm.Products.ProductTypesId;
                productFromDb.SpecialTagsId = ProductsVm.Products.SpecialTagsId;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id) {
            if (id == null) return BadRequest();
            ProductsVm.Products = await _db.Products.Include(m => m.ProductTypes).Include(m => m.SpecialTags)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ProductsVm.Products == null) return NotFound();
            return View(ProductsVm);
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id == null) return BadRequest();

            ProductsVm.Products = await _db.Products.Include(m => m.ProductTypes).Include(m => m.SpecialTags)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (ProductsVm.Products == null) return NotFound();
            return View(ProductsVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id) {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();
            
            var rootPath = _hostingEnvironment.WebRootPath;
            var uploads = Path.Combine(rootPath, SD.ProductImageFolder);
            var extension = Path.GetExtension(product.Image);

            if (System.IO.File.Exists(Path.Combine(uploads, product.Id + extension)))
                System.IO.File.Delete(Path.Combine(uploads, product.Id + extension));
            
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}