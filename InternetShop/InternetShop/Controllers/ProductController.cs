using InternetShop.Interface;
using InternetShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("Product/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var model = await _productRepository.GetProductInfoAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1) 
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }

            var model = await _productRepository.GetProductInfoAsync(productId);

            var user = await _productRepository.GetUserAsync(User.Identity.Name);

            var userCart = await _productRepository.GetUserCartAsync(user.Id);

            if (userCart == null)
            {
                var cart = new Carts 
                { 
                    User = user,
                    UserId = user.Id,
                };
                if (!_productRepository.AddCartToUser(cart))
                {
                    return RedirectToAction("Index", new { id = productId });
                }
                userCart = await _productRepository.GetUserCartAsync(user.Id);
            }

            var cartProduct = new CartsProducts
            {
                CartId = userCart.Id,
                Carts = userCart,
                ProductId = productId,
                Quantity = quantity,
            };

            if (!_productRepository.AddProductToCart(cartProduct))
            {
                return RedirectToAction("Index", new { id = productId });
            }

            return RedirectToAction("Index", new {id = productId});
        }
    }
}
