using Moq;
using MovieTicketingAPI.Models;
using MovieTicketingAPI.Models.DTOs;
using MovieTicketingAPI.Repositories.Tickets;
using MovieTicketingAPI.Repositories.MovieSessions;
using MovieTicketingAPI.Repositories.SeatLock;
using MovieTicketingAPI.Services.Tickets;

namespace MovieTicketingAPI.Tests.Services;

public class TicketServiceTests
{
    private readonly Mock<ITicketRepository> _ticketRepositoryMock;
    private readonly Mock<IMovieSessionRepository> _sessionRepositoryMock;
    private readonly Mock<ISeatLockRepository> _lockRepositoryMock;
    private readonly TicketService _ticketService;

    public TicketServiceTests()
    {
        _ticketRepositoryMock = new Mock<ITicketRepository>();
        _sessionRepositoryMock = new Mock<IMovieSessionRepository>();
        _lockRepositoryMock = new Mock<ISeatLockRepository>();

        _ticketService = new TicketService(
            _ticketRepositoryMock.Object,
            _sessionRepositoryMock.Object,
            _lockRepositoryMock.Object
         );
    }

    [Fact]
    public async Task ReserveSeatAsync_WhenSeatIsAvailable_ShouldReturnTrue()
    {
        var userId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();
        var seatId = Guid.NewGuid();

        _ticketRepositoryMock.Setup(repo => repo.IsSeatSoldAsync(sessionId, seatId)).ReturnsAsync(false);
        _lockRepositoryMock.Setup(repo => repo.LockSeatAsync(sessionId, seatId, userId, It.IsAny<TimeSpan>())).ReturnsAsync(true);

        var result = await _ticketService.ReserveSeatAsync(userId, sessionId, seatId);

        Assert.True(result);
    }

    [Fact]
    public async Task ReserveSeatAsync_WhenSeatIsAlreadySold_ShouldReturnFalse()
    {
        var sessionId = Guid.NewGuid();
        var seatId = Guid.NewGuid();

        _ticketRepositoryMock.Setup(r => r.IsSeatSoldAsync(sessionId, seatId)).ReturnsAsync(true);

        var result = await _ticketService.ReserveSeatAsync(Guid.NewGuid(), sessionId, seatId);

        Assert.False(result);

        _lockRepositoryMock.Verify(r => r.LockSeatAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<TimeSpan>()), Times.Never);
    }

    [Fact]
    public async Task ConfirmPurchaseAsync_WhenValid_ShouldReturnTicketAndReleaseLock()
    {
        var userId = Guid.NewGuid();
        var dto = new BuyTicketDto(Guid.NewGuid(), Guid.NewGuid());
        var movie = new Movie { Title = "Batman", Price = 20.00m };
        var session = new MovieSession { Id = dto.MovieSessionId, Movie = movie };

        _ticketRepositoryMock.Setup(r => r.IsSeatSoldAsync(dto.MovieSessionId, dto.SeatId)).ReturnsAsync(false);
        _lockRepositoryMock.Setup(r => r.IsSeatLockedAsync(dto.MovieSessionId, dto.SeatId)).ReturnsAsync(true);
        _sessionRepositoryMock.Setup(r => r.GetByIdWithMovieAsync(dto.MovieSessionId)).ReturnsAsync(session);

        var result = await _ticketService.ConfirmPurchaseAsync(userId, dto);

        Assert.NotNull(result);
        Assert.Equal(20.00m, result.Price);

        _ticketRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Ticket>()), Times.Once);
        _lockRepositoryMock.Verify(r => r.ReleaseSeatLockAsync(dto.MovieSessionId, dto.SeatId), Times.Once);
    }
}
