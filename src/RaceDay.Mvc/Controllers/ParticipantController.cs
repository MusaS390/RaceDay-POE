using Microsoft.AspNetCore.Mvc;
using RaceDay.Mvc.Models;
using RaceDay.Mvc.Services;
namespace RaceDay.Mvc.Controllers;
public class ParticipantController(ApiClient api):BaseController
{
 public async Task<IActionResult> Index(){var g=Guard("Participant");if(g!=null)return g;var r=await api.Get("api/events");ViewBag.Events=r.Data;ViewBag.Error=r.Error;return View();}
 public async Task<IActionResult> Event(int id){var g=Guard("Participant");if(g!=null)return g;var e=await api.Get($"api/events/{id}");ViewBag.Event=e.Data;var c=await api.Get($"api/events/{id}/categories");ViewBag.Categories=c.Data;ViewBag.Error=e.Error??c.Error;return View();}
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Enter(int categoryId,string? emergencyContact){var r=await api.Post($"api/categories/{categoryId}/enrolments",new{emergencyContact});TempData[r.Ok?"Success":"Error"]=r.Ok?"Your entry is confirmed.":r.Error;return RedirectToAction("MyEvents");}
 public async Task<IActionResult> MyEvents(){var g=Guard("Participant");if(g!=null)return g;var r=await api.Get("api/users/me/enrolments");ViewBag.Items=r.Data;ViewBag.Error=r.Error;return View();}
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Cancel(int id){var r=await api.Delete($"api/enrolments/{id}");TempData[r.Ok?"Success":"Error"]=r.Ok?"Entry cancelled.":r.Error;return RedirectToAction("MyEvents");}
 public async Task<IActionResult> MyResults(){var g=Guard("Participant");if(g!=null)return g;var r=await api.Get("api/users/me/enrolments");var list=new List<System.Text.Json.JsonElement>();if(r.Data.ValueKind==System.Text.Json.JsonValueKind.Array){foreach(var e in r.Data.EnumerateArray()){var id=e.GetProperty("enrolmentId").GetInt32();var x=await api.Get($"api/enrolments/{id}/result");if(x.Ok){using var doc=System.Text.Json.JsonDocument.Parse(x.Data.GetRawText());list.Add(doc.RootElement.Clone());}}}ViewBag.Items=r.Data;ViewBag.Results=list;return View();}
 public async Task<IActionResult> Profile(){var g=Guard("Participant");if(g!=null)return g;var r=await api.Get("api/users/me");if(!r.Ok){TempData["Error"]=r.Error;return RedirectToAction("Index");}return View(new ProfileVm{FullName=r.Data.GetProperty("fullName").GetString()??"",Email=r.Data.GetProperty("email").GetString()??""});}
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Profile(ProfileVm vm){if(!ModelState.IsValid)return View(vm);var r=await api.Put("api/users/me",new{vm.FullName,vm.Email});if(!r.Ok)ModelState.AddModelError("",r.Error??"Profile update failed.");else TempData["Success"]="Profile updated.";return View(vm);}
}
