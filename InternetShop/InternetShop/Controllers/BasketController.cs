using InternetShop.Interface;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if(!User.Identity.IsAuthenticated){ return RedirectToAction("Index", "Account"); }

            var model = await _basketRepository.GetUserCratProductsAsync(User.Identity.Name);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int cartsProductsId) 
        {
            var cartsProducts = await _basketRepository.GetCartProductAsync(cartsProductsId);

            if(cartsProducts == null) 
            {
                ModelState.AddModelError("", "There are no such product in cart");
                return RedirectToAction("Index");
            }

            if (_basketRepository.DeleteProduct(cartsProducts))
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Something went wrong while deleting");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItemCount()
        {
            int cartItemCount = 0;
            if (User.Identity.IsAuthenticated)
            {
                cartItemCount = await _basketRepository.GetCartItemCount(User.Identity.Name);
                return Json(cartItemCount);
            }

            return Json(cartItemCount);
        }

    }
}
