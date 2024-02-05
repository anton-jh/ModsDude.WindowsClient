using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace ModsDude.WindowsClient.ViewModel.ViewModels;
public partial class MainWindowViewModel
    : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentPage))]
    private MenuItemViewModel? _selectedMenuItem;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentPage))]
    private RepoItemViewModel? _selectedRepo;


    public MainWindowViewModel()
    {
        SelectedMenuItem = MenuItems.First();
    }


    public PageViewModel? CurrentPage =>
        SelectedMenuItem?.Page ??
        MakeRepoPage(SelectedRepo);

    public ObservableCollection<MenuItemViewModel> MenuItems { get; } = [
        new("Home", new ExamplePageViewModel("Home page")),
        new("Test", new ExamplePageViewModel("Test page"))
    ];

    public ObservableCollection<RepoItemViewModel> Repos { get; } = [
        new RepoItemViewModel(new()
        {
            Id = default,
            Name = "Repo 1"
        }),
        new RepoItemViewModel(new()
        {
            Id = default,
            Name = "Repo 2"
        })
    ];


    partial void OnSelectedRepoChanged(RepoItemViewModel? oldValue, RepoItemViewModel? newValue)
    {
        if (newValue is null)
        {
            SelectedMenuItem = MenuItems.First();
        }
        else if (oldValue is null && newValue is not null)
        {
            SelectedMenuItem = null;
        }
    }

    partial void OnSelectedMenuItemChanged(MenuItemViewModel? oldValue, MenuItemViewModel? newValue)
    {
        if (newValue is null && SelectedRepo is null)
        {
            SelectedMenuItem = MenuItems.First();
        }
        else if (oldValue is null && newValue is not null)
        {
            SelectedRepo = null;
        }
    }


    private PageViewModel? MakeRepoPage(RepoItemViewModel? repo)
    {
        if (repo is null)
        {
            return null;
        }

        return new RepoPageViewModel(repo.Repo);
    }
}
