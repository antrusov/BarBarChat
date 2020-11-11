using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server3.Migrations
{
    public partial class auth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birth",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Auth",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActivity",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pass",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastActivity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Pass",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birth",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
