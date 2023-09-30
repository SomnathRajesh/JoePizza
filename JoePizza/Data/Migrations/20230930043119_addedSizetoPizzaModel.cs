using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoePizza.Data.Migrations
{
    public partial class addedSizetoPizzaModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PizzaId",
                table: "Sizes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_PizzaId",
                table: "Sizes",
                column: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sizes_Pizza_PizzaId",
                table: "Sizes",
                column: "PizzaId",
                principalTable: "Pizza",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sizes_Pizza_PizzaId",
                table: "Sizes");

            migrationBuilder.DropIndex(
                name: "IX_Sizes_PizzaId",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "PizzaId",
                table: "Sizes");
        }
    }
}
