namespace MeteoStorm.InfoHub.Models.Home
{
  public class ErrorViewModel
  {
    public string RequestId { get; set; }
    public string ErrorMessage { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
  }
}