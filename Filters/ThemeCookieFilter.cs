using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TodoApp.Filters
{
    public class ThemeCookieFilter : IAsyncActionFilter
    {
        public const string ThemeKey = "Theme";
        private const string DefaultTheme = "light";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var http = context.HttpContext;

            // Lire le cookie reçu (request)
            var theme = http.Request.Cookies[ThemeKey];

            if (string.IsNullOrWhiteSpace(theme))
            {
                theme = DefaultTheme;

                // Écrire le cookie (response)
                http.Response.Cookies.Append(
                    ThemeKey,
                    theme,
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(30),
                        IsEssential = true,
                        SameSite = SameSiteMode.Lax,
                        Secure = http.Request.IsHttps,
                        HttpOnly = false // thème = préférence UI (pas un secret)
                    });
            }

            // Rendre dispo aux vues (layout inclus)
            http.Items[ThemeKey] = theme;

            await next();
        }
    }
}
