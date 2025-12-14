using TodoApp.Filters;
using TodoApp.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC + Filters globaux
builder.Services.AddControllersWithViews(options =>
{
    // Filtres globaux (s'appliquent à toutes les actions)
    options.Filters.AddService<ThemeCookieFilter>();
    options.Filters.AddService<LogFilter>();
});

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// DI (services + filters)
builder.Services.AddScoped<ISessionManagerService, SessionManagerService>();
builder.Services.AddScoped<AuthFilter>();
builder.Services.AddScoped<ThemeCookieFilter>();
builder.Services.AddScoped<LogFilter>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// (si tu as authentication/authorization plus tard, ils vont ici)
// app.UseAuthentication();
app.UseAuthorization();

// IMPORTANT : Session ici
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todo}/{action=Index}/{id?}");

app.Run();

