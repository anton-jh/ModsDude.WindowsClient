using ModsDude.WindowsClient.ApiClient.Generated;
using ModsDude.WindowsClient.Application.Exceptions;
using ModsDude.WindowsClient.Domain.LocalUsers;
using StrawberryShake;
using System.Diagnostics.CodeAnalysis;

namespace ModsDude.WindowsClient.Application.Authentication;
public class LoginService : ILoginService
{
    private readonly IModsDudeClient _modsDude;
    private readonly ILocalUserRepository _localUserRepository;


    public LoginService(IModsDudeClient modsDude, ILocalUserRepository localUserRepository)
    {
        _modsDude = modsDude;
        _localUserRepository = localUserRepository;
    }


    public async Task Login(string username, string password)
    {
        var result = await _modsDude.Login.ExecuteAsync(new LoginInput()
        {
            Username = username,
            Password = password
        });

        result.EnsureNoErrors();

        var data = result.Data
            ?? throw new GraphqlNullDataException();

        HandleErrors(data, username);

        var loginResult = data.Login.LoginResult
            ?? throw UserFriendlyException.Unknown;

        var user = new LocalUser(Username.From(username), RefreshToken.From(loginResult.RefreshToken));

        await _localUserRepository.SaveAsync(user);

        // TODO set username, accessToken (and refreshToken) in some sort of global in-memory state
        // (maybe event/notification with MediatR? or command/request even?)
    }


    private static void HandleErrors(ILoginResult data, string username)
    {
        var error = data.Login.Errors?.FirstOrDefault();

        UserFriendlyException? exception = error switch
        {
            ILogin_Login_Errors_UserNotFoundError => throw new UserFriendlyException($"User '{username}' does not exist."),
            ILogin_Login_Errors_WrongPasswordError => throw new UserFriendlyException("Incorrect password"),
            not null => throw UserFriendlyException.Unknown,
            null => null
        };

        if (exception is not null)
        {
            throw exception;
        }
    }
}
