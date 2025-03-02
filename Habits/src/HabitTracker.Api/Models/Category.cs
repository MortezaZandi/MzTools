using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Api.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation property for related events
        public virtual ICollection<Event> Events { get; set; } // Marked as virtual for lazy loading
    }
} 