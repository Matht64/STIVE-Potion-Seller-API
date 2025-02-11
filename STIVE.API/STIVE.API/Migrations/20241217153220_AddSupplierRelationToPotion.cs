using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STIVE.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplierRelationToPotion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_Potion_IdPotionId",
                table: "Supplier");

            migrationBuilder.RenameColumn(
                name: "IdPotionId",
                table: "Supplier",
                newName: "PotionId");

            migrationBuilder.RenameIndex(
                name: "IX_Supplier_IdPotionId",
                table: "Supplier",
                newName: "IX_Supplier_PotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_Potion_PotionId",
                table: "Supplier",
                column: "PotionId",
                principalTable: "Potion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_Potion_PotionId",
                table: "Supplier");

            migrationBuilder.RenameColumn(
                name: "PotionId",
                table: "Supplier",
                newName: "IdPotionId");

            migrationBuilder.RenameIndex(
                name: "IX_Supplier_PotionId",
                table: "Supplier",
                newName: "IX_Supplier_IdPotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_Potion_IdPotionId",
                table: "Supplier",
                column: "IdPotionId",
                principalTable: "Potion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
