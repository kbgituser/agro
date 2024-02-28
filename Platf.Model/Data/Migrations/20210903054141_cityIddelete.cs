using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlatF.Data.Migrations
{
    public partial class cityIddelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Cities_CityId1",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_CityId1",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CityId1",
                table: "Requests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                table: "Requests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId1",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CityId1",
                table: "Requests",
                column: "CityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Cities_CityId1",
                table: "Requests",
                column: "CityId1",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
