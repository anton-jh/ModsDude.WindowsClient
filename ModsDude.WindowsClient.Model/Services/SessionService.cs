using ModsDude.WindowsClient.Model.Exceptions;
using ModsDude.WindowsClient.Model.Helpers;
using ModsDude.WindowsClient.Model.Interfaces;
using ModsDude.WindowsClient.Model.Models;
using System.Text.Json;

namespace ModsDude.WindowsClient.Model.Services;
public class SessionService(
    IAuthService loginService)
{
    private const string _sessionFilename = "session.json";

    private Session? _session;


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
        // await _authClient.LogoutAsync(cancellationToken: cancellationToken);

        ClearSession();
        SetSession(null);

        if (triggerLogin)
        {
            await Init(cancellationToken);
        }
    }


    private async Task<Session> Login(CancellationToken cancellationToken)
    {
        var session = await loginService.Login(cancellationToken);
        SaveSession(session);

        return session;
    }

    private async Task<bool> RefreshIfNeeded(Session session, CancellationToken cancellationToken)
    {
        if (session.Expires > DateTimeOffset.Now.AddSeconds(10))
        {
            return true;
        }

        await loginService.Refresh(session, cancellationToken);
        SaveSession(session);

        // TODO: What if cannot refresh?

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
