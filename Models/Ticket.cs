using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketingAPI.Models;

public class Ticket
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid MovieSessionId { get; set; }

    [ForeignKey(nameof(MovieSessionId))]
    public MovieSession? MovieSession { get; set; }

    [Required]
    public Guid SeatId { get; set; }

    [ForeignKey(nameof(SeatId))]
    public Seat? Seat { get; set; }

    [Required]
    public DateTime PurchaseDate { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

}
