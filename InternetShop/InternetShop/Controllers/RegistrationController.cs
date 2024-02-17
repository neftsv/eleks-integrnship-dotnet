using InternetShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Controllers;

public class RegistrationController : Controller
{
    private readonly UserManager<Users> _userManager;
    private readonly SignInManager<Users> _signInManager;

    public RegistrationController(UserManager<Users> userManager, SignInManager<Users> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }
    [HttpGet] // attribute indicating that this method responds to HTTP GET requests
    public ActionResult Register()
    {
        return View();
    }
    [HttpPost] // attribute indicating that this method responds to HTTP POST requests
    public async Task<ActionResult> Register(Users model)
    {
        if (ModelState.IsValid)
        {
            // Checking whether a user with this email already exists
            if (model.Email == null)
            {
                ModelState.AddModelError("", "The email address cannot be empty");
                return View(model);
            }

            if (model.Password == null)
            {
                ModelState.AddModelError("", "The password cannot be empty");
                return View(model);
            }

            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "A user with this email address already exists");
                return View(model);
            }

            var user = new Users
            {
                Name = model.Name,
                Surname = model.Surname,
                Patronimic = model.Patronimic,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Successful registration, automatic user login
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }
}