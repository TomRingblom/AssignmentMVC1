using Assignment.MVC.Models;
using Assignment.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpVM model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,

                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    StreetName = model.StreetName,
                    PostalCode = model.PostalCode,
                    City = model.City
                };

                var createUser = await _userManager.CreateAsync(newUser, model.Password);
                if (createUser.Succeeded)
                {
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in createUser.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }
    }
}
