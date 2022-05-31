using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoscowWeatherAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Temperature = table.Column<double>(type: "double precision", nullable: false),
                    RelativeHumidity = table.Column<int>(type: "integer", nullable: false),
                    DewPoint = table.Column<double>(type: "double precision", nullable: false),
                    AtmospherePressure = table.Column<int>(type: "integer", nullable: false),
                    WindDirection = table.Column<string>(type: "text", nullable: true),
                    WindSpeed = table.Column<int>(type: "integer", nullable: false),
                    Cloudiness = table.Column<int>(type: "integer", nullable: false),
                    CloudBase = table.Column<int>(type: "integer", nullable: false),
                    HorizontalVisibility = table.Column<int>(type: "integer", nullable: true),
                    WeatherConditions = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherData");
        }
    }
}
