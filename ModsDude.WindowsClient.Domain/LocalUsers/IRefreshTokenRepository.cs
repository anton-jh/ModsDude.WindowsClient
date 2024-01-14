namespace ModsDude.WindowsClient.Domain.LocalUsers;
public interface IRefreshTokenRepository
{
    Task Set(UserId userId, RefreshToken refreshToken);
    Task<RefreshToken?> Get(UserId userId);
    Task Clear(UserId userId);
}
