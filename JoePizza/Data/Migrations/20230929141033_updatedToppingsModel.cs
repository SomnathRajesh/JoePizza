using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoePizza.Data.Migrations
{
    public partial class updatedToppingsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Toppings_Pizza_PizzaId",
                table: "Toppings");

            migrationBuilder.DropIndex(
                name: "IX_Toppings_PizzaId",
                table: "Toppings");

            migrationBuilder.DropColumn(
                name: "PizzaId",
                table: "Toppings");

            migrationBuilder.CreateTable(
                name: "PizzaToppings",
                columns: table => new
                {
                    PizzasId = table.Column<int>(type: "int", nullable: false),
                    ToppingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaToppings", x => new { x.PizzasId, x.ToppingsId });
                    table.ForeignKey(
                        name: "FK_PizzaToppings_Pizza_PizzasId",
                        column: x => x.PizzasId,
                        principalTable: "Pizza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PizzaToppings_Toppings_ToppingsId",
                        column: x => x.ToppingsId,
                        principalTable: "Toppings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PizzaToppings_ToppingsId",
                table: "PizzaToppings",
                column: "ToppingsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzaToppings");

            migrationBuilder.AddColumn<int>(
                name: "PizzaId",
                table: "Toppings",
                type: "int",
                nullable: true);

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
    }
}
