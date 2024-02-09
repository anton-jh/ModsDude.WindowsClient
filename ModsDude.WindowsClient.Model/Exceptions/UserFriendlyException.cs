namespace ModsDude.WindowsClient.Model.Exceptions;
public class UserFriendlyException(
    string userMessage,
    string? developerMessage = null,
    Exception? inner = null)
    : Exception(userMessage, inner)
{
    public string UserMessage { get; } = userMessage;
    public string DeveloperMessage { get; } = developerMessage ?? userMessage;


    public static UserFriendlyException Unknown => new("Unknown error");
}
