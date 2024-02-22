using InternetShop.Interface;
using InternetShop.Models;
using InternetShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(ICollection<int> categoryId, string productName,string sortMethod = "def", int page = 1)
        {
            ICollection<Products> model = await _productsRepository.GetAllProductsAsync();

            if (productName != null)
            {
                model = model.Where(p => p.Name == productName).ToList();
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
    }
}