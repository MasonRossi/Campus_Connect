using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusConnect.Migrations
{
    /// <inheritdoc />
    public partial class passwords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "RSVPs",
                keyColumn: "RSVPId",
                keyValue: "r1",
                column: "Timestamp",
                value: new DateTime(2025, 12, 11, 2, 47, 42, 625, DateTimeKind.Local).AddTicks(3741));

            migrationBuilder.UpdateData(
                table: "RSVPs",
                keyColumn: "RSVPId",
                keyValue: "r2",
                column: "Timestamp",
                value: new DateTime(2025, 12, 11, 2, 47, 42, 625, DateTimeKind.Local).AddTicks(3746));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: "u1",
                column: "Password",
                value: "123stu");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: "u2",
                column: "Password",
                value: "123org");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "RSVPs",
                keyColumn: "RSVPId",
                keyValue: "r1",
                column: "Timestamp",
                value: new DateTime(2025, 12, 11, 2, 42, 36, 213, DateTimeKind.Local).AddTicks(340));

            migrationBuilder.UpdateData(
                table: "RSVPs",
                keyColumn: "RSVPId",
                keyValue: "r2",
                column: "Timestamp",
                value: new DateTime(2025, 12, 11, 2, 42, 36, 213, DateTimeKind.Local).AddTicks(345));
        }
    }
}
