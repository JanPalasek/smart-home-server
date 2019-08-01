using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHome.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BatteryPowerSourceType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BatteryType = table.Column<int>(nullable: false),
                    MinimumVoltage = table.Column<double>(nullable: false),
                    MaximumVoltage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatteryPowerSourceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UnitTypeId = table.Column<long>(nullable: false),
                    BatteryPowerSourceTypeId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Unit_BatteryPowerSourceType_BatteryPowerSourceTypeId",
                        column: x => x.BatteryPowerSourceTypeId,
                        principalTable: "BatteryPowerSourceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Unit_UnitType_UnitTypeId",
                        column: x => x.UnitTypeId,
                        principalTable: "UnitType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BatteryMeasurement",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UnitId = table.Column<long>(nullable: false),
                    MeasurementDateTime = table.Column<DateTime>(nullable: false),
                    Voltage = table.Column<double>(nullable: false),
                    BatteryPowerSourceTypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatteryMeasurement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatteryMeasurement_BatteryPowerSourceType_BatteryPowerSource~",
                        column: x => x.BatteryPowerSourceTypeId,
                        principalTable: "BatteryPowerSourceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BatteryMeasurement_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HumidityMeasurement",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UnitId = table.Column<long>(nullable: false),
                    MeasurementDateTime = table.Column<DateTime>(nullable: false),
                    Humidity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumidityMeasurement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HumidityMeasurement_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemperatureMeasurement",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UnitId = table.Column<long>(nullable: false),
                    MeasurementDateTime = table.Column<DateTime>(nullable: false),
                    Temperature = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureMeasurement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemperatureMeasurement_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BatteryMeasurement_BatteryPowerSourceTypeId",
                table: "BatteryMeasurement",
                column: "BatteryPowerSourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BatteryMeasurement_MeasurementDateTime",
                table: "BatteryMeasurement",
                column: "MeasurementDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_BatteryMeasurement_UnitId",
                table: "BatteryMeasurement",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_HumidityMeasurement_MeasurementDateTime",
                table: "HumidityMeasurement",
                column: "MeasurementDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_HumidityMeasurement_UnitId",
                table: "HumidityMeasurement",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureMeasurement_MeasurementDateTime",
                table: "TemperatureMeasurement",
                column: "MeasurementDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureMeasurement_UnitId",
                table: "TemperatureMeasurement",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_BatteryPowerSourceTypeId",
                table: "Unit",
                column: "BatteryPowerSourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_UnitTypeId",
                table: "Unit",
                column: "UnitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitType_Name",
                table: "UnitType",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatteryMeasurement");

            migrationBuilder.DropTable(
                name: "HumidityMeasurement");

            migrationBuilder.DropTable(
                name: "TemperatureMeasurement");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "BatteryPowerSourceType");

            migrationBuilder.DropTable(
                name: "UnitType");
        }
    }
}
