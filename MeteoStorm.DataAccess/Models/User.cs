using MeteoStorm.DataAccess.Interfaces;

namespace MeteoStorm.DataAccess.Models
{
  /// <summary>
  /// Represents a user entity
  /// </summary>
  public class User : IEntity
  {
    /// <summary>
    /// The unique identifier of the user
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The login name of the user
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// The password hash of the user
    /// </summary>
    public string PasswordHash { get; set; }

    /// <summary>
    /// The role of the user
    /// </summary>
    public string Role { get; set; }

    /// <summary>
    /// Whether the user is active or not
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// The city where the user lives
    /// </summary>
    public virtual City City { get; set; }

    /// <summary>
    /// The first name of the user
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// The patronymic of the user
    /// </summary>
    public string Patronymic { get; set; }

    /// <summary>
    /// The last name of the user
    /// </summary>
    public string LastName { get; set; }

    public static User Create(string login, string role, City city = null,
      string firstname = null, string patronymic = null, string lastname = null)
    {
      var user = new User();
      user.IsActive = true;
      user.Login = login;
      user.Role = role;
      user.City = city;
      user.FirstName = firstname;
      user.Patronymic = patronymic;
      user.LastName = lastname;
      return user;
    }
  }
}
