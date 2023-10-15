using System.Security;

namespace ModsDude.WindowsClient.Application.Authentication;
public interface IAuthenticationService
{
    Task Login(string username, string password);
}