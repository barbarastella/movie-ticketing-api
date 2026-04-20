using MovieTicketingAPI.Models;
using MovieTicketingAPI.Models.DTOs;

namespace MovieTicketingAPI.Services
{
    public interface ITicketService
    {
        Task<Ticket?> BuyAsync(Guid UserId, BuyTicketDto dto);
    }
}
