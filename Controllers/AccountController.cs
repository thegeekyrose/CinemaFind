
using CinemaFind.Models;
using CinemaFind.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaFind.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Movies");
            }

            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(
            RegisterViewModel model)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Movies");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                DisplayName = model.DisplayName.Trim(),
                UserName = model.Email.Trim(),
                Email = model.Email.Trim()
            };

            var result = await _userManager.CreateAsync(
                user,
                model.Password);

            if (result.Succeeded)
            {
                // Sign in the newly registered user.
                await _signInManager.SignInAsync(
                    user,
                    isPersistent: false);

                return RedirectToAction("Index", "Movies");
            }

            // Display Identity validation errors.
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(
                    string.Empty,
                    error.Description);
            }

            return View(model);
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Movies");
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            LoginViewModel model,
            string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Movies");
            }

            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Email.Trim(),
                model.Password,
                model.RememberMe,
                lockoutOnFailure: true);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) &&
                    Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToAction("Index", "Movies");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "This account is temporarily locked due to multiple failed login attempts.");
            }
            else
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Invalid email or password.");
            }

            return View(model);
        }

        // POST: /Account/Logout
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Movies");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                await _signInManager.SignOutAsync();
                return Challenge();
            }

            var model = new AccountProfileViewModel
            {
                DisplayName = user.DisplayName ?? user.UserName ?? "User",
                Email = user.Email ?? string.Empty,
                ProfilePictureUrl = user.ProfilePictureUrl
            };

            return View(model);
        }

    }
}
