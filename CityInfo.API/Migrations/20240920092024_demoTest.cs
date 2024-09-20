using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfo.API.Migrations
{
    public partial class demoTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 4, 1, "The one with that big Central park4", "Central park4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
