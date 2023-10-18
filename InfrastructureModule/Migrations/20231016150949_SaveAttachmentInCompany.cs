using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureModule.Migrations
{
    /// <inheritdoc />
    public partial class SaveAttachmentInCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "logo",
                table: "Company",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "action_on",
                table: "audit_logs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 16, 20, 54, 49, 147, DateTimeKind.Local).AddTicks(7656),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 10, 15, 23, 53, 34, 242, DateTimeKind.Local).AddTicks(546));

            migrationBuilder.CreateIndex(
                name: "IX_Company_logo",
                table: "Company",
                column: "logo");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Attachment_logo",
                table: "Company",
                column: "logo",
                principalTable: "Attachment",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Attachment_logo",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_logo",
                table: "Company");

            migrationBuilder.AlterColumn<string>(
                name: "logo",
                table: "Company",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "action_on",
                table: "audit_logs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 15, 23, 53, 34, 242, DateTimeKind.Local).AddTicks(546),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 10, 16, 20, 54, 49, 147, DateTimeKind.Local).AddTicks(7656));
        }
    }
}
