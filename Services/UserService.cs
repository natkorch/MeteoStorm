using MeteoStorm.DataAccess.Models;
using Microsoft.AspNetCore.Identity;

namespace Services
{
  public class UserService
  {
    private readonly PasswordHasher<User> _passwordHasher;

    public UserService(PasswordHasher<User> passwordHasher)
    {
      _passwordHasher = passwordHasher;
    }

    public string HashPassword(User user, string password)
    {
      return _passwordHasher.HashPassword(user, password);
    }

    public bool IsPasswordCorrect(User user, string hashPassword, string providedPassword)
    {
      var passCheckResult = _passwordHasher.VerifyHashedPassword(user, hashPassword, providedPassword);
      return passCheckResult == PasswordVerificationResult.Success ||
             passCheckResult == PasswordVerificationResult.SuccessRehashNeeded;
    }
  }
}

