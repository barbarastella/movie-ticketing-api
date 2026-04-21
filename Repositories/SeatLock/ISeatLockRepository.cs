namespace MovieTicketingAPI.Repositories.SeatLock;

public interface ISeatLockRepository
{
    Task<bool> LockSeatAsync(Guid movieSessionId, Guid seatId, Guid userId, TimeSpan expirationTime);
    Task<bool> IsSeatLockedAsync(Guid movieSessionId, Guid seatId);
    Task ReleaseSeatLockAsync(Guid movieSessionId, Guid seatId);
}
