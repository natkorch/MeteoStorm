using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using MeteoStorm.InfoHub.MediatR.Access.Commands;

namespace MeteoStorm.InfoHub.MediatR.Access.Handlers
{
  public class LogoutHandler : IRequestHandler<LogoutCommand>
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LogoutHandler(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
      await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
  }
}
