using Microsoft.AspNetCore.Mvc.Filters;

namespace TodoApp.Filters
{
    public class LogFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var dateHeure = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var controller =
                context.RouteData.Values["controller"]?.ToString();

            var action =
                context.RouteData.Values["action"]?.ToString();

            var line =
                $"{dateHeure} - {controller} - {action}{Environment.NewLine}";

            File.AppendAllText("log.txt", line);
        }
    }
}
