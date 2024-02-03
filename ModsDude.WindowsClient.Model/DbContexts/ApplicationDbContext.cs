using Microsoft.EntityFrameworkCore;
using ModsDude.WindowsClient.Model.Models;

namespace ModsDude.WindowsClient.Model.DbContexts;
public class ApplicationDbContext
    : DbContext
{
    private const string _dbFilename = "db.sqlite";


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"DataSource={Path.Combine(GetDbDirectory(), _dbFilename)}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }


    public required DbSet<RefreshToken> RefreshTokens { get; init; }
    public required DbSet<LocalInstance> LocalInstances { get; init; }


    public static string GetDbDirectory()
    {
        var localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        return Path.Combine(localAppDataPath, "ModsDude");
    }
}
