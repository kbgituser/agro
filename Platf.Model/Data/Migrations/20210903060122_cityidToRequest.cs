using Microsoft.EntityFrameworkCore.Migrations;

namespace PlatF.Data.Migrations
{
    public partial class cityidToRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CityId",
                table: "Requests",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Cities_CityId",
                table: "Requests",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Cities_CityId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_CityId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Requests");
        }
    }
}
