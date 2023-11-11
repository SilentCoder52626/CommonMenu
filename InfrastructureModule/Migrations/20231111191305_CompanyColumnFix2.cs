using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureModule.Migrations
{
    /// <inheritdoc />
    public partial class CompanyColumnFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Attachment_logo",
                table: "Company");

            migrationBuilder.AlterColumn<int>(
                name: "logo",
                table: "Company",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "action_on",
                table: "audit_logs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 12, 0, 58, 5, 202, DateTimeKind.Local).AddTicks(3874),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 11, 12, 0, 44, 22, 906, DateTimeKind.Local).AddTicks(1137));

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Attachment_logo",
                table: "Company",
                column: "logo",
                principalTable: "Attachment",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Attachment_logo",
                table: "Company");

            migrationBuilder.AlterColumn<int>(
                name: "logo",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "action_on",
                table: "audit_logs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 12, 0, 44, 22, 906, DateTimeKind.Local).AddTicks(1137),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 11, 12, 0, 58, 5, 202, DateTimeKind.Local).AddTicks(3874));

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Attachment_logo",
                table: "Company",
                column: "logo",
                principalTable: "Attachment",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
