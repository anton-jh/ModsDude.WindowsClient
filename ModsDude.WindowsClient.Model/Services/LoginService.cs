using Auth0.OidcClient;
using ModsDude.WindowsClient.Persistence.DbContexts;
using ModsDude.WindowsClient.Persistence.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace ModsDude.WindowsClient.Model.Services;
public class LoginService
{
    private readonly Auth0Client _client;
    private readonly ApplicationDbContext _dbContext;


    public LoginService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        var clientOptions = new Auth0ClientOptions
        {
            Domain = "modsdude-dev.eu.auth0.com",
            ClientId = "Hh7QKply1Ktxoq2Xv2mOicHp2VIWWAia",
            Scope = "openid profile email offline_access create:repo"
        };
        _client = new Auth0Client(clientOptions);
        clientOptions.PostLogoutRedirectUri = clientOptions.RedirectUri;
    }


    public async Task Login()
    {
        await _client.LogoutAsync();
        var loginResult = await _client.LoginAsync(new
        {
            audience = "api.modsdude.com"
        });

        if (loginResult.IsError)
        {
            throw new Exception(loginResult.Error);
        }

        var userId = loginResult.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
        var refreshToken = loginResult.RefreshToken;

        await SetRefreshToken(userId, refreshToken);
    }


    private async Task SetRefreshToken(string userId, string tokenValue)
    {
        var existing = await _dbContext.RefreshTokens
            .FindAsync(userId);

        if (existing is not null)
        {
            existing.Value = tokenValue;
        }
        else
        {
            var newToken = new RefreshToken(userId, tokenValue);
            _dbContext.RefreshTokens.Add(newToken);
        }

        await _dbContext.SaveChangesAsync();
    }
}
