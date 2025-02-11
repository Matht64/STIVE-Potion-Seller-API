using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STIVE.API.Migrations
{
    /// <inheritdoc />
    public partial class modifPotion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Potion",
                newName: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Potion",
                newName: "Value");
        }
    }
}
