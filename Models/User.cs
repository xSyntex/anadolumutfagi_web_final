using System.ComponentModel.DataAnnotations;

namespace Web_Programlama_Proje.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty; // In a real app, verify this is hashed!

        public string? FullName { get; set; }
        public string? Address { get; set; }

        public string Role { get; set; } = "User"; // "Admin" or "User"
    }
}
