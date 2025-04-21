using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using CST_326TempoTunes.Models;
using CST_326TempoTunes.Services.Business;
using CST_326TempoTunes.Security; // for PasswordHasher

namespace CST_326TempoTunes.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserCollection _users;

        public LoginController(UserCollection users)
        {
            _users = users;
        }

        /* ---------- GET /Login ---------- */
        [HttpGet]
        public IActionResult Index(string? returnUrl = null)
        {
            ViewData["BodyClass"] = "login-background";
            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());
        }

        /* ---------- POST /Login ---------- */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel vm, string? returnUrl = null)
        {
            ViewData["BodyClass"] = "login-background";
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(vm);

            var user = _users.GetUserByUsername(vm.Username);
            bool isValid = user != null && PasswordHasher.Verify(vm.Password, user.Password);

            if (!isValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(vm);
            }

            // ---------- Build cookie identity ----------
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("UserId", user.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(4)
                });

            // ---------- Redirect ----------
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        /* ---------- /Logout ---------- */
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
