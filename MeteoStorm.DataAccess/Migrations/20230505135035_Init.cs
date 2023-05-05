using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeteoStorm.DataAccess.Migrations
{
  /// <inheritdoc />
  public partial class Init : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Cities",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            RussianName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
            EnglishName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
            Latitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
            Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
            TimeZoneOffset = table.Column<int>(type: "int", nullable: false),
            GatherMeteoData = table.Column<bool>(type: "bit", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Cities", x => x.Id);
          });



      migrationBuilder.CreateTable(
          name: "MeteoDataEntries",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            CityId = table.Column<int>(type: "int", nullable: false),
            DateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
            Temperature = table.Column<double>(type: "float", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_MeteoDataEntries", x => x.Id);
            table.ForeignKey(
                      name: "FK_MeteoDataEntries_Cities_CityId",
                      column: x => x.CityId,
                      principalTable: "Cities",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_MeteoDataEntries_CityId",
          table: "MeteoDataEntries",
          column: "CityId");

      migrationBuilder.Sql(@"
        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Москва', 'Moscow', 55.7558, 37.6173, 180, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Санкт-Петербург', 'Saint Petersburg', 59.9391, 30.3158, 180, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Новосибирск', 'Novosibirsk', 55.0084, 82.9357, 420, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Киев', 'Kiev', 50.4501, 30.5234, 180, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Ереван', 'Yerevan', 40.1811, 44.5136, 240, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Париж', 'Paris', 48.8566, 2.3522, 60, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Рига', 'Riga', 56.9496, 24.1052, 120, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Варшава', 'Warsaw', 52.2297, 21.0122, 60, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Сидней', 'Sydney', -33.8651, 151.2094, 600, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Токио', 'Tokyo', 35.6762, 139.6503, 540, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Каир', 'Cairo', 30.0444, 31.2357, 120, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Дакар', 'Dakar', 14.7167, -17.4677, 0, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Ванкувер', 'Vancouver', 49.2827, -123.1207, -480, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Нью-Йорк', 'New York', 40.7128, -74.0060, -240, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Майами', 'Miami', 25.7617, -80.1918, -240, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Порт-о-Пренс', 'Port-au-Prince', 18.5392, -72.3364, -300, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Рио-де-Жанейро', 'Rio de Janeiro', -22.9068, -43.1729, -180, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Буэнос-Айрес', 'Buenos Aires', -34.6037, -58.3816, -180, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Пекин', 'Beijing', 39.9042, 116.4074, 480, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Дели', 'Delhi', 28.7041, 77.1025, 330, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Дамаск', 'Damascus', 33.5138, 36.2765, 180, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Иерусалим', 'Jerusalem', 31.7683, 35.2137, 180, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Дубай', 'Dubai', 25.2048, 55.2708, 240, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData) 
        VALUES ('Кейптаун', 'Cape Town', -33.9249, 18.4241, 120, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData)
        VALUES ('Мелитополь', 'Melitopol', 46.8434, 35.3653, 120, 1);

        INSERT INTO Cities (RussianName, EnglishName, Latitude, Longitude, TimeZoneOffset, GatherMeteoData)
        VALUES ('Тольятти', 'Togliatti', 53.5208, 49.3892, 240, 1);
        ");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "MeteoDataEntries");

      migrationBuilder.DropTable(
          name: "Cities");
    }
  }
}
