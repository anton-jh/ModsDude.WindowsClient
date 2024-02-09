using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModsDude.WindowsClient.Model.Services;
using ModsDude.WindowsClient.ViewModel.Pages;

namespace ModsDude.WindowsClient.ViewModel.Windows;
public partial class MainWindowViewModel
    : ObservableObject
{
    private readonly SessionService _sessionService;


    public MainWindowViewModel(SessionService sessionService)
    {
        _sessionService = sessionService;

        _sessionService.LoggedInChanged += OnLoggedInChanged;
    }


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentPage))]
    private bool _loggedIn = false;

    public PageViewModel CurrentPage => LoggedIn
        ? new MainPageViewModel()
        : new LoginPageViewModel();


    [RelayCommand]
    public Task Logout()
    {
        return _sessionService.Logout();
    }


    private void OnLoggedInChanged(object? sender, bool e)
    {
        LoggedIn = e;
    }
}
