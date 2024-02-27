using InternetShop.Data;
using InternetShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

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
		public IActionResult SubmitOrder(string deliveryType, string address)
		{
			try
			{
				// Find the delivery record based on the selected delivery type
				var delivery = _context.Delivery.FirstOrDefault(d => d.DeliveryType == deliveryType);

				if (delivery == null)
				{
					return RedirectToAction("Index");
				}

				// Create a new Orders object
				var order = new Orders
				{
					DeliveryID = delivery.Id, // Set the DeliveryID property
					DeliveryAddress = address,
					Date = DateTime.Now // Set the order date to current date and time
										// You may need to set other properties of the Orders object as needed
				};

				// Add the Orders object to the context
				_context.Orders.Add(order);

				// Save changes to the database
				_context.SaveChanges();

				// Redirect to the index page or a success page
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				// Log the exception
				_logger.LogError(ex, "An error occurred while submitting the order.");

				// You can redirect to an error page or display an error message to the user
				return RedirectToAction("Error", "Home");
			}
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

