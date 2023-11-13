using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureModule.Migrations
{
    /// <inheritdoc />
    public partial class itemImageIdRemoveRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Attachment_ImageId",
                table: "Item");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Item",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "action_on",
                table: "audit_logs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 11, 13, 21, 53, 56, 263, DateTimeKind.Local).AddTicks(555),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 11, 13, 21, 41, 27, 637, DateTimeKind.Local).AddTicks(1604));

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Attachment_ImageId",
                table: "Item",
                column: "ImageId",
                principalTable: "Attachment",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Attachment_ImageId",
                table: "Item");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Item",
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
                defaultValue: new DateTime(2023, 11, 13, 21, 41, 27, 637, DateTimeKind.Local).AddTicks(1604),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 11, 13, 21, 53, 56, 263, DateTimeKind.Local).AddTicks(555));

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Attachment_ImageId",
                table: "Item",
                column: "ImageId",
                principalTable: "Attachment",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
