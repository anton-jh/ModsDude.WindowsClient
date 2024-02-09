﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModsDude.WindowsClient.ApiClient;
using ModsDude.WindowsClient.Model.DbContexts;
using ModsDude.WindowsClient.Model.Helpers;
using ModsDude.WindowsClient.Model.Services;
using ModsDude.WindowsClient.ViewModel.Windows;
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
        InitSession();
    }


    private void MigrateDatabase()
    {
        var dbContext = _serviceProvider.GetRequiredService<ApplicationDbContext>();

        Directory.CreateDirectory(FileSystemHelper.GetDbDirectory());

        dbContext.Database.Migrate();
    }

    private async void InitSession()
    {
        await _serviceProvider.GetRequiredService<SessionService>()
            .Init(default);

        // todo?
    }


    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();

        services.AddSingleton<SessionService>();
        services.AddSingleton<RepoService>();

        services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Transient);

        services.AddModsDudeClient();
    }

    private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        //TODO
        // check if userfriendly exception
        //MessageBox.Show()
    }
}
