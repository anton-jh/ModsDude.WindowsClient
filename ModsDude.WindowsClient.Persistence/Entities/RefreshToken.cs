using System.ComponentModel.DataAnnotations;

namespace ModsDude.WindowsClient.Persistence.Entities;
public class RefreshToken(string userId, string value)
{
    [Key]
    public string UserId { get; init; } = userId;

    public string Value { get; set; } = value;
}
