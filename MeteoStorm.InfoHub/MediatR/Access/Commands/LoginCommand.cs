using MeteoStorm.InfoHub.Models.Access;

namespace MeteoStorm.InfoHub.MediatR.Access.Commands
{
  public class LoginCommand : ModelCommand<LoginModel>
  {
    public LoginCommand(LoginModel model) : base(model) { }
  }
}
