using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TodoApp.Filters
{
    public class AuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // ⚠️ Mets ici la clé EXACTE que tu utilises pour stocker l'utilisateur en session
            // Exemple : "User" / "Login" / "CurrentUser" ...
            var user = context.HttpContext.Session.GetString("User");

            if (string.IsNullOrEmpty(user))
            {
                // bloque l’accès et redirige vers Auth/Login
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }
        }
    }
}
