using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using CST_326TempoTunes.Models;
using CST_326TempoTunes.Services.Business;
using CST_326TempoTunes.Security;   // <-- PasswordHasher lives here

namespace CST_326TempoTunes.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserCollection _users;

        public LoginController(UserCollection users)
        {
            _users = users;
        }

        /* ----------  GET /Login  ---------- */
        [HttpGet]
        public IActionResult Index(string? returnUrl = null)
        {
            ViewData["BodyClass"] = "login-background";
            ViewData["ReturnUrl"] = returnUrl;  // so the view can keep it in a hidden field
            return View(new LoginViewModel());
        }

        /* ----------  POST /Login  ---------- */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel vm, string? returnUrl = null)
        {
            ViewData["BodyClass"] = "login-background";
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(vm);

            var user = _users.GetUserByUsername(vm.Username);
            bool ok = user != null && PasswordHasher.Verify(vm.Password, user.Password);
            if (!ok)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(vm);
            }

            /* ----------  Build authentication cookie  ---------- */
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("UserId", user.Id.ToString())
                // add more claims (roles, email, etc.) as needed
            };

            var identity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,                    // remember‑me
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(4)
                });

            /* ----------  Redirect  ---------- */
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        /* ----------  GET /Logout  ---------- */
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
