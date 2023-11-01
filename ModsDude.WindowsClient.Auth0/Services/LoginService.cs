using Auth0.OidcClient;

namespace ModsDude.WindowsClient.Auth0.Services;
public class LoginService
{
    private readonly Auth0Client _client;


    public LoginService()
    {
        var clientOptions = new Auth0ClientOptions
        {
            Domain = "modsdude-dev.eu.auth0.com",
            ClientId = "Hh7QKply1Ktxoq2Xv2mOicHp2VIWWAia"
        };

        _client = new Auth0Client(clientOptions);
        clientOptions.PostLogoutRedirectUri = clientOptions.RedirectUri;
    }


    public async Task Login()
    {
        var loginResult = await _client.LoginAsync();
    }
}
