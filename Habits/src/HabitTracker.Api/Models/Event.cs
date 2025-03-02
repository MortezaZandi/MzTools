using System;
using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Api.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public DateTime EventTime { get; set; }

        // Navigation properties can also be marked as virtual if needed
        public virtual Category Category { get; set; } // Example of a navigation property
    }
} 