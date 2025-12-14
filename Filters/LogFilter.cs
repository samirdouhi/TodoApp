using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TodoApp.Filters
{
    public class LogFilter : IAsyncActionFilter
    {
        private readonly IWebHostEnvironment _env;
        private static readonly object _lock = new();

        public LogFilter(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var http = context.HttpContext;

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var user = http.Session.GetString("User") ?? "Anonymous";
            var controller = context.RouteData.Values["controller"]?.ToString() ?? "?";
            var action = context.RouteData.Values["action"]?.ToString() ?? "?";

            var line = $"{timestamp} – {user} – {controller} – {action}{Environment.NewLine}";

            // log.txt à la racine du projet
            var path = Path.Combine(_env.ContentRootPath, "log.txt");

            lock (_lock)
            {
                File.AppendAllText(path, line);
            }

            await next();
        }
    }
}
