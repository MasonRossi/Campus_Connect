using CampusConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusConnect.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<RSVP> RSVPs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed two users: one student, one organizer
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = "u1",
                    Email = "student@example.com",
                    DisplayName = "John Student",
                    Password = "123stu",
                    Role = "Student"
                },
                new User
                {
                    UserId = "u2",
                    Email = "organizer@example.com",
                    DisplayName = "Jane Organizer",
                    Password = "123org",
                    Role = "Organizer"
                }
            );

            modelBuilder.Entity<Event>().HasData(
new Event { EventId = "e1", Title = "Campus Club Fair", Description = "Meet all clubs on campus!", Location = "Conestoga College", Date = new DateTime(2025, 12, 15, 12, 0, 0), CreatedBy = "u2", Category = "Fair", RSVPCount = 0 },
new Event { EventId = "e2", Title = "Math Workshop", Description = "Improve your math skills", Location = "Ontario Tech University", Date = new DateTime(2025, 12, 16, 14, 0, 0), CreatedBy = "u2", Category = "Workshop", RSVPCount = 0 },
new Event { EventId = "e3", Title = "Science Fair", Description = "Showcase science projects", Location = "Conestoga College", Date = new DateTime(2025, 12, 17, 10, 0, 0), CreatedBy = "u2", Category = "Fair", RSVPCount = 0 },
new Event { EventId = "e4", Title = "Art Exhibition", Description = "Display of student artworks", Location = "Sheridan College", Date = new DateTime(2025, 12, 18, 16, 0, 0), CreatedBy = "u2", Category = "Exhibition", RSVPCount = 0 },
new Event { EventId = "e5", Title = "Coding Hackathon", Description = "24-hour coding competition", Location = "Ontario Tech University", Date = new DateTime(2025, 12, 19, 9, 0, 0), CreatedBy = "u2", Category = "Competition", RSVPCount = 0 },
new Event { EventId = "e6", Title = "Music Concert", Description = "Live student bands", Location = "Sheridan College", Date = new DateTime(2025, 12, 20, 19, 0, 0), CreatedBy = "u2", Category = "Concert", RSVPCount = 0 },
new Event { EventId = "e7", Title = "Drama Play", Description = "School theatre production", Location = "Conestoga College", Date = new DateTime(2025, 12, 21, 18, 0, 0), CreatedBy = "u2", Category = "Theatre", RSVPCount = 0 },
new Event { EventId = "e8", Title = "Robotics Workshop", Description = "Build and program robots", Location = "Ontario Tech University", Date = new DateTime(2025, 12, 22, 14, 0, 0), CreatedBy = "u2", Category = "Workshop", RSVPCount = 0 },
new Event { EventId = "e9", Title = "Photography Contest", Description = "Capture campus life", Location = "Sheridan College", Date = new DateTime(2025, 12, 23, 12, 0, 0), CreatedBy = "u2", Category = "Competition", RSVPCount = 0 },
new Event { EventId = "e10", Title = "Sports Day", Description = "Various sporting events", Location = "Conestoga College", Date = new DateTime(2025, 12, 24, 10, 0, 0), CreatedBy = "u2", Category = "Sports", RSVPCount = 0 },
new Event { EventId = "e11", Title = "Career Fair", Description = "Meet potential employers", Location = "Ontario Tech University", Date = new DateTime(2025, 12, 25, 11, 0, 0), CreatedBy = "u2", Category = "Fair", RSVPCount = 0 },
new Event { EventId = "e12", Title = "Chess Tournament", Description = "Test your chess skills", Location = "Sheridan College", Date = new DateTime(2025, 12, 26, 13, 0, 0), CreatedBy = "u2", Category = "Competition", RSVPCount = 0 },
new Event { EventId = "e13", Title = "Environmental Seminar", Description = "Learn about sustainability", Location = "Conestoga College", Date = new DateTime(2025, 12, 27, 15, 0, 0), CreatedBy = "u2", Category = "Seminar", RSVPCount = 0 },
new Event { EventId = "e14", Title = "Volunteer Fair", Description = "Find volunteering opportunities", Location = "Ontario Tech University", Date = new DateTime(2025, 12, 28, 12, 0, 0), CreatedBy = "u2", Category = "Fair", RSVPCount = 0 },
new Event { EventId = "e15", Title = "Language Exchange", Description = "Practice foreign languages", Location = "Sheridan College", Date = new DateTime(2025, 12, 29, 17, 0, 0), CreatedBy = "u2", Category = "Workshop", RSVPCount = 0 },
new Event { EventId = "e16", Title = "Cooking Class", Description = "Learn new recipes", Location = "Conestoga College", Date = new DateTime(2025, 12, 30, 14, 0, 0), CreatedBy = "u2", Category = "Workshop", RSVPCount = 0 },
new Event { EventId = "e17", Title = "Tech Talk", Description = "Latest trends in technology", Location = "Ontario Tech University", Date = new DateTime(2025, 12, 31, 16, 0, 0), CreatedBy = "u2", Category = "Seminar", RSVPCount = 0 },
new Event { EventId = "e18", Title = "Film Screening", Description = "Student short films", Location = "Sheridan College", Date = new DateTime(2026, 1, 1, 18, 0, 0), CreatedBy = "u2", Category = "Exhibition", RSVPCount = 0 },
new Event { EventId = "e19", Title = "Dance Workshop", Description = "Learn new dance routines", Location = "Conestoga College", Date = new DateTime(2026, 1, 2, 15, 0, 0), CreatedBy = "u2", Category = "Workshop", RSVPCount = 0 },
new Event { EventId = "e20", Title = "New Year Party", Description = "Celebrate the new year!", Location = "Ontario Tech University", Date = new DateTime(2026, 1, 3, 20, 0, 0), CreatedBy = "u2", Category = "Party", RSVPCount = 0 },
new Event { EventId = "e21", Title = "Winter Coding Challenge", Description = "Test your coding skills in a fun environment", Location = "Sheridan College", Date = new DateTime(2026, 1, 4, 10, 0, 0), CreatedBy = "u2", Category = "Competition", RSVPCount = 0 },
new Event { EventId = "e22", Title = "Campus Yoga", Description = "Relax and stretch with fellow students", Location = "Conestoga College", Date = new DateTime(2026, 1, 5, 9, 0, 0), CreatedBy = "u2", Category = "Wellness", RSVPCount = 0 },
new Event { EventId = "e23", Title = "Photography Walk", Description = "Explore campus and take photos", Location = "Ontario Tech University", Date = new DateTime(2026, 1, 6, 14, 0, 0), CreatedBy = "u2", Category = "Workshop", RSVPCount = 0 },
new Event { EventId = "e24", Title = "Student Open Mic", Description = "Showcase your talent", Location = "Sheridan College", Date = new DateTime(2026, 1, 7, 19, 0, 0), CreatedBy = "u2", Category = "Concert", RSVPCount = 0 },
new Event { EventId = "e25", Title = "Gaming Tournament", Description = "Battle your friends in popular games", Location = "Conestoga College", Date = new DateTime(2026, 1, 8, 13, 0, 0), CreatedBy = "u2", Category = "Competition", RSVPCount = 0 },
new Event { EventId = "e26", Title = "Science Seminar", Description = "Learn about latest discoveries", Location = "Ontario Tech University", Date = new DateTime(2026, 1, 9, 15, 0, 0), CreatedBy = "u2", Category = "Seminar", RSVPCount = 0 },
new Event { EventId = "e27", Title = "Winter Art Show", Description = "See winter-themed artworks", Location = "Sheridan College", Date = new DateTime(2026, 1, 10, 16, 0, 0), CreatedBy = "u2", Category = "Exhibition", RSVPCount = 0 },
new Event { EventId = "e28", Title = "Entrepreneurship Talk", Description = "Learn tips from successful founders", Location = "Conestoga College", Date = new DateTime(2026, 1, 11, 11, 0, 0), CreatedBy = "u2", Category = "Seminar", RSVPCount = 0 },
new Event { EventId = "e29", Title = "Campus Film Festival", Description = "Student-made short films", Location = "Ontario Tech University", Date = new DateTime(2026, 1, 12, 18, 0, 0), CreatedBy = "u2", Category = "Exhibition", RSVPCount = 0 },
new Event { EventId = "e30", Title = "Winter Dance Party", Description = "Celebrate winter with music and dance", Location = "Sheridan College", Date = new DateTime(2026, 1, 13, 20, 0, 0), CreatedBy = "u2", Category = "Party", RSVPCount = 0 },
new Event { EventId = "e31", Title = "Tech Hackathon", Description = "Collaborate and code in teams", Location = "Conestoga College", Date = new DateTime(2026, 1, 14, 9, 0, 0), CreatedBy = "u2", Category = "Competition", RSVPCount = 0 },
new Event { EventId = "e32", Title = "Student Choir Concert", Description = "Enjoy performances by the student choir", Location = "Ontario Tech University", Date = new DateTime(2026, 1, 15, 19, 0, 0), CreatedBy = "u2", Category = "Concert", RSVPCount = 0 },
new Event { EventId = "e33", Title = "Campus Debate", Description = "Debate on current topics", Location = "Sheridan College", Date = new DateTime(2026, 1, 16, 14, 0, 0), CreatedBy = "u2", Category = "Competition", RSVPCount = 0 },
new Event { EventId = "e34", Title = "Cooking Workshop", Description = "Learn winter recipes", Location = "Conestoga College", Date = new DateTime(2026, 1, 17, 16, 0, 0), CreatedBy = "u2", Category = "Workshop", RSVPCount = 0 },
new Event { EventId = "e35", Title = "Campus Talent Show", Description = "Show off your skills", Location = "Ontario Tech University", Date = new DateTime(2026, 1, 18, 18, 0, 0), CreatedBy = "u2", Category = "Concert", RSVPCount = 0 },
new Event { EventId = "e36", Title = "Winter Wellness Fair", Description = "Learn about health and wellness", Location = "Sheridan College", Date = new DateTime(2026, 1, 19, 12, 0, 0), CreatedBy = "u2", Category = "Fair", RSVPCount = 0 },
new Event { EventId = "e37", Title = "Photography Exhibition", Description = "Campus photography showcase", Location = "Conestoga College", Date = new DateTime(2026, 1, 20, 17, 0, 0), CreatedBy = "u2", Category = "Exhibition", RSVPCount = 0 },
new Event { EventId = "e38", Title = "Winter Coding Bootcamp", Description = "Learn coding skills fast", Location = "Ontario Tech University", Date = new DateTime(2026, 1, 21, 10, 0, 0), CreatedBy = "u2", Category = "Workshop", RSVPCount = 0 },
new Event { EventId = "e39", Title = "Student Winter Gala", Description = "Formal winter celebration", Location = "Sheridan College", Date = new DateTime(2026, 1, 22, 20, 0, 0), CreatedBy = "u2", Category = "Party", RSVPCount = 0 },
new Event { EventId = "e40", Title = "Entrepreneur Workshop", Description = "Learn business and startup skills", Location = "Conestoga College", Date = new DateTime(2026, 1, 23, 14, 0, 0), CreatedBy = "u2", Category = "Workshop", RSVPCount = 0 }

                );

            modelBuilder.Entity<RSVP>().HasData(
        new RSVP
        {
            RSVPId = "r1",
            EventId = "e1", // Campus Club Fair
            UserId = "u1",  // Student user
            Timestamp = DateTime.Now
        },
        new RSVP
        {
            RSVPId = "r2",
            EventId = "e2", // Math Workshop
            UserId = "u1",  // Student user
            Timestamp = DateTime.Now
        }
    );
        }
    }
}
