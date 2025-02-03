using ModsDude.WindowsClient.Model.Exceptions;
using ModsDude.WindowsClient.Model.Interfaces;
using ModsDude.WindowsClient.Model.Models;

namespace ModsDude.WindowsClient.Model.Services;
public class SessionService(
    IAuthService authService)
{
    private readonly SemaphoreSlim _semaphore = new(1);

    private bool _isLoggedIn = false;


    public event EventHandler<bool>? LoggedInChanged;


    public bool IsLoggedIn
    {
        get => _isLoggedIn;
        private set
        {
            var wasLoggedIn = _isLoggedIn;
            _isLoggedIn = value;
            if (wasLoggedIn != _isLoggedIn)
            {
                LoggedInChanged?.Invoke(this, _isLoggedIn);
            }
        }
    }
    

    public async Task<Session> GetSession(CancellationToken cancellationToken)
    {
        await _semaphore.WaitAsync(cancellationToken);

        try
        {
            var session = await authService.GetSession(cancellationToken);
            IsLoggedIn = true;

            return session;
        }
        catch (Exception ex)
        {
            IsLoggedIn = false;
            throw new UserFriendlyException("Could not resume your active session for some reason.", ex.Message, ex);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task Logout(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
