using MovieTicketingAPI.Models;
using MovieTicketingAPI.Models.DTOs;

namespace MovieTicketingAPI.Services
{
    public interface ITicketService
    {
        Task<bool> ReserveSeatAsync(Guid userId, Guid movieSessionId, Guid seatId);
        Task<Ticket?> ConfirmPurchaseAsync(Guid userId, BuyTicketDto dto);
    }
}
