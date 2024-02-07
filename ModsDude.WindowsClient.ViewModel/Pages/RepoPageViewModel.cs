using CommunityToolkit.Mvvm.ComponentModel;
using ModsDude.WindowsClient.Model.Models;
using ModsDude.WindowsClient.ViewModel.ViewModels;
using System.Collections.ObjectModel;

namespace ModsDude.WindowsClient.ViewModel.Pages;
public partial class RepoPageViewModel
    : PageViewModel
{
    private readonly CombinedRepo _repo;


    public RepoPageViewModel(CombinedRepo repo)
    {
        _repo = repo;
        _name = repo.Name;

        MenuItems = [
            new MenuItemViewModel("Overview", new ExamplePageViewModel($"Repo overview ({repo.Name})")),
            new MenuItemViewModel("Setup", new ExamplePageViewModel($"Setup repo ({repo.Name})"))
        ];
        _selectedMenuItem = MenuItems.First();
    }


    [ObservableProperty]
    private string _name;

    private IMenuItemViewModel _selectedMenuItem;
    public IMenuItemViewModel SelectedMenuItem
    {
        get => _selectedMenuItem;
        set
        {
            OnPropertyChanging(nameof(SelectedMenuItem));
            _selectedMenuItem = null!;
            OnPropertyChanged(nameof(SelectedMenuItem));

            OnPropertyChanging(nameof(SelectedMenuItem));
            OnPropertyChanging(nameof(CurrentPage));
            _selectedMenuItem = value;
            OnPropertyChanged(nameof(SelectedMenuItem));
            OnPropertyChanged(nameof(CurrentPage));
        }
    }

    public PageViewModel CurrentPage => SelectedMenuItem.GetPage();

    public ObservableCollection<IMenuItemViewModel> MenuItems { get; }

    public ObservableCollection<IMenuItemViewModel> Instances { get; } = [
        new MenuItemViewModel("Game", new ExamplePageViewModel("Instance (Game)")),
        new MenuItemViewModel("Dedicated server", new ExamplePageViewModel("Instance (Dedicated server)"))
    ];

    public ObservableCollection<IMenuItemViewModel> Profiles { get; } = [
        new MenuItemViewModel("Profile 1", new ExamplePageViewModel("Profile 1")),
        new MenuItemViewModel("Profile 2", new ExamplePageViewModel("Profile 2")),
        new MenuItemViewModel("Profile 3", new ExamplePageViewModel("Profile 3")),
        new MenuItemViewModel("Profile 4", new ExamplePageViewModel("Profile 4")),
        new MenuItemViewModel("Profile 5", new ExamplePageViewModel("Profile 5")),
        new MenuItemViewModel("Profile 6", new ExamplePageViewModel("Profile 6")),
        new MenuItemViewModel("Profile 7", new ExamplePageViewModel("Profile 7")),
        new MenuItemViewModel("Profile 8", new ExamplePageViewModel("Profile 8"))
    ];
}
