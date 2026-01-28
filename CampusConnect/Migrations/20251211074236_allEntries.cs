using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CampusConnect.Migrations
{
    /// <inheritdoc />
    public partial class allEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Category", "CreatedBy", "Date", "Description", "Location", "RSVPCount", "Title" },
                values: new object[,]
                {
                    { "e1", "Fair", "u2", new DateTime(2025, 12, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), "Meet all clubs on campus!", "Main Gym", 0, "Campus Club Fair" },
                    { "e10", "Sports", "u2", new DateTime(2025, 12, 24, 10, 0, 0, 0, DateTimeKind.Unspecified), "Various sporting events", "Stadium", 0, "Sports Day" },
                    { "e11", "Fair", "u2", new DateTime(2025, 12, 25, 11, 0, 0, 0, DateTimeKind.Unspecified), "Meet potential employers", "Conference Hall", 0, "Career Fair" },
                    { "e12", "Competition", "u2", new DateTime(2025, 12, 26, 13, 0, 0, 0, DateTimeKind.Unspecified), "Test your chess skills", "Room 202", 0, "Chess Tournament" },
                    { "e13", "Seminar", "u2", new DateTime(2025, 12, 27, 15, 0, 0, 0, DateTimeKind.Unspecified), "Learn about sustainability", "Lecture Hall 1", 0, "Environmental Seminar" },
                    { "e14", "Fair", "u2", new DateTime(2025, 12, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), "Find volunteering opportunities", "Lobby", 0, "Volunteer Fair" },
                    { "e15", "Workshop", "u2", new DateTime(2025, 12, 29, 17, 0, 0, 0, DateTimeKind.Unspecified), "Practice foreign languages", "Room 303", 0, "Language Exchange" },
                    { "e16", "Workshop", "u2", new DateTime(2025, 12, 30, 14, 0, 0, 0, DateTimeKind.Unspecified), "Learn new recipes", "Kitchen Lab", 0, "Cooking Class" },
                    { "e17", "Seminar", "u2", new DateTime(2025, 12, 31, 16, 0, 0, 0, DateTimeKind.Unspecified), "Latest trends in technology", "Lecture Hall 2", 0, "Tech Talk" },
                    { "e18", "Exhibition", "u2", new DateTime(2026, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), "Student short films", "Auditorium", 0, "Film Screening" },
                    { "e19", "Workshop", "u2", new DateTime(2026, 1, 2, 15, 0, 0, 0, DateTimeKind.Unspecified), "Learn new dance routines", "Dance Studio", 0, "Dance Workshop" },
                    { "e2", "Workshop", "u2", new DateTime(2025, 12, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), "Improve your math skills", "Room 101", 0, "Math Workshop" },
                    { "e20", "Party", "u2", new DateTime(2026, 1, 3, 20, 0, 0, 0, DateTimeKind.Unspecified), "Celebrate the new year!", "Campus Center", 0, "New Year Party" },
                    { "e3", "Fair", "u2", new DateTime(2025, 12, 17, 10, 0, 0, 0, DateTimeKind.Unspecified), "Showcase science projects", "Science Hall", 0, "Science Fair" },
                    { "e4", "Exhibition", "u2", new DateTime(2025, 12, 18, 16, 0, 0, 0, DateTimeKind.Unspecified), "Display of student artworks", "Art Gallery", 0, "Art Exhibition" },
                    { "e5", "Competition", "u2", new DateTime(2025, 12, 19, 9, 0, 0, 0, DateTimeKind.Unspecified), "24-hour coding competition", "Lab 2", 0, "Coding Hackathon" },
                    { "e6", "Concert", "u2", new DateTime(2025, 12, 20, 19, 0, 0, 0, DateTimeKind.Unspecified), "Live student bands", "Auditorium", 0, "Music Concert" },
                    { "e7", "Theatre", "u2", new DateTime(2025, 12, 21, 18, 0, 0, 0, DateTimeKind.Unspecified), "School theatre production", "Main Stage", 0, "Drama Play" },
                    { "e8", "Workshop", "u2", new DateTime(2025, 12, 22, 14, 0, 0, 0, DateTimeKind.Unspecified), "Build and program robots", "Engineering Lab", 0, "Robotics Workshop" },
                    { "e9", "Competition", "u2", new DateTime(2025, 12, 23, 12, 0, 0, 0, DateTimeKind.Unspecified), "Capture campus life", "Online", 0, "Photography Contest" }
                });

            migrationBuilder.InsertData(
                table: "RSVPs",
                columns: new[] { "RSVPId", "EventId", "Timestamp", "UserId" },
                values: new object[,]
                {
                    { "r1", "e1", new DateTime(2025, 12, 11, 2, 42, 36, 213, DateTimeKind.Local).AddTicks(340), "u1" },
                    { "r2", "e2", new DateTime(2025, 12, 11, 2, 42, 36, 213, DateTimeKind.Local).AddTicks(345), "u1" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DisplayName", "Email", "Role" },
                values: new object[,]
                {
                    { "u1", "John Student", "student@example.com", "Student" },
                    { "u2", "Jane Organizer", "organizer@example.com", "Organizer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e1");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e10");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e11");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e12");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e13");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e14");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e15");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e16");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e17");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e18");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e19");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e2");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e20");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e3");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e4");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e5");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e6");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e7");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e8");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: "e9");

            migrationBuilder.DeleteData(
                table: "RSVPs",
                keyColumn: "RSVPId",
                keyValue: "r1");

            migrationBuilder.DeleteData(
                table: "RSVPs",
                keyColumn: "RSVPId",
                keyValue: "r2");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: "u1");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: "u2");
        }
    }
}
