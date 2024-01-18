using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Popasu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParametersValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametersValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WindRoses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WindRoses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Climates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WindRoseId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Climates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Climates_WindRoses_WindRoseId",
                        column: x => x.WindRoseId,
                        principalTable: "WindRoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false),
                    ParameterValueId = table.Column<Guid>(type: "uuid", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    ClimateId = table.Column<Guid>(type: "uuid", nullable: true),
                    WindRoseId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parameters_Climates_ClimateId",
                        column: x => x.ClimateId,
                        principalTable: "Climates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Parameters_ParametersValues_ParameterValueId",
                        column: x => x.ParameterValueId,
                        principalTable: "ParametersValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parameters_WindRoses_WindRoseId",
                        column: x => x.WindRoseId,
                        principalTable: "WindRoses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Climates_WindRoseId",
                table: "Climates",
                column: "WindRoseId");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_ClimateId",
                table: "Parameters",
                column: "ClimateId");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_ParameterValueId",
                table: "Parameters",
                column: "ParameterValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_WindRoseId",
                table: "Parameters",
                column: "WindRoseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropTable(
                name: "Climates");

            migrationBuilder.DropTable(
                name: "ParametersValues");

            migrationBuilder.DropTable(
                name: "WindRoses");
        }
    }
}
