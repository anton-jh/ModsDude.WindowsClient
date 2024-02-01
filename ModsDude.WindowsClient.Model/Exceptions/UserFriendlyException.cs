namespace ModsDude.WindowsClient.Model.Exceptions;
public class UserFriendlyException : Exception
{
    public UserFriendlyException(string userMessage, string? developerMessage = null)
        : base(userMessage)
    {
        UserMessage = userMessage;
        DeveloperMessage = developerMessage ?? userMessage;
    }


    public string UserMessage { get; }
    public string DeveloperMessage { get; }


    public static UserFriendlyException Unknown => new UserFriendlyException("Unknown error");
}
