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

namespace InternetShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly int defaultRoleId = 1; // The default role ID

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            var users = new Users();

            // Get data from the form using form collection
            users.Name = form["Name"];
            users.Surname = form["Surname"];
            users.Patronimic = form["Patronimic"];
            users.Email = form["Email"];
            users.PhoneNumber = form["PhoneNumber"];
            users.Password = form["Password"];

            string passwordConfirmation = form["passwordConfirmation"]!;

            // Checking if the password and confirmation are not null or empty
            if (string.IsNullOrWhiteSpace(users.Password) || string.IsNullOrWhiteSpace(passwordConfirmation))
            {
                ModelState.AddModelError("", "Please fill in all fields");
            }
            else
            {
                // Checking that all fields are filled
                if (string.IsNullOrWhiteSpace(users.Name) ||
                    string.IsNullOrWhiteSpace(users.Surname) ||
                    string.IsNullOrWhiteSpace(users.Patronimic) ||
                    string.IsNullOrWhiteSpace(users.Email) ||
                    string.IsNullOrWhiteSpace(users.PhoneNumber))
                {
                    ModelState.AddModelError("", "Please fill in all fields");
                }
                else
                {
                    // Checking whether a user with such surname, first name and patronymic already exists in the database
                    if (_context.Users.Any(u => u.Name == users.Name && u.Surname == users.Surname && u.Patronimic == users.Patronimic))
                    {
                        ModelState.AddModelError("", "Such a user is already registered");
                    }

                    // Checking if the email entered by the user is valid
                    if (users.Email != null && !Regex.IsMatch(users.Email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
                    {
                        ModelState.AddModelError("Email", "Please enter a valid email");
                    }

                    // Checking whether the phone number entered by the user is valid
                    if (users.PhoneNumber != null && !Regex.IsMatch(users.PhoneNumber, @"^\+?[0-9]{3}-?[0-9]{6,12}$"))
                    {
                        ModelState.AddModelError("PhoneNumber", "Please enter a valid phone number");
                    }

                    // Checking the password for compliance with the requirements
                    if (string.IsNullOrEmpty(users.Password) || users.Password.Length < 8)
                    {
                        ModelState.AddModelError("Password", "Password must contain at least 8 characters");
                    }
                    else if (!Regex.IsMatch(users.Password, @"^(?=.*[A-Z].*[A-Z])(?=.*[a-z].*[a-z])(?=.*\d.*\d)[A-Za-z\d@$!%*?&]+$"))
                    {
                        ModelState.AddModelError("Password", "\r\nPassword must contain at least 2 uppercase letters, 2 lowercase letters, and 2 numbers, and must not contain prohibited characters");
                    }
                    // Checking whether the password and its confirmation match
                    if (users.Password != passwordConfirmation)
                    {
                        ModelState.AddModelError("Password", "Password and confirmation do not match");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                users.RoleId = defaultRoleId; // Assigning a role to a user

                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoleId,Email,Password,Surname,Name,Patronimic,PhoneNumber")] Users users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }
            try
            {
                _context.Update(users);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(users.Id))
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

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users != null)
            {
                _context.Users.Remove(users);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
