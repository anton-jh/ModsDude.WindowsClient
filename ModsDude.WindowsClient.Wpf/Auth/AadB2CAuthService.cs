using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Desktop;
using ModsDude.WindowsClient.Model.Interfaces;
using ModsDude.WindowsClient.Model.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ModsDude.WindowsClient.Wpf.Auth;
internal class AadB2CAuthService : IAuthService
{
    private static readonly string _tenantName = "modsdude";
    private static readonly string _tenant = $"{_tenantName}.onmicrosoft.com";
    private static readonly string _azureAdB2CHostname = $"{_tenantName}.b2clogin.com";
    private static readonly string _clientId = "6ef1be23-6842-4764-a341-9228af7ade41";
    private static readonly string _redirectUri = $"https://{_tenantName}.b2clogin.com/oauth2/nativeclient";
    private static readonly string _policySignUpSignIn = "B2C_1_signupsignin1";
    //private static readonly string _policyEditProfile = "b2c_1_edit_profile";
    private static readonly string _policyResetPassword = "b2c_1_reset";
    private static readonly string[] _apiScopes = ["offline_access", "openid", $"https://{_tenant}/modsdude-server/default"];
    private static readonly string _authorityBase = $"https://{_azureAdB2CHostname}/tfp/{_tenant}/";
    private static readonly string _authoritySignUpSignIn = $"{_authorityBase}{_policySignUpSignIn}";
    //private static readonly string _authorityEditProfile = $"{_authorityBase}{_policyEditProfile}";
    private static readonly string _authorityResetPassword = $"{_authorityBase}{_policyResetPassword}";

    private IPublicClientApplication _publicClientApp;


    public AadB2CAuthService()
    {
        _publicClientApp = PublicClientApplicationBuilder.Create(_clientId)
            .WithB2CAuthority(_authoritySignUpSignIn)
            .WithRedirectUri(_redirectUri)
            .WithWindowsEmbeddedBrowserSupport()
            .Build();
    }


    public async Task<Session> Login(CancellationToken cancellationToken)
    {
        var authResult = await _publicClientApp.AcquireTokenInteractive(_apiScopes)
            .WithPrompt(Prompt.SelectAccount)
            .WithB2CAuthority(_authorityResetPassword)
            .ExecuteAsync(cancellationToken);

        return new Session()
        {
            AccessToken = authResult.AccessToken,
            Expires = authResult.ExpiresOn,
            UserId = authResult.Account.Username,
            RefreshToken = null!,
        };
    }

    public async Task Refresh(Session session, CancellationToken cancellationToken)
    {
        var account = await _publicClientApp.GetAccountAsync(session.UserId);
        AuthenticationResult authResult;

        try
        {
            authResult = await _publicClientApp.AcquireTokenSilent(_apiScopes, account)
                .ExecuteAsync(cancellationToken);
        }
        catch (MsalUiRequiredException)
        {
            authResult = await _publicClientApp.AcquireTokenInteractive(_apiScopes)
                .ExecuteAsync(cancellationToken);
        }

        session.AccessToken = authResult.AccessToken;
        session.Expires = authResult.ExpiresOn;
        session.UserId = authResult.Account.Username;
    }
}
