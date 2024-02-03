using Auth0.OidcClient;
using ModsDude.WindowsClient.Model.DbContexts;
using ModsDude.WindowsClient.Model.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ModsDude.WindowsClient.Model.Services;
public class Session
{
    private readonly Auth0Client _authClient;
    private readonly ApplicationDbContext _dbContext;

    private UserData? _userData;


    public Session(ApplicationDbContext dbContext)
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


    public bool LoggedIn => _userData is not null;
    public string UserId => _userData?.UserId
        ?? throw new InvalidOperationException("Not logged in");
    public string AccessToken => _userData?.AccessToken
        ?? throw new InvalidOperationException("Not logged in");
    public string RefreshToken => _userData?.RefreshToken
        ?? throw new InvalidOperationException("No refresh token");


    public async Task Login()
    {
        //await _authClient.LogoutAsync();
        var loginResult = await _authClient.LoginAsync(new
        {
            audience = "api.modsdude.com"
        });

        if (loginResult.IsError)
        {
            throw new Exception(loginResult.Error);
        }

        _userData = new()
        {
            UserId = new(loginResult.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value),
            AccessToken = loginResult.AccessToken,
            RefreshToken = loginResult.RefreshToken
        };

        await SaveRefreshToken();
    }


    private async Task SaveRefreshToken()
    {
        if (_userData is null)
        {
            throw new InvalidOperationException("Not logged in");
        }

        var existing = await _dbContext.RefreshTokens
            .FindAsync(_userData.UserId);

        if (existing is not null)
        {
            existing.Value = _userData.RefreshToken;
        }
        else
        {
            var newToken = new RefreshToken()
            {
                UserId = _userData.UserId,
                Value = _userData.RefreshToken
            };
            _dbContext.RefreshTokens.Add(newToken);
        }

        await _dbContext.SaveChangesAsync();
    }


    private class UserData
    {
        public required string UserId { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
