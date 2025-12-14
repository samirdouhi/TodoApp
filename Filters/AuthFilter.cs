using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TodoApp.Filters
{
    public class AuthFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.Session.GetString("User");

            if (string.IsNullOrWhiteSpace(user))
            {
                // Pas connecté => redirection vers /Auth/Login
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            await next();
        }
    }
}