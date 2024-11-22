using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CuidandoPawsApi.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelSpecies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EntryOfSpecie",
                table: "Species",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryOfSpecie",
                table: "Species");
        }
    }
}
