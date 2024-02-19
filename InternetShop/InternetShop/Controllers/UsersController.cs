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
        public async Task<IActionResult> Create([Bind("Id,RoleId,Email,Password,Surname,Name,Patronimic,PhoneNumber")] Users users)
        {
            // Checking that all fields are filled
            if (string.IsNullOrWhiteSpace(users.Name) ||
                string.IsNullOrWhiteSpace(users.Surname) ||
                string.IsNullOrWhiteSpace(users.Patronimic) ||
                string.IsNullOrWhiteSpace(users.Email) ||
                string.IsNullOrWhiteSpace(users.PhoneNumber) ||
                string.IsNullOrWhiteSpace(users.Password))
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
                if (string.IsNullOrEmpty(users.Password) || users.Password.Length < 6)
                {
                    ModelState.AddModelError("Password", "The password must contain at least 6 characters");
                }
                else if (!Regex.IsMatch(users.Password, @"^[^\s~!@#\$%\^&\*\(\)_\+\-=\\/\{\}\[\]\.,\?\<\>:;]*$"))
                {
                    ModelState.AddModelError("Password", "The password must not contain spaces or symbols ~! @#$%^&*()-_+=/{}[].,? <>:;");
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoleId,Email,Password,Surname,Name,Patronimic,PhoneNumber")] Users users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            return View(users);
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
