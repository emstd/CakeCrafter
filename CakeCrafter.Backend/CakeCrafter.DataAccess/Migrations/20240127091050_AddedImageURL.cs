using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CakeCrafter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Cakes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Cakes");
        }
    }
}
