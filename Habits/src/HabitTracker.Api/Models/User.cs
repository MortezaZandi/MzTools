using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HabitTracker.Api.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}