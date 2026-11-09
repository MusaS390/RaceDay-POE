using Microsoft.AspNetCore.Mvc;
namespace RaceDay.Mvc.Controllers;
public abstract class BaseController : Controller
{
    protected bool SignedIn => HttpContext.Session.GetInt32("UserId") != null;
    protected string Role => HttpContext.Session.GetString("Role") ?? "";
    protected IActionResult Guard(string role)
    {
        if(!SignedIn) return RedirectToAction("Login","Auth");
        if(Role!=role) return RedirectToAction("Index","Home");
        return null!;
    }
}
