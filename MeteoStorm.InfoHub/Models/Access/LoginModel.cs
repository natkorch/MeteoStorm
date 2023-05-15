using System.ComponentModel.DataAnnotations;

namespace MeteoStorm.InfoHub.Models.Access
{
  public class LoginModel
  {
    [Display(Name = "Login"), Required(ErrorMessage = "Enter login")]
    public string Login { get; set; }

    [Display(Name = "Password"), Required(ErrorMessage = "Enter password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string ErrorMessage { get; set; }
  }
}
