using System.ComponentModel.DataAnnotations;

namespace CustomerApi.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; } // Use Guid for unique ID

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public int LoyaltyPoints { get; set; } = 0;

        public List<string> FavoriteDishes { get; set; } = new();
        public List<int> OrderIds { get; set; } = new();
        public List<string> Reservations { get; set; } = new();
        public List<string> Feedbacks { get; set; } = new();
    }
}
