using Auth0.OidcClient;
using Microsoft.EntityFrameworkCore;
using ModsDude.WindowsClient.Model.Helpers;
using ModsDude.WindowsClient.Model.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace ModsDude.WindowsClient.Model.Services;
public class SessionService
{
    private const string _sessionFilename = "session.json";
    private readonly Auth0Client _authClient;
    private Session? _session;

    
    public SessionService()
    {
        var clientOptions = new Auth0ClientOptions
        {
            Domain = "modsdude-dev.eu.auth0.com",
            ClientId = "Hh7QKply1Ktxoq2Xv2mOicHp2VIWWAia",
            Scope = "openid profile email offline_access create:repo"
        };
        _authClient = new Auth0Client(clientOptions);
        clientOptions.PostLogoutRedirectUri = clientOptions.RedirectUri;
    }


    public Task<string> GetAccessToken()
    {
        // TODO
    }

    public async Task Init(CancellationToken cancellationToken)
    {
        _session = LoadSession();

        if (_session is not null)
        {
            var refreshSuccess = await Refresh(_session, cancellationToken);
            if (refreshSuccess)
            {
                return;
            }
        }

        _session = await Login(cancellationToken);
    }


    private async Task<Session> Login(CancellationToken cancellationToken)
    {
        var loginResult = await _authClient.LoginAsync(new
        {
            audience = "api.modsdude.com"
        }, cancellationToken);

        if (loginResult.IsError)
        {
            throw new Exception(loginResult.Error);
        }

        var userId = loginResult.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;

        return new Session()
        {
            AccessToken = loginResult.AccessToken,
            Expires = loginResult.AccessTokenExpiration,
            RefreshToken = loginResult.RefreshToken,
            UserId = userId
        };
    }

    private async Task<bool> Refresh(Session session, CancellationToken cancellationToken)
    {
        var refreshResult = await _authClient.RefreshTokenAsync(session.RefreshToken, cancellationToken);

        if (refreshResult.IsError)
        {
            return false;
        }

        session.RefreshToken = refreshResult.RefreshToken;
        session.AccessToken = refreshResult.AccessToken;
        session.Expires = refreshResult.AccessTokenExpiration;

        SaveSession(session);
        return true;
    }


    private static Session? LoadSession()
    {
        var filepath = Path.Combine(FileSystemHelper.GetDbDirectory(), _sessionFilename);

        if (File.Exists(filepath) == false)
        {
            return null;
        }

        var serializedSession = File.ReadAllText(filepath);

        return JsonSerializer.Deserialize<Session>(serializedSession);
    }

    private static void SaveSession(Session session)
    {
        var filepath = Path.Combine(FileSystemHelper.GetDbDirectory(), _sessionFilename);
        var serializedSession = JsonSerializer.Serialize(session);
        
        File.WriteAllText(filepath, serializedSession);
    }
}
