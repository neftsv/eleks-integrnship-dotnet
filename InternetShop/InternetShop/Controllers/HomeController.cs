using InternetShop.Interface;
using InternetShop.Data;
using InternetShop.Models;
using InternetShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace InternetShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;
		private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository, ApplicationDbContext context)
        {
            _logger = logger;
            _homeRepository = homeRepository;
			_context = context;
		}

        public async Task<IActionResult> Index()
        {
            var model = await _homeRepository.GetAllCategoriesAsync();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Products()
        {
            return View();
        }
		public async Task<IActionResult> Order()
		{
			var deliveryTypes = _context.Delivery.Select(d => d.DeliveryType).ToList();
			ViewBag.DeliveryTypes = deliveryTypes;
			var user = await _context.Users
							  .Include(u => u.Cart)
							  .FirstOrDefaultAsync(u => u.Email == User.Identity.Name);

			// Check if user exists
			if (user == null)
			{
				return NotFound("User not found.");
			}

			// Check if user has a cart
			if (user.Cart == null)
			{
				return NotFound("User does not have a cart.");
			}

			// Retrieve cart products for the user's cart
			var cartProducts = await _context.CartsProducts
											.Include(cp => cp.Products)
											.Where(cp => cp.CartId == user.Cart.Id)
											.ToListAsync();

			return View(cartProducts);
		}
		[HttpPost]
        [HttpPost]
        public async Task<IActionResult> SubmitOrder(string deliveryType, string address)
        {
            // Validate inputs
            if (string.IsNullOrEmpty(deliveryType) || string.IsNullOrEmpty(address))
            {
                return BadRequest("Delivery type and address are required.");
            }

            var user = await _context.Users
                                      .Include(u => u.Cart)
                                      .FirstOrDefaultAsync(u => u.Email == User.Identity.Name);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (user.Cart == null)
            {
                return NotFound("User does not have a cart.");
            }

            var delivery = await _context.Delivery.FirstOrDefaultAsync(d => d.DeliveryType == deliveryType);

            if (delivery == null)
            {
                return NotFound("Delivery type not found.");
            }

            var order = new Orders
            {
                UserId = user.Id,
                Users = user,
                DeliveryId = delivery.Id,
                DeliveryAddress = address,
                Date = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); // Save the order to get the generated order Id

            var cartProducts = await _context.CartsProducts
                                            .Where(cp => cp.CartId == user.Cart.Id)
                                            .ToListAsync();

            foreach (var cartProduct in cartProducts)
            {
                var orderProduct = new OrdersProducts
                {
                    OrderId = order.Id, // Use the generated order Id
                    ProductId = cartProduct.ProductId,
                    Quantity = cartProduct.Quantity
                };
                _context.OrdersProducts.Add(orderProduct);
            }

            _context.CartsProducts.RemoveRange(cartProducts);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "UserDashboard");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
