using Microsoft.EntityFrameworkCore.Migrations;
using MeteoStorm.DataAccess.Constants;
using MeteoStorm.DataAccess.Models;
using Microsoft.AspNetCore.Identity;

#nullable disable

namespace MeteoStorm.DataAccess.Migrations
{
  /// <inheritdoc />
  public partial class AddUser : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Users",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Login = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
            PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
            Role = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
            IsActive = table.Column<bool>(type: "bit", nullable: false),
            CityId = table.Column<int>(type: "int", nullable: true),
            FirstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
            Patronymic = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
            LastName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Users", x => x.Id);
            table.ForeignKey(
                      name: "FK_Users_Cities_CityId",
                      column: x => x.CityId,
                      principalTable: "Cities",
                      principalColumn: "Id");
          });

      migrationBuilder.CreateIndex(
          name: "IX_Users_CityId",
          table: "Users",
          column: "CityId");

      var user = User.Create("admin", AppRoles.Admin);
      var hashedPassword = new PasswordHasher<User>().HashPassword(user, "password");
      migrationBuilder.Sql($"INSERT INTO Users (Login, PasswordHash, IsActive, Role) VALUES ('{user.Login}', '{hashedPassword}', 1, '{user.Role}')");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Users");
    }
  }
}
