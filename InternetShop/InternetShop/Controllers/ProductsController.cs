using InternetShop.Data;
using InternetShop.Interface;
using InternetShop.Models;
using InternetShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ApplicationDbContext _context;
        public ProductsController(IProductsRepository productsRepository, ApplicationDbContext context)
        {
            _productsRepository = productsRepository;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index(ICollection<int> categoryId, string productName,string sortMethod = "def", int page = 1)
        {
            ICollection<Products> model = await _productsRepository.GetAllProductsAsync();

            if (productName != null)
            {
                model = model.Where(p => p.Name.ToUpper().Contains(productName.ToUpper())).ToList();
                ViewBag.productName = productName;
            }

            if(categoryId.Count() != 0)
            {
                var sortedData = new List<Products>();
                foreach (var category in categoryId)
                {
                    var data = model.Where(d => d.CategoryId == category).ToList();
                    sortedData.AddRange(data);
                }

                model = sortedData;
                ViewBag.checkedTypes = categoryId;
            }

            switch (sortMethod)
            {
                case "priceDESC":
                    model = model.OrderByDescending(m => m.Price).ToList();
                    break;

                case "priceASC":
                    model = model.OrderBy(m => m.Price).ToList();
                    break;

                case "nameDESC":
                    model = model.OrderByDescending(m => m.Name).ToList();
                    break;

                case "nameASC":
                    model = model.OrderBy(m => m.Name).ToList();
                    break;

                default:

                    break;
            }

            ViewBag.sortMethod = sortMethod;

            var pagination = await _productsRepository.PaginationProductsAsync(page, model);
            return View(pagination);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = _context.Products.Include(p => p.Images).FirstOrDefault(p => p.Id == id);

            if (product == null /*|| !product.IsApproved*/)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,Name,Description,Price")] Products product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}