using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agro.Model.Migrations
{
    public partial class animal_part_to_intention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestStatus",
                table: "Requests");

            migrationBuilder.AddColumn<int>(
                name: "AnimalPart",
                table: "Intentions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnimalPart",
                table: "Intentions");

            migrationBuilder.AddColumn<int>(
                name: "RequestStatus",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
