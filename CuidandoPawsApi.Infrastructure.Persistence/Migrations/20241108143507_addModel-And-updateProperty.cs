using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CuidandoPawsApi.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addModelAndupdateProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecord",
                table: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Adoptions");

            migrationBuilder.RenameTable(
                name: "MedicalRecords",
                newName: "MedicalRecord");

            migrationBuilder.RenameTable(
                name: "Appoinments",
                newName: "Appoinment");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalRecords_IdPet",
                table: "MedicalRecord",
                newName: "IX_MedicalRecord_IdPet");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Species",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Pets",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "NamePaws",
                table: "Pets",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Pets",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Pets",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Bred",
                table: "Pets",
                type: "character varying(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Treatment",
                table: "MedicalRecord",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Recommendations",
                table: "MedicalRecord",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Appoinment",
                type: "character varying(75)",
                maxLength: 75,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdServiceCatalog",
                table: "Appoinment",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ServiceCatalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameService = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "Text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    Duration = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCatalog", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appoinment_IdServiceCatalog",
                table: "Appoinment",
                column: "IdServiceCatalog");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCatalog_NameService",
                table: "ServiceCatalog",
                column: "NameService",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCatalog",
                table: "Appoinment",
                column: "IdServiceCatalog",
                principalTable: "ServiceCatalog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets",
                table: "MedicalRecord",
                column: "IdPet",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCatalog",
                table: "Appoinment");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets",
                table: "MedicalRecord");

            migrationBuilder.DropTable(
                name: "ServiceCatalog");

            migrationBuilder.DropIndex(
                name: "IX_Appoinment_IdServiceCatalog",
                table: "Appoinment");

            migrationBuilder.DropColumn(
                name: "IdServiceCatalog",
                table: "Appoinment");

            migrationBuilder.RenameTable(
                name: "MedicalRecord",
                newName: "MedicalRecords");

            migrationBuilder.RenameTable(
                name: "Appoinment",
                newName: "Appoinments");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalRecord_IdPet",
                table: "MedicalRecords",
                newName: "IX_MedicalRecords_IdPet");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Species",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Pets",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "NamePaws",
                table: "Pets",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Pets",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Pets",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Bred",
                table: "Pets",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(75)",
                oldMaxLength: 75);

            migrationBuilder.AlterColumn<string>(
                name: "Treatment",
                table: "MedicalRecords",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Recommendations",
                table: "MedicalRecords",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Appoinments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(75)",
                oldMaxLength: 75,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Adoptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPets = table.Column<int>(type: "integer", nullable: false),
                    AdoptionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adoption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets",
                        column: x => x.IdPets,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adoptions_IdPets",
                table: "Adoptions",
                column: "IdPets",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecord",
                table: "MedicalRecords",
                column: "IdPet",
                principalTable: "Pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
