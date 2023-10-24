using ValueOf;

namespace ModsDude.WindowsClient.Domain.LocalUsers;
public class LocalUser
{
    public LocalUser(Username username, RefreshToken refreshToken)
    {
        Username = username;
        RefreshToken = refreshToken;
    }


    public Username Username { get; }
    public RefreshToken RefreshToken { get; }
}

public class Username : ValueOf<string, Username> { }
public class RefreshToken : ValueOf<string, RefreshToken> { }
