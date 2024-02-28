﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace PlatF.Data.Migrations
{
    public partial class premise_user_cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Premises_AspNetUsers_UserId",
                table: "Premises");

            migrationBuilder.AddForeignKey(
                name: "FK_Premises_AspNetUsers_UserId",
                table: "Premises",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Premises_AspNetUsers_UserId",
                table: "Premises");

            migrationBuilder.AddForeignKey(
                name: "FK_Premises_AspNetUsers_UserId",
                table: "Premises",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
