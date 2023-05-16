using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using MeteoStorm.InfoHub.Models.Access;
using MeteoStorm.InfoHub.MediatR.Access.Commands;
using MeteoStorm.DataAccess;
using Services;
using Microsoft.EntityFrameworkCore;

namespace MeteoStorm.InfoHub.MediatR.Access.Handlers
{
  public class LoginHandler : IRequestHandler<LoginCommand, LoginModel>
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<LoginHandler> _logger;
    private readonly MeteoStormDbContext _dbContext;
    private readonly UserService _userService;

    public LoginHandler(IHttpContextAccessor httpContextAccessor, 
      ILogger<LoginHandler> logger, MeteoStormDbContext dbContext,
      UserService userService)
    {
      _httpContextAccessor = httpContextAccessor;
      _logger = logger;
      _dbContext = dbContext;
      _userService = userService;
    }

    public async Task<LoginModel> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
      var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == request.Model.Login && u.IsActive);
      if (user == null) 
      {
        _logger.LogInformation("Unexisted user {User} login attempt", request.Model.Login);
        request.Model.ErrorMessage = "User not found";
        return request.Model;
      }
      if(!_userService.IsPasswordCorrect(user, user.PasswordHash, request.Model.Password))
      {
        _logger.LogInformation("{User} login attempt failed", request.Model.Login);
        request.Model.ErrorMessage = "Incorrect password";
        return request.Model;
      }

      var claims = new List<Claim>
      { 
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, request.Model.Login),
        new Claim(ClaimTypes.Role, user.Role)
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
  }
}
