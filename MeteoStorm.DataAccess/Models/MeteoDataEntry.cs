using MeteoStorm.DataAccess.Interfaces;

namespace MeteoStorm.DataAccess.Models
{
  /// <summary>
  /// Замер метеорологических данных, предоставляемый внешним сервисом
  /// </summary>
  public class MeteoDataEntry: IEntity
  {
    /// <summary>
    /// Идентификатор в базе данных
    /// </summary>
    public int Id { get; protected set; }

    /// <summary>
    /// Идентификатор города в таблице Cities
    /// </summary>
    public int CityId { get; internal set; }
    /// <summary>
    /// Город
    /// </summary>
    public virtual City City { get; set; }

    /// <summary>
    /// Дата и время замера, предоставляемого метеорологическим сервисом 
    /// </summary>
    public DateTimeOffset DateTime { get; set; }

    /// <summary>
    /// Температура в городе на момент замера
    /// </summary>
    public double Temperature { get; set; }

    public static MeteoDataEntry Create(City city, DateTimeOffset dateTime, double temperature)
    {
      var meteoDataEntry = new MeteoDataEntry();
      meteoDataEntry.City = city;
      meteoDataEntry.DateTime = dateTime;
      meteoDataEntry.Temperature = temperature;

      return meteoDataEntry;
    }
  }
}
