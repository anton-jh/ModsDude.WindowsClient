using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModsDude.WindowsClient.ApiClient;
using ModsDude.WindowsClient.Model.Services;
using ModsDude.WindowsClient.Persistence.DbContexts;
using ModsDude.WindowsClient.ViewModel.ViewModelFactories;
using ModsDude.WindowsClient.ViewModel.ViewModels;
using System;
using System.IO;
using System.Windows;

namespace ModsDude.WindowsClient.Wpf;
public partial class App : Application
{
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

        Directory.CreateDirectory(ApplicationDbContext.GetDbDirectory());

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

        services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Transient);

        services.AddModsDudeClient();
    }
}
