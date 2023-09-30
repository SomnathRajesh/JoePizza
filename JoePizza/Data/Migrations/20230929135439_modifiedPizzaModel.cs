using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoePizza.Data.Migrations
{
    public partial class modifiedPizzaModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Pizza",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Pizza");
        }
    }
}
