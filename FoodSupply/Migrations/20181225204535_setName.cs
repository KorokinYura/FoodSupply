using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodSupply.Migrations
{
    public partial class setName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GoodsSets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "GoodsSets");
        }
    }
}
