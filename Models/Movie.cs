using System.ComponentModel.DataAnnotations;

namespace MovieTicketingAPI.Models
{
    public class Movie
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int DurationMin { get; set; }

        public string Genre { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
