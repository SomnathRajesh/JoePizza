using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoePizza.Data.Migrations
{
    public partial class updatedPizzaModelagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Pizza");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Pizza",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
