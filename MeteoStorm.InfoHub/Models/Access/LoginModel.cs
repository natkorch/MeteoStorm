using System.ComponentModel.DataAnnotations;

namespace MeteoStorm.InfoHub.Models.Access
{
  public class LoginModel
  {
    [Display(Name = "Login"), Required(ErrorMessage = "Enter login")]
    [MaxLength(32, ErrorMessage = "Maximum length for login is 32 characters")]
    public string Login { get; set; }

    [Display(Name = "Password"), Required(ErrorMessage = "Enter password")]
    [MaxLength(16, ErrorMessage = "Maximum length for password is 16 characters")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string ErrorMessage { get; set; }
  }
}
