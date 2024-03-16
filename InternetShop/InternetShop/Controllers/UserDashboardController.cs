using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternetShop.Data;
using InternetShop.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using InternetShop.Services;

public class UserDashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserDashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: UserDashboard
    [HttpGet]
    public IActionResult Index()
    {
        var user = _context.Users.Include(u => u.Orders)
                              .ThenInclude(o => o.OrdersProducts)
                              .ThenInclude(op => op.Products)
                              .FirstOrDefault(u => u.Email == User.Identity.Name);
        if (user == null)
        {
            return NotFound();
        }
        // Get all user posts from database
        var userPosts = _context.BlogPosts.Where(post => post.UserId == user.Id).ToList();

        // Submitting a user and their posts to a view
        ViewBag.UserPosts = userPosts;
        return View(user);
    }
    // GET: UserDashboard/Edit/5
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: UserDashboard/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Name,Surname,Patronimic,Email,PhoneNumber")] Users user)
    {
        if (id != user.Id)
        {
            return NotFound();
        }
        try
        {
            var existingUser = _context.Users.Find(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            existingUser.Name = user.Name;
            existingUser.Surname = user.Surname;
            existingUser.Patronimic = user.Patronimic;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(user.Id))
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
    [HttpGet]
    public IActionResult OrderDetails(int orderId)
    {
        var order = _context.Orders
                            .Include(o => o.OrdersProducts)
                            .ThenInclude(op => op.Products)
                            .Include(o => o.Delivery)
                            .FirstOrDefault(o => o.Id == orderId);

        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }
    [HttpGet]
    public async Task<IActionResult> ChangeOrderStatus()
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);

        if (user == null)
        {
            return NotFound();
        }

        int userId = user.Id;

        var productIds = await _context.OrdersProducts
                                    .Where(op => op.Orders.UserId != user.Id)
                                    .Select(op => op.ProductId)
                                    .Distinct()
                                    .ToListAsync();

        var products = await _context.Products
                                    .Include(p => p.Categories)
                                    .Where(p => productIds.Contains(p.Id) && p.UsersProducts.Any(up => up.UserId == userId))
                                    .ToListAsync();

        return View(products);
    }
    [HttpPost]
    [Route("ChangeOrderStatus/SaveOrderStatus")]
    public async Task<IActionResult> SaveOrderStatus(int[] productIds, int[] statuses)
    {
        for (int i = 0; i < productIds.Length; i++)
        {
            var productId = productIds[i];
            var status = statuses[i];

            var existingProduct = await _context.OrdersProducts.FirstOrDefaultAsync(op => op.ProductId == productId);

            if (existingProduct != null)
            {
                existingProduct.Status = status;
            }
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    // GET: UserDashboard/EditPassword
    [HttpGet]
    public IActionResult EditPassword(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: UserDashboard/EditPassword
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditPassword(Users model, IFormCollection form)
    {
        // Extracting password and confirmation from form collection
        string password = form["Password"];
        string passwordConfirmation = form["passwordConfirmation"];

        // Checking if the password and confirmation are not null or empty
        if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordConfirmation))
        {
            ModelState.AddModelError("", "Please fill in all fields");
            return View(model);
        }

        // Checking the password for compliance with the requirements
        if (string.IsNullOrEmpty(password) || password.Length < 8)
        {
            ModelState.AddModelError("Password", "Password must contain at least 8 characters");
            return View(model);
        }
        else if (!Regex.IsMatch(password, @"^(?=.*[A-Z].*[A-Z])(?=.*[a-z].*[a-z])(?=.*\d.*\d)[A-Za-z\d@$!%*?&]+$"))
        {
            ModelState.AddModelError("Password", "Password must contain at least 2 uppercase letters, 2 lowercase letters, and 2 numbers, and must not contain prohibited characters");
            return View(model);
        }

        // Checking whether the password and its confirmation match
        if (password != passwordConfirmation)
        {
            ModelState.AddModelError("Password", "Password and confirmation do not match");
            return View(model);
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
        if (user == null)
        {
            return NotFound();
        }

        // Hashing the new password before saving it
        string hashedPassword = PasswordManager.HashPassword(password);
        user.Password = hashedPassword;

        _context.Update(user);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Authorize(Roles = "2,3")]
    public IActionResult Adminpannel()
    {
        return RedirectToAction("Index", "Users");
    }
    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
    }
}