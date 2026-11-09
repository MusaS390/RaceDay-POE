using Microsoft.AspNetCore.Mvc;
using RaceDay.Mvc.Services;
namespace RaceDay.Mvc.Controllers;
public class HomeController(ApiClient api) : BaseController
{
 public async Task<IActionResult> Index(){var r=await api.Get("api/events"); ViewBag.Events=r.Data; ViewBag.ApiError=r.Error; return View();}
 public IActionResult Error()=>View();
}
