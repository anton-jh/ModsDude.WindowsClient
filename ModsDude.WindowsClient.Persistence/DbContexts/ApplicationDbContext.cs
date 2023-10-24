using Microsoft.EntityFrameworkCore;

namespace ModsDude.WindowsClient.Persistence.DbContexts;
public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var innerPath = "ModsDude/db.sqlite";
        var dbFullPath = Path.Combine(localAppDataPath, innerPath);

        optionsBuilder.UseSqlite($"DataSource={dbFullPath}");
    }
}
