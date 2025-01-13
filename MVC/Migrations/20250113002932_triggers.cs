using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC.Migrations
{
    /// <inheritdoc />
    public partial class triggers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION actualizare_angajat()
                RETURNS TRIGGER AS $$
                DECLARE
                    v_CAS_Percent INTEGER;
                    v_CASS_Percent INTEGER;
                    v_Impozit_Percent INTEGER;
                BEGIN
                    -- Setarea valorilor procentuale din ProcentajValorile
                    SELECT ""CAS"", ""CASS"", ""Impozit""
                    INTO v_CAS_Percent, v_CASS_Percent, v_Impozit_Percent
                    FROM ""Taxe""
                    LIMIT 1;

                    -- Calcularea valorilor pentru angajat
                    NEW.""TotalBrut"" := ROUND(NEW.""SalarBaza"" + (NEW.""SalarBaza"" * NEW.""SporProcentual"" / 100.0) + NEW.""PremiiBrute"");
                    NEW.""CAS"" := ROUND(NEW.""TotalBrut"" * v_CAS_Percent / 100.0);
                    NEW.""CASS"" := ROUND(NEW.""TotalBrut"" * v_CASS_Percent / 100.0);
                    NEW.""BrutImpozitabil"" := ROUND(NEW.""TotalBrut"" - NEW.""CAS"" - NEW.""CASS"");
                    NEW.""Impozit"" := ROUND(NEW.""BrutImpozitabil"" * v_Impozit_Percent / 100.0);
                    NEW.""ViratCard"" := ROUND(NEW.""BrutImpozitabil"" - NEW.""Impozit"" - NEW.""Retineri"");

                    RETURN NEW;
                END;
                $$ LANGUAGE plpgsql;
            ");
            
            migrationBuilder.Sql(@"
                CREATE or replace TRIGGER trg_update_onInsert_onUpdaate
                BEFORE INSERT OR UPDATE ON ""Angajati""
                FOR EACH ROW
                EXECUTE FUNCTION actualizare_angajat();
            ");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION actualizeaza_tot_angajati()
                RETURNS TRIGGER AS $$
                BEGIN
                    -- Actualizarea tuturor angajaților pentru recalculare
                    UPDATE ""Angajati""
                    SET ""SalarBaza"" = ""SalarBaza""; -- declanșează recalcularea prin triggerul trg_update_onInsert_onUpdaate

                    RETURN NEW;
                END;
                $$ LANGUAGE plpgsql;
            ");
            
            migrationBuilder.Sql(@"
                CREATE OR REPLACE TRIGGER trg_update_angajati
                AFTER UPDATE ON ""Taxe""
                FOR EACH ROW
                EXECUTE FUNCTION actualizeaza_tot_angajati();
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP TRIGGER trg_update_angajati ON ""Taxe"";
            ");
            
            migrationBuilder.Sql(@"
                DROP FUNCTION actualizeaza_tot_angajati;
            ");
            
            migrationBuilder.Sql(@"
                DROP TRIGGER trg_update_onInsert_onUpdaate ON ""Angajati"";
            ");
            
            migrationBuilder.Sql(@"
                DROP FUNCTION actualizare_angajat;
            ");
        }
    }
}
