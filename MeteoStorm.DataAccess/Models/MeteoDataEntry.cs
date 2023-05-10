using MeteoStorm.DataAccess.Interfaces;

namespace MeteoStorm.DataAccess.Models
{
  /// <summary>
  /// Represents a single weather measurement record
  /// </summary>
  public class MeteoDataEntry: IEntity
  {
    /// <summary>
    /// The unique identifier for this weather measurement record in the database
    /// </summary>
    public int Id { get; protected set; }

    /// <summary>
    /// The unique identifier for the city where the weather was measured, as stored in the database
    /// </summary>
    public int CityId { get; internal set; }

    /// <summary>
    /// A reference to the City object representing the location where the weather was measured
    /// </summary>
    public virtual City City { get; set; }

    /// <summary>
    /// The date and time when the weather measurement was taken
    /// </summary>
    public DateTimeOffset DateTime { get; set; }

    /// <summary>
    /// The temperature at the time of the weather measurement, expressed in Celsius
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
