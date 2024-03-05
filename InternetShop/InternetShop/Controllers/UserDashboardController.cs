﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternetShop.Data;
using InternetShop.Models;
using System.Text.RegularExpressions;

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
        var userPosts = _context.BlogPosts.Where(post => post.AuthorId == user.Id).ToList();

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

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(user);
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
        return View(user);
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
    // GET: UserDashboard/EditPassword
    [HttpGet]
    public IActionResult EditPassword()
    {
        return View();
    }

    // POST: UserDashboard/EditPassword
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditPassword(Users model, IFormCollection form)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Extracting password and confirmation from form collection
        string password = form["Password"];
        string passwordConfirmation = form["passwordConfirmation"];
        // Checking if the password and confirmation are not null or empty
        if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordConfirmation))
        {
            ModelState.AddModelError("", "Please fill in all fields");
            return View(model);
        }
        // Checking whether a user with such surname, first name and patronymic already exists in the database
        if (_context.Users.Any(u => u.Name == model.Name && u.Surname == model.Surname && u.Patronimic == model.Patronimic))
        {
            ModelState.AddModelError("", "Such a user is already registered");
            return View(model);
        }

        // Checking if the email entered by the user is valid
        if (model.Email != null && !Regex.IsMatch(model.Email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
        {
            ModelState.AddModelError("Email", "Please enter a valid email");
            return View(model);
        }

        // Checking whether the phone number entered by the user is valid
        if (model.PhoneNumber != null && !Regex.IsMatch(model.PhoneNumber, @"^\+?[0-9]{3}-?[0-9]{6,12}$"))
        {
            ModelState.AddModelError("PhoneNumber", "Please enter a valid phone number");
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

        // Obtaining and changing a user's password
        user.Password = model.Password;

        _context.Update(user);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
    }
}