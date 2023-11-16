using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureModule.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdandCompanyinItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "Company",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "action_on",
                table: "audit_logs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 16, 21, 43, 8, 913, DateTimeKind.Local).AddTicks(856),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 11, 13, 21, 53, 56, 263, DateTimeKind.Local).AddTicks(555));

            migrationBuilder.CreateIndex(
                name: "IX_Item_CompanyId",
                table: "Item",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Company_CompanyId",
                table: "Item",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Company_CompanyId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_CompanyId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "Company");

            migrationBuilder.AlterColumn<DateTime>(
                name: "action_on",
                table: "audit_logs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 13, 21, 53, 56, 263, DateTimeKind.Local).AddTicks(555),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 11, 16, 21, 43, 8, 913, DateTimeKind.Local).AddTicks(856));
        }
    }
}
