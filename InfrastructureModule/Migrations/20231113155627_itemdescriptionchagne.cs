using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureModule.Migrations
{
    /// <inheritdoc />
    public partial class itemdescriptionchagne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Item",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "action_on",
                table: "audit_logs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 13, 21, 41, 27, 637, DateTimeKind.Local).AddTicks(1604),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 11, 12, 1, 28, 2, 526, DateTimeKind.Local).AddTicks(4070));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "description",
                keyValue: null,
                column: "description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Item",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "action_on",
                table: "audit_logs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 12, 1, 28, 2, 526, DateTimeKind.Local).AddTicks(4070),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 11, 13, 21, 41, 27, 637, DateTimeKind.Local).AddTicks(1604));
        }
    }
}
