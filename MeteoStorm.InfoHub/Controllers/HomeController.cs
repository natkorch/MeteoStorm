using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using MeteoStorm.InfoHub.Authorization;
using MeteoStorm.InfoHub.Models.Home;

namespace MeteoStorm.InfoHub.Controllers
{
  [Authorize(Policy = Policies.Employees)]
  public class HomeController : Controller
  {
    [HttpGet]
    public IActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public IActionResult Privacy()
    {
      return View();
    }

    [HttpGet]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
      var exception = exceptionHandlerPathFeature?.Error;

      var errorViewModel = new ErrorViewModel
      {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
        ErrorMessage = exception?.Message
      };

      return View(errorViewModel);
    }
  }
}