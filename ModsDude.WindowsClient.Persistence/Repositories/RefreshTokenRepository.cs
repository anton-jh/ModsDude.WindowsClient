using ModsDude.WindowsClient.Domain.LocalUsers;
using ModsDude.WindowsClient.Persistence.DbContexts;
using ModsDude.WindowsClient.Persistence.Entities;

namespace ModsDude.WindowsClient.Persistence.Repositories;
public class RefreshTokenRepository(ApplicationDbContext dbContext)
    : IRefreshTokenRepository
{
    public async Task<RefreshToken?> Get(UserId userId)
    {
        var entity = await dbContext.RefreshTokens.FindAsync(userId.Value);

        if (entity is null)
        {
            return null;
        }

        return RefreshToken.From(entity.RefreshToken);
    }

    public async Task Set(UserId userId, RefreshToken refreshToken)
    {
        var existing = await dbContext.RefreshTokens.FindAsync(userId.Value);

        if (existing is null)
        {
            dbContext.RefreshTokens.Add(new RefreshTokenEntity(userId.Value, refreshToken.Value));
        }
        else
        {
            existing.RefreshToken = refreshToken.Value;
        }
    }

    public async Task Clear(UserId userId)
    {
        var existing = await dbContext.RefreshTokens.FindAsync(userId.Value);

        if (existing is not null)
        {
            dbContext.Remove(existing);
        }
    }
}
