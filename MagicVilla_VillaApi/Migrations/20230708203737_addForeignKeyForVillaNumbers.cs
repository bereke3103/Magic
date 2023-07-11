using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaApi.Migrations
{
    /// <inheritdoc />
    public partial class addForeignKeyForVillaNumbers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VilldID",
                table: "VillaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 9, 2, 37, 37, 402, DateTimeKind.Local).AddTicks(8761));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 9, 2, 37, 37, 402, DateTimeKind.Local).AddTicks(8774));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 9, 2, 37, 37, 402, DateTimeKind.Local).AddTicks(8776));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 9, 2, 37, 37, 402, DateTimeKind.Local).AddTicks(8778));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 9, 2, 37, 37, 402, DateTimeKind.Local).AddTicks(8780));

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumbers_VilldID",
                table: "VillaNumbers",
                column: "VilldID");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumbers_Villas_VilldID",
                table: "VillaNumbers",
                column: "VilldID",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumbers_Villas_VilldID",
                table: "VillaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_VillaNumbers_VilldID",
                table: "VillaNumbers");

            migrationBuilder.DropColumn(
                name: "VilldID",
                table: "VillaNumbers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 9, 2, 7, 58, 839, DateTimeKind.Local).AddTicks(6213));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 9, 2, 7, 58, 839, DateTimeKind.Local).AddTicks(6226));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 9, 2, 7, 58, 839, DateTimeKind.Local).AddTicks(6228));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 9, 2, 7, 58, 839, DateTimeKind.Local).AddTicks(6230));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 9, 2, 7, 58, 839, DateTimeKind.Local).AddTicks(6231));
        }
    }
}
