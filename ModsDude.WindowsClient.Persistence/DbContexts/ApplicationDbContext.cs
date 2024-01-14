using Microsoft.EntityFrameworkCore;
using ModsDude.WindowsClient.Persistence.Entities;

namespace ModsDude.WindowsClient.Persistence.DbContexts;
public class ApplicationDbContext(DbContextOptions options)
    : DbContext(options)
{
    public required DbSet<RefreshTokenEntity> RefreshTokens { get; init; }
}
