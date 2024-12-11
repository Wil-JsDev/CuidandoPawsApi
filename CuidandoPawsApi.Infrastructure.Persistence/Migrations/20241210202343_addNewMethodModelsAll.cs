using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CuidandoPawsApi.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addNewMethodModelsAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER SEQUENCE \"Appoinment_Id_seq\" RESTART WITH 10000;");
            migrationBuilder.Sql("ALTER SEQUENCE \"MedicalRecord_Id_seq\" RESTART WITH 10000;");
            migrationBuilder.Sql("ALTER SEQUENCE \"Species_Id_seq\" RESTART WITH 10000;");
            migrationBuilder.Sql("ALTER SEQUENCE \"Pets_Id_seq\" RESTART WITH 10000;");
            migrationBuilder.Sql("ALTER SEQUENCE \"ServiceCatalog_Id_seq\" RESTART WITH 10000;");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER SEQUENCE \"Appoinment_Id_seq\" RESTART WITH 10000;");
            migrationBuilder.Sql("ALTER SEQUENCE \"MedicalRecord_Id_seq\" RESTART WITH 10000;");
            migrationBuilder.Sql("ALTER SEQUENCE \"Species_Id_seq\" RESTART WITH 10000;");
            migrationBuilder.Sql("ALTER SEQUENCE \"Pets_Id_seq\" RESTART WITH 10000;");
            migrationBuilder.Sql("ALTER SEQUENCE \"ServiceCatalog_Id_seq\" RESTART WITH 10000;");
        }
    }
}
