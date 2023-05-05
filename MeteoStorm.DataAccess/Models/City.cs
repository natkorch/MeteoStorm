using MeteoStorm.DataAccess.Interfaces;

namespace MeteoStorm.DataAccess.Models
{
  /// <summary>
  /// Город
  /// </summary>
  public class City: IEntity
  {
    /// <summary>
    /// Идентификатор в базе данных
    /// </summary>
    public int Id { get; protected set; }

    /// <summary>
    /// Русское название
    /// </summary>
    public string RussianName { get; set; }

    /// <summary>
    /// Английское название
    /// </summary>
    public string EnglishName { get; set; }

    /// <summary>
    /// Широта
    /// </summary>
    public decimal Latitude { get; set; }

    /// <summary>
    /// Долгота
    /// </summary>
    public decimal Longitude { get; set; }

    /// <summary>
    /// Часовой пояс, выраженный в разнице с UTC в минутах
    /// </summary>
    public int TimeZoneOffset { get; set; }

    /// <summary>
    /// Если флаг отмечен, по городу собирается ежедневная погодная сводка
    /// </summary>
    public bool GatherMeteoData { get; set; }

    /// <summary>
    /// История метеорологических замеров
    /// </summary>
    public virtual List<MeteoDataEntry> MeteoDataEntries { get; set;}
  }
}
