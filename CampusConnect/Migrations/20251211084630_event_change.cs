using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CampusConnect.Migrations
{
    /// <inheritdoc />
    public partial class event_change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e1",
                column: "Location",
                value: "Conestoga College");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e10",
                column: "Location",
                value: "Conestoga College");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e11",
                column: "Location",
                value: "Ontario Tech University");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e12",
                column: "Location",
                value: "Sheridan College");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e13",
                column: "Location",
                value: "Conestoga College");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e14",
                column: "Location",
                value: "Ontario Tech University");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e15",
                column: "Location",
                value: "Sheridan College");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e16",
                column: "Location",
                value: "Conestoga College");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e17",
                column: "Location",
                value: "Ontario Tech University");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e18",
                column: "Location",
                value: "Sheridan College");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e19",
                column: "Location",
                value: "Conestoga College");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e2",
                column: "Location",
                value: "Ontario Tech University");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e20",
                column: "Location",
                value: "Ontario Tech University");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e3",
                column: "Location",
                value: "Conestoga College");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e4",
                column: "Location",
                value: "Sheridan College");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e5",
                column: "Location",
                value: "Ontario Tech University");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e6",
                column: "Location",
                value: "Sheridan College");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e7",
                column: "Location",
                value: "Conestoga College");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e8",
                column: "Location",
                value: "Ontario Tech University");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e9",
                column: "Location",
                value: "Sheridan College");

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Category", "CreatedBy", "Date", "Description", "Location", "RSVPCount", "Title" },
                values: new object[,]
                {
                    { "e21", "Competition", "u2", new DateTime(2026, 1, 4, 10, 0, 0, 0, DateTimeKind.Unspecified), "Test your coding skills in a fun environment", "Sheridan College", 0, "Winter Coding Challenge" },
                    { "e22", "Wellness", "u2", new DateTime(2026, 1, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), "Relax and stretch with fellow students", "Conestoga College", 0, "Campus Yoga" },
                    { "e23", "Workshop", "u2", new DateTime(2026, 1, 6, 14, 0, 0, 0, DateTimeKind.Unspecified), "Explore campus and take photos", "Ontario Tech University", 0, "Photography Walk" },
                    { "e24", "Concert", "u2", new DateTime(2026, 1, 7, 19, 0, 0, 0, DateTimeKind.Unspecified), "Showcase your talent", "Sheridan College", 0, "Student Open Mic" },
                    { "e25", "Competition", "u2", new DateTime(2026, 1, 8, 13, 0, 0, 0, DateTimeKind.Unspecified), "Battle your friends in popular games", "Conestoga College", 0, "Gaming Tournament" },
                    { "e26", "Seminar", "u2", new DateTime(2026, 1, 9, 15, 0, 0, 0, DateTimeKind.Unspecified), "Learn about latest discoveries", "Ontario Tech University", 0, "Science Seminar" },
                    { "e27", "Exhibition", "u2", new DateTime(2026, 1, 10, 16, 0, 0, 0, DateTimeKind.Unspecified), "See winter-themed artworks", "Sheridan College", 0, "Winter Art Show" },
                    { "e28", "Seminar", "u2", new DateTime(2026, 1, 11, 11, 0, 0, 0, DateTimeKind.Unspecified), "Learn tips from successful founders", "Conestoga College", 0, "Entrepreneurship Talk" },
                    { "e29", "Exhibition", "u2", new DateTime(2026, 1, 12, 18, 0, 0, 0, DateTimeKind.Unspecified), "Student-made short films", "Ontario Tech University", 0, "Campus Film Festival" },
                    { "e30", "Party", "u2", new DateTime(2026, 1, 13, 20, 0, 0, 0, DateTimeKind.Unspecified), "Celebrate winter with music and dance", "Sheridan College", 0, "Winter Dance Party" },
                    { "e31", "Competition", "u2", new DateTime(2026, 1, 14, 9, 0, 0, 0, DateTimeKind.Unspecified), "Collaborate and code in teams", "Conestoga College", 0, "Tech Hackathon" },
                    { "e32", "Concert", "u2", new DateTime(2026, 1, 15, 19, 0, 0, 0, DateTimeKind.Unspecified), "Enjoy performances by the student choir", "Ontario Tech University", 0, "Student Choir Concert" },
                    { "e33", "Competition", "u2", new DateTime(2026, 1, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), "Debate on current topics", "Sheridan College", 0, "Campus Debate" },
                    { "e34", "Workshop", "u2", new DateTime(2026, 1, 17, 16, 0, 0, 0, DateTimeKind.Unspecified), "Learn winter recipes", "Conestoga College", 0, "Cooking Workshop" },
                    { "e35", "Concert", "u2", new DateTime(2026, 1, 18, 18, 0, 0, 0, DateTimeKind.Unspecified), "Show off your skills", "Ontario Tech University", 0, "Campus Talent Show" },
                    { "e36", "Fair", "u2", new DateTime(2026, 1, 19, 12, 0, 0, 0, DateTimeKind.Unspecified), "Learn about health and wellness", "Sheridan College", 0, "Winter Wellness Fair" },
                    { "e37", "Exhibition", "u2", new DateTime(2026, 1, 20, 17, 0, 0, 0, DateTimeKind.Unspecified), "Campus photography showcase", "Conestoga College", 0, "Photography Exhibition" },
                    { "e38", "Workshop", "u2", new DateTime(2026, 1, 21, 10, 0, 0, 0, DateTimeKind.Unspecified), "Learn coding skills fast", "Ontario Tech University", 0, "Winter Coding Bootcamp" },
                    { "e39", "Party", "u2", new DateTime(2026, 1, 22, 20, 0, 0, 0, DateTimeKind.Unspecified), "Formal winter celebration", "Sheridan College", 0, "Student Winter Gala" },
                    { "e40", "Workshop", "u2", new DateTime(2026, 1, 23, 14, 0, 0, 0, DateTimeKind.Unspecified), "Learn business and startup skills", "Conestoga College", 0, "Entrepreneur Workshop" }
                });

            migrationBuilder.UpdateData(
                table: "RSVPs",
                keyColumn: "RSVPId",
                keyValue: "r1",
                column: "Timestamp",
                value: new DateTime(2025, 12, 11, 3, 46, 29, 607, DateTimeKind.Local).AddTicks(5314));

            migrationBuilder.UpdateData(
                table: "RSVPs",
                keyColumn: "RSVPId",
                keyValue: "r2",
                column: "Timestamp",
                value: new DateTime(2025, 12, 11, 3, 46, 29, 607, DateTimeKind.Local).AddTicks(5365));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e21");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e22");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e23");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e24");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e25");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e26");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e27");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e28");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e29");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e30");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e31");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e32");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e33");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e34");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e35");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e36");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e37");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e38");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e39");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e40");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e1",
                column: "Location",
                value: "Main Gym");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e10",
                column: "Location",
                value: "Stadium");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e11",
                column: "Location",
                value: "Conference Hall");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e12",
                column: "Location",
                value: "Room 202");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e13",
                column: "Location",
                value: "Lecture Hall 1");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e14",
                column: "Location",
                value: "Lobby");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e15",
                column: "Location",
                value: "Room 303");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e16",
                column: "Location",
                value: "Kitchen Lab");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e17",
                column: "Location",
                value: "Lecture Hall 2");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e18",
                column: "Location",
                value: "Auditorium");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e19",
                column: "Location",
                value: "Dance Studio");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e2",
                column: "Location",
                value: "Room 101");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e20",
                column: "Location",
                value: "Campus Center");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e3",
                column: "Location",
                value: "Science Hall");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e4",
                column: "Location",
                value: "Art Gallery");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e5",
                column: "Location",
                value: "Lab 2");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e6",
                column: "Location",
                value: "Auditorium");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e7",
                column: "Location",
                value: "Main Stage");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e8",
                column: "Location",
                value: "Engineering Lab");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e9",
                column: "Location",
                value: "Online");

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
        }
    }
}
