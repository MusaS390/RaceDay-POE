// Import the RaceDay API client service.
using RaceDay.Mvc.Services;
// Create the application builder and load configuration settings.
var builder = WebApplication.CreateBuilder(args);
// Register MVC controllers and views for handling web requests.
builder.Services.AddControllersWithViews();
// Register distributed memory cache for storing session data.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(o => { o.Cookie.Name = ".RaceDay.Mvc"; o.Cookie.HttpOnly = true; o.Cookie.IsEssential = true; o.IdleTimeout = TimeSpan.FromMinutes(30); });
builder.Services.AddHttpClient<ApiClient>();
var app = builder.Build();
if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/Home/Error"); app.UseHsts(); }
app.UseHttpsRedirection(); app.UseStaticFiles(); app.UseRouting(); app.UseSession();
app.MapControllerRoute(name:"default",pattern:"{controller=Auth}/{action=Login}/{id?}");
app.Run();
