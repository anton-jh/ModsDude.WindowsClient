﻿using Auth0.OidcClient;
using ModsDude.WindowsClient.Model.Exceptions;
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
            Scope = "openid profile email offline_access create:repo",
        };
        _authClient = new Auth0Client(clientOptions);
        clientOptions.PostLogoutRedirectUri = clientOptions.RedirectUri;
    }


    public event EventHandler<bool>? LoggedInChanged;


    public bool IsLoggedIn => _session is not null;
    public string UserId => _session?.UserId
        ?? throw new InvalidOperationException("Not logged in");


    public async Task<string> GetAccessToken(CancellationToken cancellationToken)
    {
        if (_session is null)
        {
            throw new InvalidOperationException("Not logged in");
        }

        var refreshSuccess = await RefreshIfNeeded(_session, cancellationToken);
        if (refreshSuccess)
        {
            return _session.AccessToken;
        }

        SetSession(null);
        throw new UserFriendlyException(
            "Something went wrong, try logging in again",
            "Refresh failed");
    }

    public async Task Init(CancellationToken cancellationToken)
    {
        SetSession(LoadSession());

        if (_session is not null)
        {
            var refreshSuccess = await RefreshIfNeeded(_session, cancellationToken);
            if (refreshSuccess)
            {
                return;
            }
        }

        SetSession(await Login(cancellationToken));
    }

    public async Task Logout(bool triggerLogin = true, CancellationToken cancellationToken = default)
    {
        await _authClient.LogoutAsync(cancellationToken: cancellationToken);

        ClearSession();
        SetSession(null);

        if (triggerLogin)
        {
            await Init(cancellationToken);
        }
    }


    private async Task<Session> Login(CancellationToken cancellationToken)
    {
        var loginResult = await _authClient.LoginAsync(new
        {
            audience = "api.modsdude.com"
        }, cancellationToken);

        if (loginResult.IsError)
        {
            throw new UserFriendlyException(
                "Login failed",
                loginResult.Error);
        }

        var userId = loginResult.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
        var session = new Session()
        {
            AccessToken = loginResult.AccessToken,
            Expires = loginResult.AccessTokenExpiration,
            RefreshToken = loginResult.RefreshToken,
            UserId = userId
        };

        SaveSession(session);

        return session;
    }

    private async Task<bool> RefreshIfNeeded(Session session, CancellationToken cancellationToken)
    {
        if (session.Expires > DateTimeOffset.Now.AddSeconds(10))
        {
            return true;
        }

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

    private void SetSession(Session? session)
    {
        var wasLoggedIn = IsLoggedIn;
        _session = session;

        if (wasLoggedIn != IsLoggedIn)
        {
            LoggedInChanged?.Invoke(this, IsLoggedIn);
        }
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

    private static void ClearSession()
    {
        var filepath = Path.Combine(FileSystemHelper.GetDbDirectory(), _sessionFilename);
        if (File.Exists(filepath))
        {
            File.Delete(filepath);
        }
    }
}
