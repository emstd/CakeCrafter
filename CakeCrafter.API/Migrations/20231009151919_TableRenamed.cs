using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CakeCrafter.API.Migrations
{
    /// <inheritdoc />
    public partial class TableRenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ingredients",
                table: "ingredients");

            migrationBuilder.RenameTable(
                name: "ingredients",
                newName: "Ingredients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "ingredients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ingredients",
                table: "ingredients",
                column: "Id");
        }
    }
}
