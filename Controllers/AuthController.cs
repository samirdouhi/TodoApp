using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return View();

            HttpContext.Session.SetString("User", login);
            return RedirectToAction("Index", "Todo");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User");
            return RedirectToAction("Login");
        }
    }
}

