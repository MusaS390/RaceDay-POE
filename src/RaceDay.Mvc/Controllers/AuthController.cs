using Microsoft.AspNetCore.Mvc;
using RaceDay.Mvc.Models;
using RaceDay.Mvc.Services;
namespace RaceDay.Mvc.Controllers;

//Start of Authenticatin controller 
public class AuthController(ApiClient api):BaseController
{
 public IActionResult Login()=>View(new LoginVm());
    //Saves the newly registered user's information in the session.
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Login(LoginVm vm)
 {
  if(!ModelState.IsValid)return View(vm);
        //Logs out the user and clears their session information.
        var r =await api.Post("api/auth/login",new{vm.Email,vm.Password});
        //  Manages user authentication, registration and logout functionality for RaceDay.
        if (!r.Ok){ModelState.AddModelError("","Email or password is incorrect, or the API is unavailable.");return View(vm);}
  var u=r.Data.GetProperty("user"); HttpContext.Session.SetInt32("UserId",u.GetProperty("userId").GetInt32());
        // 9. Checks whether the submitted login information passes validation.

        HttpContext.Session.SetString("Role",u.GetProperty("role").GetString()??"Participant");
        //Checks whether the API successfully authenticated the user.
  HttpContext.Session.SetString("FullName",u.GetProperty("fullName").GetString()??"");
        //  Retrieves the authenticated user's information from the API response.
        return RedirectToAction("Index", Role=="Organiser"?"Organiser":"Participant");
        //  Stores the user's role to support role-based navigation.
    }
    public IActionResult Register()=>View(new RegisterVm());
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Register(RegisterVm vm)
 {
  if(!ModelState.IsValid)return View(vm);
  var r=await api.Post("api/auth/register",new{vm.FullName,vm.Email,vm.Password,vm.Role});
  if(!r.Ok){ModelState.AddModelError("",r.Error??"Registration failed.");return View(vm);}
  var u=r.Data; HttpContext.Session.SetInt32("UserId",u.GetProperty("userId").GetInt32()); HttpContext.Session.SetString("Role",vm.Role); HttpContext.Session.SetString("FullName",vm.FullName);
  return RedirectToAction("Index",vm.Role=="Organiser"?"Organiser":"Participant");
 }
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Logout(){if(SignedIn) await api.Post("api/auth/logout",new{});HttpContext.Session.Clear();return RedirectToAction("Index","Home");}
}
