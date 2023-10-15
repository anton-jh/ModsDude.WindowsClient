using ModsDude.WindowsClient.ApiClient.Generated;
using StrawberryShake;
using System.Security;

namespace ModsDude.WindowsClient.Application.Authentication;
public class AuthenticationService : IAuthenticationService
{
    private readonly IModsDudeClient _modsDude;


    public AuthenticationService(IModsDudeClient modsDude)
    {
        _modsDude = modsDude;
    }


    public async Task Login(string username, string password)
    {
        var result = await _modsDude.Login.ExecuteAsync(new LoginInput()
        {
            Username = username,
            Password = password
        });

        result.EnsureNoErrors();

        result.Data?.Login.LoginResult?.AccessToken
    }
}
