using System.Security;

namespace ModsDude.WindowsClient.Application.Authentication;
public interface ILoginService
{
    Task Login(string username, string password);
}