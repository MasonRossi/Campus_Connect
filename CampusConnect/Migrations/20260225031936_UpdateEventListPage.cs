using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusConnect.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventListPage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "event1",
                column: "Date",
                value: new DateTime(2026, 3, 4, 3, 19, 36, 431, DateTimeKind.Utc).AddTicks(2239));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "event2",
                column: "Date",
                value: new DateTime(2026, 3, 7, 3, 19, 36, 431, DateTimeKind.Utc).AddTicks(2247));

            migrationBuilder.UpdateData(
                table: "RSVPs",
                keyColumn: "RSVPId",
                keyValue: "rsvp1",
                column: "Timestamp",
                value: new DateTime(2026, 2, 25, 3, 19, 36, 431, DateTimeKind.Utc).AddTicks(2272));

            migrationBuilder.UpdateData(
                table: "RSVPs",
                keyColumn: "RSVPId",
                keyValue: "rsvp2",
                column: "Timestamp",
                value: new DateTime(2026, 2, 25, 3, 19, 36, 431, DateTimeKind.Utc).AddTicks(2274));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "event1",
                column: "Date",
                value: new DateTime(2026, 2, 7, 9, 7, 9, 700, DateTimeKind.Utc).AddTicks(5303));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "event2",
                column: "Date",
                value: new DateTime(2026, 2, 10, 9, 7, 9, 700, DateTimeKind.Utc).AddTicks(5314));

            migrationBuilder.UpdateData(
                table: "RSVPs",
                keyColumn: "RSVPId",
                keyValue: "rsvp1",
                column: "Timestamp",
                value: new DateTime(2026, 1, 31, 9, 7, 9, 700, DateTimeKind.Utc).AddTicks(5342));

            migrationBuilder.UpdateData(
                table: "RSVPs",
                keyColumn: "RSVPId",
                keyValue: "rsvp2",
                column: "Timestamp",
                value: new DateTime(2026, 1, 31, 9, 7, 9, 700, DateTimeKind.Utc).AddTicks(5348));
        }
    }
}
