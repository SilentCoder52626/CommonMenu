using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureModule.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyInMenuCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "MenuCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "action_on",
                table: "audit_logs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 17, 21, 5, 47, 95, DateTimeKind.Local).AddTicks(6413),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 11, 16, 21, 43, 8, 913, DateTimeKind.Local).AddTicks(856));

            migrationBuilder.CreateIndex(
                name: "IX_MenuCategory_CompanyId",
                table: "MenuCategory",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCategory_Company_CompanyId",
                table: "MenuCategory",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuCategory_Company_CompanyId",
                table: "MenuCategory");

            migrationBuilder.DropIndex(
                name: "IX_MenuCategory_CompanyId",
                table: "MenuCategory");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "MenuCategory");

            migrationBuilder.AlterColumn<DateTime>(
                name: "action_on",
                table: "audit_logs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 16, 21, 43, 8, 913, DateTimeKind.Local).AddTicks(856),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 11, 17, 21, 5, 47, 95, DateTimeKind.Local).AddTicks(6413));
        }
    }
}
