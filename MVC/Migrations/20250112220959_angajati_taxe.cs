using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MVC.Migrations
{
    /// <inheritdoc />
    public partial class angajati_taxe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Angajati",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nume = table.Column<string>(type: "text", nullable: false),
                    Prenume = table.Column<string>(type: "text", nullable: false),
                    Functie = table.Column<string>(type: "text", nullable: false),
                    SalarBaza = table.Column<int>(type: "integer", nullable: false),
                    SporProcentual = table.Column<double>(type: "double precision", nullable: false),
                    PremiiBrute = table.Column<int>(type: "integer", nullable: false),
                    TotalBrut = table.Column<int>(type: "integer", nullable: false),
                    BrutImpozitabil = table.Column<int>(type: "integer", nullable: false),
                    Impozit = table.Column<int>(type: "integer", nullable: false),
                    CAS = table.Column<int>(type: "integer", nullable: false),
                    CASS = table.Column<int>(type: "integer", nullable: false),
                    Retineri = table.Column<int>(type: "integer", nullable: false),
                    ViratCard = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Angajati", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Taxe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CAS = table.Column<double>(type: "double precision", nullable: false),
                    CASS = table.Column<double>(type: "double precision", nullable: false),
                    Impozit = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxe", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Angajati");

            migrationBuilder.DropTable(
                name: "Taxe");
        }
    }
}
