using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CakeCrafter.API.Migrations
{
    /// <inheritdoc />
    public partial class IngredientModelUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IngredientCategoryId",
                table: "Ingredients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MeasureUnitId",
                table: "Ingredients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_IngredientCategoryId",
                table: "Ingredients",
                column: "IngredientCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_MeasureUnitId",
                table: "Ingredients",
                column: "MeasureUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_IngredientCategories_IngredientCategoryId",
                table: "Ingredients",
                column: "IngredientCategoryId",
                principalTable: "IngredientCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_MeasureUnits_MeasureUnitId",
                table: "Ingredients",
                column: "MeasureUnitId",
                principalTable: "MeasureUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_IngredientCategories_IngredientCategoryId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_MeasureUnits_MeasureUnitId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_IngredientCategoryId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_MeasureUnitId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "IngredientCategoryId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "MeasureUnitId",
                table: "Ingredients");
        }
    }
}
