using Auth0.OidcClient;
using Microsoft.EntityFrameworkCore;
using ModsDude.WindowsClient.Model.DbContexts;
using ModsDude.WindowsClient.Model.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ModsDude.WindowsClient.Model.Services;
public class SessionOld
{
    private readonly Auth0Client _authClient;
    private readonly ApplicationDbContext _dbContext;

    private SessionData? _sessionData;


    public SessionOld(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        var clientOptions = new Auth0ClientOptions
        {
            Domain = "modsdude-dev.eu.auth0.com",
            ClientId = "Hh7QKply1Ktxoq2Xv2mOicHp2VIWWAia",
            Scope = "openid profile email offline_access create:repo"
        };
        _authClient = new Auth0Client(clientOptions);
        clientOptions.PostLogoutRedirectUri = clientOptions.RedirectUri;
    }


    public bool LoggedIn => _sessionData is not null;
    public string UserId => _sessionData?.UserId
        ?? throw new InvalidOperationException("Not logged in");
    public string AccessToken => _sessionData?.AccessToken
        ?? throw new InvalidOperationException("Not logged in"); // refresh if needed


    public async Task Login()
    {


        await SaveRefreshToken(loginResult.RefreshToken);
    }

    private async Task SaveRefreshToken(string token)
    {
        if (_sessionData is null)
        {
            throw new InvalidOperationException("Not logged in");
        }

        var existing = await _dbContext.RefreshTokens
            .ToListAsync();
        var newToken = new RefreshToken()
        {
            UserId = _sessionData.UserId,
            Value = token
        };

        _dbContext.RefreshTokens.RemoveRange(existing);
        _dbContext.RefreshTokens.Add(newToken);

        await _dbContext.SaveChangesAsync();
    }


    private class SessionData
    {
        public required string UserId { get; set; }
        public required string AccessToken { get; set; }
    }
}
