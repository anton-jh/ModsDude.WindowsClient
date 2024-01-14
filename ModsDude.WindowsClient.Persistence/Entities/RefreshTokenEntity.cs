using System.ComponentModel.DataAnnotations;

namespace ModsDude.WindowsClient.Persistence.Entities;
public class RefreshTokenEntity(string userId, string refreshToken)
{
    [Key]
    public string UserId { get; init; } = userId;

    public string RefreshToken { get; set; } = refreshToken;
}
