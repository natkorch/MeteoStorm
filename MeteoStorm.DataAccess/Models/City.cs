using MeteoStorm.DataAccess.Interfaces;

namespace MeteoStorm.DataAccess.Models
{
  /// <summary>
  /// Represents a city
  /// </summary>
  public class City: IEntity
  {
    /// <summary>
    /// The unique identifier for this city in the database
    /// </summary>
    public int Id { get; protected set; }

    /// <summary>
    /// The name of the city in Russian
    /// </summary>
    public string RussianName { get; set; }

    /// <summary>
    /// The name of the city in English
    /// </summary>
    public string EnglishName { get; set; }

    /// <summary>
    /// The latitude of the city, expressed in decimal degrees
    /// </summary>
    public decimal Latitude { get; set; }

    /// <summary>
    /// The longitude of the city, expressed in decimal degrees
    /// </summary>
    public decimal Longitude { get; set; }

    /// <summary>
    /// The time zone difference between the city and UTC, expressed in minutes
    /// </summary>
    public int TimeZoneOffset { get; set; }

    /// <summary>
    /// If true, daily weather data is gathered for this city
    /// </summary>
    public bool GatherMeteoData { get; set; }

    /// <summary>
    /// The collection of historical weather data entries for this city
    /// </summary>
    public virtual List<MeteoDataEntry> MeteoDataEntries { get; set;}
  }
}
