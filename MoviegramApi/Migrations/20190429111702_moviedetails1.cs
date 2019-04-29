using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviegramApi.Migrations
{
    public partial class moviedetails1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Movies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Movies");
        }
    }
}
