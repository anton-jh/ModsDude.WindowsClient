using ModsDude.WindowsClient.ViewModel.Pages.EditRepoPage;
using ModsDude.WindowsClient.ViewModel.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace ModsDude.WindowsClient.ViewModel.Pages.StartPage;
public class StartEditRepoCommand
    : ICommand, IDisposable
{
    private readonly StartPageViewModel _page;
    private readonly EditRepoPageViewModelFactory _editRepoPageViewModelFactory;
    private readonly NavigationService _navigationService;


    public StartEditRepoCommand(
        StartPageViewModel page,
        EditRepoPageViewModelFactory editRepoPageViewModelFactory,
        NavigationService navigationService)
    {
        _page = page;
        _editRepoPageViewModelFactory = editRepoPageViewModelFactory;
        _navigationService = navigationService;
        _page.PropertyChanged += SelectedRepoChanged;
    }


    public event EventHandler? CanExecuteChanged;


    public bool CanExecute(object? parameter)
    {
        return _page.SelectedRepo is not null;
    }

    public void Execute(object? parameter)
    {
        if (_page.SelectedRepo is null)
        {
            throw new InvalidOperationException("No repo selected");
        }

        var editRepoPage = _editRepoPageViewModelFactory.Create(_page.SelectedRepo.Repo);
        _navigationService.Navigate(editRepoPage);
    }

    public void Dispose()
    {
        _page.PropertyChanged -= SelectedRepoChanged;
    }


    private void SelectedRepoChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(StartPageViewModel.SelectedRepo))
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
