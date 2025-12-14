using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TodoApp.Filters;

namespace TodoApp.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            // Si déjà connecté, on peut envoyer vers Todo
            var user = HttpContext.Session.GetString("User");
            if (!string.IsNullOrWhiteSpace(user))
                return RedirectToAction("Index", "Todo");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                ModelState.AddModelError("", "Login obligatoire.");
                return View();
            }

            HttpContext.Session.SetString("User", login.Trim());
            return RedirectToAction("Index", "Todo");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User");
            return RedirectToAction(nameof(Login));
        }

        
        [HttpGet]
        public IActionResult SetTheme(string theme)
        {
            theme = (theme ?? "").Trim().ToLowerInvariant();
            if (theme != "light" && theme != "dark")
                theme = "light";

            HttpContext.Response.Cookies.Append(
                ThemeCookieFilter.ThemeKey,  
                theme,
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(30),
                    IsEssential = true,
                    SameSite = SameSiteMode.Lax,
                    Secure = Request.IsHttps,
                    HttpOnly = false
                });

            // Revenir à la page précédente si possible
            var referer = Request.Headers.Referer.ToString();
            if (!string.IsNullOrWhiteSpace(referer))
                return Redirect(referer);

            return RedirectToAction("Index", "Todo");
        }
    }
}

