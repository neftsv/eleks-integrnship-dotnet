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
        private IWebHostEnvironment _hostingenvironment;
        public ProductsController(IProductsRepository productsRepository, ApplicationDbContext context, IWebHostEnvironment hostingenvironment)
        {
            _productsRepository = productsRepository;
            _context = context;
            this._hostingenvironment = hostingenvironment;
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

        [Authorize(Roles = "2, 3")]
        public async Task<IActionResult> Edit(int? id)
        {
            var user = User;
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
        [Authorize]
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
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(ProductCreateViewModel product)
        {
            string filename = "";
            var images = new List<Images>();
            if (product.Images != null)
            {
                string uploadfolder = Path.Combine(_hostingenvironment.WebRootPath, "ProductImages");
                foreach (var image in product.Images)
                {
                    filename = Guid.NewGuid().ToString() + "_" + image.FileName;
                    string filepath = Path.Combine(uploadfolder, filename);
                    image.CopyTo(new FileStream(filepath, FileMode.Create));
                    images.Add(new Images { Url = filename });
                }
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            var prod = new Products
            {
                UsersProducts = new List<UsersProducts> { new UsersProducts { Users = user, UserId = user.Id } },
                CategoryId = product.CategoryId,
                Images = images,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };
            //product.UserId = Convert.ToInt32(user?.Id);

            _context.Add(prod);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Products");

            //return View(product);
        }

        [Authorize(Roles = "2, 3")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            _context.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(int id, int quantity)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            var cartExist = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == user.Id);
            if(cartExist == null) 
            {
                var cart = new CartsProducts
                {
                    Quantity = quantity,
                    Carts = new Carts
                    {
                        User = user,
                        UserId = user.Id,
                    },
                    ProductId = id,
                };
                _context.Add(cart);
                _context.SaveChanges();
                return RedirectToAction("Index", "Basket");
            }
            var cart1 = new CartsProducts
            {
                Quantity = quantity,
                Carts = cartExist,
                ProductId = id,
            };
            _context.Add(cart1);
            _context.SaveChanges();
            return RedirectToAction("Index", "Basket");
        }
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}