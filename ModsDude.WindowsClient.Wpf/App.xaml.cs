using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModsDude.WindowsClient.ViewModel.ViewModelFactories;
using ModsDude.WindowsClient.ViewModel.ViewModels;
using System;
using System.IO;
using System.Windows;

namespace ModsDude.WindowsClient.Wpf;
public partial class App : System.Windows.Application
{
    private IServiceProvider? _serviceProvider;
    private IConfiguration? _configuration;


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
    }


    private void ConfigureServices(IServiceCollection services)
    {
        services
            .AddTransient<MainWindow>()
            .AddTransient<MainWindowViewModel>();

        services
            .AddTransient<LoginPageViewModelFactory>();

        services
            .AddModsDudeClient()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://localhost:7035/graphql"));
    }
}
