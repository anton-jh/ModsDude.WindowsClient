namespace ModsDude.WindowsClient.Model.Models;
public class Session
{
    public required string UserId { get; set; }
    public required string RefreshToken { get; set; }
    public required string AccessToken { get; set; }
    public required DateTimeOffset Expires { get; set; }
}
