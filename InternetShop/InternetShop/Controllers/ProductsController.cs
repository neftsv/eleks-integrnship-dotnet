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
        public async Task<IActionResult> Index(ICollection<int> categoryId, string productName, int page = 1)
        {
            ICollection<Products> model = await _productsRepository.GetAllProductsAsync();

            if (productName != null)
            {
                model = await _productsRepository.SearchAsync(model, productName);
                ViewBag.productName = productName;
            }

            if(categoryId.Count() != 0)
            {
                model = await _productsRepository.GetProductsByCategoryAsync(categoryId);
                ViewBag.checkedTypes = categoryId;
            }

            var pagination = await _productsRepository.PaginationProductsAsync(page, model);
            return View(pagination);
        }
    }
}
