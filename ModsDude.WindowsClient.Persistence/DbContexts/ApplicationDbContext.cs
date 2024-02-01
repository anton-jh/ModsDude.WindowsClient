using Microsoft.EntityFrameworkCore;
using ModsDude.WindowsClient.Persistence.Entities;

namespace ModsDude.WindowsClient.Persistence.DbContexts;
public class ApplicationDbContext
    : DbContext
{
    private const string _dbFilename = "db.sqlite";


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"DataSource={Path.Combine(GetDbDirectory(), _dbFilename)}");
    }


    public required DbSet<RefreshToken> RefreshTokens { get; init; }


    public static string GetDbDirectory()
    {
        var localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        return Path.Combine(localAppDataPath, "ModsDude");
    }
}
