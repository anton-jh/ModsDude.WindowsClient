using ModsDude.WindowsClient.Model.Models;

namespace ModsDude.WindowsClient.Model.Interfaces;
public interface IAuthService
{
    Task<Session> Login(CancellationToken cancellationToken);
    Task Refresh(Session session, CancellationToken cancellationToken);
}
