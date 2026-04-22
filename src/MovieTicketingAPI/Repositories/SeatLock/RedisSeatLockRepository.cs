using StackExchange.Redis;

namespace MovieTicketingAPI.Repositories.SeatLock;

public class RedisSeatLockRepository : ISeatLockRepository
{
    private readonly IDatabase _redisDatabase;

    public RedisSeatLockRepository(IConnectionMultiplexer redisConnection)
    {
        _redisDatabase = redisConnection.GetDatabase();
    }

    private string GetLockKey(Guid movieSessionId, Guid seatId)
    {
        return $"seatlock:session:{movieSessionId}:seat:{seatId}";
    }

    public async Task<bool> LockSeatAsync(Guid movieSessionId, Guid seatId, Guid userId, TimeSpan expirationTime)
    {
        var key = GetLockKey(movieSessionId, seatId);
        var value = userId.ToString();

        return await _redisDatabase.StringSetAsync(key, value, expirationTime, When.NotExists);
    }

    public async Task<bool> IsSeatLockedAsync(Guid movieSessionId, Guid seatId)
    {
        var key = GetLockKey(movieSessionId, seatId);
        return await _redisDatabase.KeyExistsAsync(key);
    }

    public async Task ReleaseSeatLockAsync(Guid movieSessionId, Guid seatId)
    {
        var key = GetLockKey(movieSessionId, seatId);
        await _redisDatabase.KeyDeleteAsync(key);
    }
}
