using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CuidandoPawsApi.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addNewConfigurationModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    IsAvaible = table.Column<bool>(type: "boolean", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCatalog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    EntryOfSpecie = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appoinment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReservationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    IdServiceCatalog = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appoinment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCatalog",
                        column: x => x.IdServiceCatalog,
                        principalTable: "ServiceCatalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NamePaws = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Bred = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Color = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    AdoptionStatus = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateOfEntry = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SpeciesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Species",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Treatment = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Recommendations = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    IdPet = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets",
                        column: x => x.IdPet,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appoinment_IdServiceCatalog",
                table: "Appoinment",
                column: "IdServiceCatalog");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecord_IdPet",
                table: "MedicalRecord",
                column: "IdPet",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_SpeciesId",
                table: "Pets",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCatalog_NameService",
                table: "ServiceCatalog",
                column: "NameService",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appoinment");

            migrationBuilder.DropTable(
                name: "MedicalRecord");

            migrationBuilder.DropTable(
                name: "ServiceCatalog");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Species");
        }
    }
}
