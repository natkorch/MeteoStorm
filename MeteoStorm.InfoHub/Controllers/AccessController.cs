using Microsoft.AspNetCore.Mvc;
using MediatR;
using MeteoStorm.InfoHub.Models.Access;
using MeteoStorm.InfoHub.MediatR.Access.Commands;

namespace MeteoStorm.InfoHub.Controllers
{
  public class AccessController : BaseController
  {
    public AccessController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    public IActionResult Login()
    {
      var user = HttpContext.User;
      if (user.Identity.IsAuthenticated)
        return RedirectToAction("Index", "Home");

      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
      if (!ModelState.IsValid)
        return View(model);

      var result = await _mediator.Send(new LoginCommand(model));
      if (string.IsNullOrEmpty(result.ErrorMessage))
        return RedirectToAction("Index", "Home");
      else
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
      await _mediator.Send(new LogoutCommand());
      return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
      return View();
    }
  }
}
