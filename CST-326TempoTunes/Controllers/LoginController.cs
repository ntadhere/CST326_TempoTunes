using Microsoft.AspNetCore.Mvc;
using CST_326TempoTunes.Models;
using CST_326TempoTunes.Services.Business;
using BCrypt.Net;
using Org.BouncyCastle.Crypto.Generators;

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
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        ///* ----------  POST /Login  ---------- */
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Index(LoginViewModel vm)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.AddModelError(string.Empty, "Please provide both username and password.");
        //        return View(vm);
        //    }

        //    var user = _users.GetUserByUsername(vm.Username);

        //    if (user is null || !BCrypt.Net.BCrypt.Verify(vm.Password, user.Password))
        //    {
        //        ModelState.AddModelError(string.Empty, "Invalid username or password.");
        //        return View(vm);
        //    }

        //    // ***** Login success *****
        //    // TODO: create auth cookie / claims identity here.
        //    TempData["LoginMessage"] = $"Welcome, {user.Username}!";
        //    return RedirectToAction("Index", "Home");   // or wherever you want to land
        //}
    }
}
