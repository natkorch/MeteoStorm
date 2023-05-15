using MediatR;
using MeteoStorm.InfoHub.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using MeteoStorm.InfoHub.Models.Access;
using MeteoStorm.InfoHub.MediatR.Access.Commands;
using MeteoStorm.DataAccess;

namespace MeteoStorm.InfoHub.MediatR.Access.Handlers
{
  public class LoginHandler : IRequestHandler<LoginCommand, LoginModel>
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<LoginHandler> _logger;
    private readonly MeteoStormDbContext _dbContext;

    public LoginHandler(IHttpContextAccessor httpContextAccessor, 
      ILogger<LoginHandler> logger, MeteoStormDbContext dbContext)
    {
      _httpContextAccessor = httpContextAccessor;
      _logger = logger;
      _dbContext = dbContext;
    }

    public async Task<LoginModel> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
      var cities = _dbContext.Cities.ToList();
      _logger.LogInformation("{CityCount} cities found", cities.Count);

      if (request.Model.Login == "admin" && request.Model.Password == "111")
      {
        var claims = new List<Claim>
        { 
          //TODO: Change to Id in DB
          new Claim(ClaimTypes.NameIdentifier, request.Model.Login),
          new Claim(ClaimTypes.Name, request.Model.Login),
          //TODO: Change to Role string from DB
          new Claim(ClaimTypes.Role, AppRoles.Admin)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var properties = new AuthenticationProperties
        {
          AllowRefresh = true,
          IsPersistent = true
        };

        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
          new ClaimsPrincipal(identity), properties);
        _logger.LogInformation("{User} has signed in successfully", request.Model.Login);
        return request.Model;
      }

      _logger.LogInformation("{User} login attempt failed", request.Model.Login);
      request.Model.ErrorMessage = "Incorrect credentials";
      return request.Model;
    }
  }
}
