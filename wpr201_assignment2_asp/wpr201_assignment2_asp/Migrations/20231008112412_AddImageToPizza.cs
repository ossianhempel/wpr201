using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wpr201_assignment2_asp.Migrations
{
    public partial class AddImageToPizza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Pizzas");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Pizzas",
                type: "BLOB",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Pizzas");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Pizzas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
