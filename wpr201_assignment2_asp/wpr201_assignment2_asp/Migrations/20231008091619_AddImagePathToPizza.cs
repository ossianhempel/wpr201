using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wpr201_assignment2_asp.Migrations
{
    public partial class AddImagePathToPizza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Pizzas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Pizzas");
        }
    }
}
