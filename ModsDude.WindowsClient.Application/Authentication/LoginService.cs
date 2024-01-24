using Auth0.OidcClient;
using ModsDude.WindowsClient.Domain.LocalUsers;
using System.IdentityModel.Tokens.Jwt;

namespace ModsDude.WindowsClient.Application.Authentication;
public class LoginService
{
    private readonly Auth0Client _client;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public LoginService(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;

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

        var userId = UserId.From(loginResult.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value);
        var refreshToken = RefreshToken.From(loginResult.RefreshToken);

        await _refreshTokenRepository.Set(userId, refreshToken);
    }
}
