using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModsDude.WindowsClient.Model.Services;
using ModsDude.WindowsClient.ViewModel.ViewModels;
using System.Collections.ObjectModel;

namespace ModsDude.WindowsClient.ViewModel.Pages;
public partial class MainPageViewModel
    : PageViewModel
{
    private readonly RepoService _repoService;


    public MainPageViewModel(RepoService repoService)
    {
        _selectedMenuItem = MenuItems.First();
        _repoService = repoService;
    }


    [ObservableProperty]
    private NewRepoItemViewModel? _repoDraft;

    private IMenuItemViewModel? _selectedMenuItem;
    public IMenuItemViewModel? SelectedMenuItem
    {
        get => _selectedMenuItem;
        set
        {
            OnPropertyChanging(nameof(SelectedMenuItem));
            _selectedMenuItem = null;
            OnPropertyChanged(nameof(SelectedMenuItem));

            OnPropertyChanging(nameof(SelectedMenuItem));
            OnPropertyChanging(nameof(CurrentPage));
            _selectedMenuItem = value;
            OnPropertyChanged(nameof(SelectedMenuItem));
            OnPropertyChanged(nameof(CurrentPage));
        }
    }

    public PageViewModel? CurrentPage => SelectedMenuItem?.GetPage();

    public ObservableCollection<IMenuItemViewModel> MenuItems { get; } = [
        new MenuItemViewModel("Home", new ExamplePageViewModel("Home page")),
        new MenuItemViewModel("Test", new ExamplePageViewModel("Test page"))
    ];

    public ObservableCollection<IMenuItemViewModel> Repos { get; } = [];


    public override void Init()
    {
        LoadReposCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadRepos(CancellationToken cancellationToken)
    {
        var repos = await _repoService.GetRepos(cancellationToken);
        var viewModels = repos.Select(x => new RepoItemViewModel(x));

        Repos.Clear();
        foreach (var repo in viewModels)
        {
            Repos.Add(repo);
        }
    }

    [RelayCommand]
    private void StartCreateRepo()
    {
        RepoDraft = new NewRepoItemViewModel();
    }

    partial void OnRepoDraftChanged(NewRepoItemViewModel? oldValue, NewRepoItemViewModel? newValue)
    {
        if (oldValue is not null)
        {
            Repos.Remove(oldValue);
        }
        if (newValue is not null)
        {
            Repos.Insert(0, newValue);
            SelectedMenuItem = newValue;
        }
    }
}
