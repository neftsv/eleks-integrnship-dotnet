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
            var model = _context.UsersProducts.Where(p => p.ProductId == id).Select(p => new ProductDetailsViewModel
            {
                Id = p.ProductId,
                UserId = p.UserId,
                CategoryId = p.Products.CategoryId,
                Images = p.Products.Images,
                Name = p.Products.Name,
                Price = p.Products.Price,
                Description = p.Products.Description
            }).FirstOrDefault();

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _context.UsersProducts.Where(p => p.ProductId == id).Select(p => new ProductDetailsViewModel
            {
                Id = p.ProductId,
                UserId = p.UserId,
                CategoryId = p.Products.CategoryId,
                Images = p.Products.Images,
                Name = p.Products.Name,
                Price = p.Products.Price,
                Description = p.Products.Description
            }).FirstOrDefault();

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name,Images,Description,Price")] Products product)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (true)
            {
                try
                {
                    
                    if (existingProduct == null)
                    {
                        return NotFound();
                    }

                    // Update the properties of the existing entity
                    existingProduct.CategoryId= product.CategoryId;
                    existingProduct.Name = product.Name;
                    existingProduct.Images = product.Images;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;

                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));
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
            return NotFound();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}