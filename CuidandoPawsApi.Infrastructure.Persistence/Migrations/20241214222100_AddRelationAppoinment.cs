using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CuidandoPawsApi.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationAppoinment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCatalog",
                table: "Appoinment");

            migrationBuilder.AddForeignKey(
                name: "Fk_Appoinment",
                table: "Appoinment",
                column: "IdServiceCatalog",
                principalTable: "ServiceCatalog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Fk_Appoinment",
                table: "Appoinment");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCatalog",
                table: "Appoinment",
                column: "IdServiceCatalog",
                principalTable: "ServiceCatalog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
