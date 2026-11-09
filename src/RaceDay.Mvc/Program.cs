
// 1. Import the RaceDay API client service.
using RaceDay.Mvc.Services;

// 2. Create the application builder.
var builder = WebApplication.CreateBuilder(args);

// 3. Register MVC controllers and views.
builder.Services.AddControllersWithViews();

// 4. Register distributed memory cache for session storage.
builder.Services.AddDistributedMemoryCache();

// 5. Register HTTP context access for current user requests.
builder.Services.AddHttpContextAccessor();

// 6. Configure session services for maintaining user sessions.
builder.Services.AddSession(o =>
{
    // 7. Set a unique cookie name for the MVC application.
    o.Cookie.Name = ".RaceDay.Mvc";

    // 8. Prevent JavaScript from accessing the session cookie.
    o.Cookie.HttpOnly = true;

    // 9. Mark the session cookie as essential.
    o.Cookie.IsEssential = true;

    // 10. Set the session timeout to 30 minutes.
    o.IdleTimeout = TimeSpan.FromMinutes(30);
});

// 11. Register the API client using HttpClient.
builder.Services.AddHttpClient<ApiClient>();

// 12. Build the application using the configured services.
var app = builder.Build();

// 13. Check whether the application is running in production.
if (!app.Environment.IsDevelopment())
{
    // 14. Configure the application's error handling page.
    app.UseExceptionHandler("/Home/Error");

    // 15. Enable HTTP Strict Transport Security.
    app.UseHsts();
}

// 16. Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// 17. Enable static files such as CSS, JavaScript and images.
app.UseStaticFiles();

// 18. Enable routing and session middleware.
app.UseRouting();
app.UseSession();

// 19. Configure the default route to open the login page.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}"
);

// 20. Start the RaceDay MVC application.
app.Run();
