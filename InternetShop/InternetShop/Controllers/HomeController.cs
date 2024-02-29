using InternetShop.Data;
using InternetShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace InternetShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _context;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
			_context = context;
		}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Products()
        {
            return View();
        }
		public IActionResult Order()
		{
			var deliveryTypes = _context.Delivery.Select(d => d.DeliveryType).ToList();
			ViewBag.DeliveryTypes = deliveryTypes;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SubmitOrder(string deliveryType, string address)
		{
			// Validate inputs
			if (string.IsNullOrEmpty(deliveryType) || string.IsNullOrEmpty(address))
			{
				// Return a bad request response if inputs are invalid
				return BadRequest("Delivery type and address are required.");
			}

			// Find the delivery record based on the selected delivery type asynchronously
			var delivery = await _context.Delivery.FirstOrDefaultAsync(d => d.DeliveryType == deliveryType);

			if (delivery == null)
			{
				// Return a not found response if delivery type is not found
				return NotFound("Delivery type not found.");
			}
			/*var username = User.Identity.Name;
			var User = await _context.Users.FirstOrDefaultAsync(d => d.Email == User.Identity.Name);*/
			// Create a new Orders object
			var order = new Orders
			{
				DeliveryId = delivery.Id, // Set the DeliveryID property
				DeliveryAddress = address,
				Date = DateTime.UtcNow // Use UTC time for consistency
			};

			// Add the Orders object to the context
			_context.Orders.Add(order);

			// Save changes to the database asynchronously
			await _context.SaveChangesAsync();

			// Return a success response with the newly created order
			return RedirectToAction("Order");

		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
