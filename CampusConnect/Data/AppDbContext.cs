using Microsoft.EntityFrameworkCore;
using CampusConnect.Models;
using System;

namespace CampusConnect.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<RSVP> RSVPs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -----------------------------
            // RSVPs → Users / Events
            // -----------------------------
            modelBuilder.Entity<RSVP>()
                .HasOne(r => r.User)
                .WithMany(u => u.RSVPs)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict); // prevent multiple cascade paths

            modelBuilder.Entity<RSVP>()
                .HasOne(r => r.Event)
                .WithMany(e => e.RSVPs)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade); // allow cascading deletes from Events

            // -----------------------------
            // Users Seed
            // -----------------------------
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = "user1",
                    DisplayName = "Alice",
                    Email = "alice@student.edu",
                    Password = "password1",
                    Role = "Student"
                },
                new User
                {
                    UserId = "user2",
                    DisplayName = "Bob",
                    Email = "bob@student.edu",
                    Password = "password2",
                    Role = "Student"
                },
                new User
                {
                    UserId = "org1",
                    DisplayName = "Prof. Smith",
                    Email = "smith@campus.edu",
                    Password = "password3",
                    Role = "Organizer"
                }
            );

            // -----------------------------
            // Locations Seed
            // -----------------------------
            modelBuilder.Entity<Location>().HasData(
                new Location
                {
                    LocationId = "loc1",
                    Name = "Main Auditorium",
                    Description = "The large auditorium in the main building."
                },
                new Location
                {
                    LocationId = "loc2",
                    Name = "Student Center",
                    Description = "Central hub for student activities."
                }
            );

            // -----------------------------
            // Events Seed
            // -----------------------------
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    EventId = "event1",
                    Title = "Orientation Day",
                    Description = "Welcome new students! Meet your peers and faculty.",
                    Date = DateTime.UtcNow.AddDays(7),
                    Category = "Social",
                    CreatedById = "org1",
                    LocationId = "loc1"
                },
                new Event
                {
                    EventId = "event2",
                    Title = "Coding Workshop",
                    Description = "Learn to code in C# and build small projects.",
                    Date = DateTime.UtcNow.AddDays(10),
                    Category = "Educational",
                    CreatedById = "org1",
                    LocationId = "loc2"
                }
            );

            // -----------------------------
            // RSVPs Seed
            // -----------------------------
            modelBuilder.Entity<RSVP>().HasData(
                new RSVP
                {
                    RSVPId = "rsvp1",
                    EventId = "event1",
                    UserId = "user1",
                    Timestamp = DateTime.UtcNow
                },
                new RSVP
                {
                    RSVPId = "rsvp2",
                    EventId = "event2",
                    UserId = "user2",
                    Timestamp = DateTime.UtcNow
                }
            );
        }

    }
}
