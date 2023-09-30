using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoePizza.Data.Migrations
{
    public partial class addedPizzaModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PizzaId",
                table: "Toppings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pizza",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PizzaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizza", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Toppings_PizzaId",
                table: "Toppings",
                column: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Toppings_Pizza_PizzaId",
                table: "Toppings",
                column: "PizzaId",
                principalTable: "Pizza",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Toppings_Pizza_PizzaId",
                table: "Toppings");

            migrationBuilder.DropTable(
                name: "Pizza");

            migrationBuilder.DropIndex(
                name: "IX_Toppings_PizzaId",
                table: "Toppings");

            migrationBuilder.DropColumn(
                name: "PizzaId",
                table: "Toppings");
        }
    }
}
