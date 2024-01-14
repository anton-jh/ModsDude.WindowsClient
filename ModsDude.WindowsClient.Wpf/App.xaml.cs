using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ModsDude.WindowsClient.Application;
using ModsDude.WindowsClient.Application.Authentication;
using ModsDude.WindowsClient.Domain.LocalUsers;
using ModsDude.WindowsClient.Persistence.DbContexts;
using ModsDude.WindowsClient.Persistence.Repositories;
using ModsDude.WindowsClient.ViewModel.ViewModelFactories;
using ModsDude.WindowsClient.ViewModel.ViewModels;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows;

namespace ModsDude.WindowsClient.Wpf;
public partial class App : System.Windows.Application
{
    private const string _dbFilename = "db.sqlite";


    private IServiceProvider _serviceProvider = null!;
    private IConfiguration _configuration = null!;


    protected override void OnStartup(StartupEventArgs e)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        _configuration = builder.Build();

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        _serviceProvider = serviceCollection.BuildServiceProvider();

        var window = _serviceProvider.GetRequiredService<MainWindow>();
        window.Show();

        MigrateDatabase();
        Login();
    }


    private void MigrateDatabase()
    {
        var dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();

        Directory.CreateDirectory(GetDbDirectory());

        dbContext.Database.Migrate();
    }

    private async void Login()
    {
        await _serviceProvider.GetRequiredService<LoginService>().Login();
    }


    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<MainWindow>();
        services.AddTransient<MainWindowViewModel>();

        services.AddTransient<StartPageViewModelFactory>();

        services.AddTransient<LoginService>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite($"DataSource={Path.Combine(GetDbDirectory(), _dbFilename)}");
        }, ServiceLifetime.Transient);

        services.AddSingleton<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddModsDudeClient()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://localhost:7035/graphql"));

        services.AddMediatR(config => config.RegisterServicesFromAssemblies(
            typeof(ApplicationAssemblyMarker).Assembly));
    }

    private static string GetDbDirectory()
    {
        var localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        return Path.Combine(localAppDataPath, "ModsDude");
    }
}
