using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CakeCrafter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TasteId",
                table: "Cakes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Cakes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Cakes",
                type: "nvarchar(500)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cakes_CategoryId",
                table: "Cakes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cakes_ImageId",
                table: "Cakes",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Cakes_TasteId",
                table: "Cakes",
                column: "TasteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cakes_Categories_CategoryId",
                table: "Cakes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cakes_Images_ImageId",
                table: "Cakes",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cakes_Tastes_TasteId",
                table: "Cakes",
                column: "TasteId",
                principalTable: "Tastes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cakes_Categories_CategoryId",
                table: "Cakes");

            migrationBuilder.DropForeignKey(
                name: "FK_Cakes_Images_ImageId",
                table: "Cakes");

            migrationBuilder.DropForeignKey(
                name: "FK_Cakes_Tastes_TasteId",
                table: "Cakes");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Cakes_CategoryId",
                table: "Cakes");

            migrationBuilder.DropIndex(
                name: "IX_Cakes_ImageId",
                table: "Cakes");

            migrationBuilder.DropIndex(
                name: "IX_Cakes_TasteId",
                table: "Cakes");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Cakes");

            migrationBuilder.AlterColumn<int>(
                name: "TasteId",
                table: "Cakes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Cakes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
