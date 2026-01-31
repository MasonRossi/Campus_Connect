using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusConnect.Models
{
    public class Event
    {
        [Key]
        public string EventId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Category { get; set; }

        // Foreign key to User (Organizer)
        [Required]
        public string CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public User CreatedBy { get; set; }

        // Foreign key to Location
        public string LocationId { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        // Navigation property
        public ICollection<RSVP> RSVPs { get; set; }

        [NotMapped]
        public int RSVPCount => RSVPs?.Count ?? 0;
    }
}
