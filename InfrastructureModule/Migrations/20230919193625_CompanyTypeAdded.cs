using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureModule.Migrations
{
    /// <inheritdoc />
    public partial class CompanyTypeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "action_on",
                table: "audit_logs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 20, 1, 21, 25, 135, DateTimeKind.Local).AddTicks(9501),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 9, 2, 0, 31, 46, 934, DateTimeKind.Local).AddTicks(2902));

            migrationBuilder.CreateTable(
                name: "CompanyTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTypes", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyTypes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "action_on",
                table: "audit_logs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 2, 0, 31, 46, 934, DateTimeKind.Local).AddTicks(2902),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 9, 20, 1, 21, 25, 135, DateTimeKind.Local).AddTicks(9501));
        }
    }
}
