using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParaTest.Migrations
{
    public partial class InitialCityzen_WO_Seeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "registry");

            migrationBuilder.CreateSequence(
                name: "cityzen_id_seq",
                schema: "registry",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "cityzen",
                schema: "registry",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "NEXT VALUE FOR registry.cityzen_id_seq"),
                    full_name = table.Column<string>(type: "nvarchar(747)", maxLength: 747, nullable: false),
                    SNILS = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    INN = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    birth_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    death_date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cityzen", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "cz_birth_date",
                schema: "registry",
                table: "cityzen",
                column: "full_name");

            migrationBuilder.CreateIndex(
                name: "cz_death_date",
                schema: "registry",
                table: "cityzen",
                column: "full_name");

            migrationBuilder.CreateIndex(
                name: "cz_full_name",
                schema: "registry",
                table: "cityzen",
                column: "full_name");

            migrationBuilder.CreateIndex(
                name: "cz_inn_uq",
                schema: "registry",
                table: "cityzen",
                column: "INN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "cz_snils_uq",
                schema: "registry",
                table: "cityzen",
                column: "SNILS",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cityzen",
                schema: "registry");

            migrationBuilder.DropSequence(
                name: "cityzen_id_seq",
                schema: "registry");
        }
    }
}
