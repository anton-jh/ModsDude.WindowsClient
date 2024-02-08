using CommunityToolkit.Mvvm.ComponentModel;
using ModsDude.WindowsClient.Model.Models;
using ModsDude.WindowsClient.ViewModel.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace ModsDude.WindowsClient.ViewModel.Pages;
public partial class RepoPageViewModel
    : PageViewModel
{
    private readonly CombinedRepo _repo;


    public RepoPageViewModel()
    {
        _repo = null!;
        _name = "Test repo 123";

        CreateMenu();
        _selectedMenuItem = MenuItems.First();
    }
    public RepoPageViewModel(CombinedRepo repo)
    {
        _repo = repo;
        _name = repo.Name;

        CreateMenu();
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

    public ObservableCollection<IMenuItemViewModel> MenuItems { get; private set; }

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
        new MenuItemViewModel("Profile 8", new ExamplePageViewModel("Profile 8")),
        new MenuItemViewModel("Profile 9", new ExamplePageViewModel("Profile 9")),
        new MenuItemViewModel("Profile 10", new ExamplePageViewModel("Profile 10")),
        new MenuItemViewModel("Profile 11", new ExamplePageViewModel("Profile 11")),
        new MenuItemViewModel("Profile 12", new ExamplePageViewModel("Profile 12")),
        new MenuItemViewModel("Profile 13", new ExamplePageViewModel("Profile 13")),
        new MenuItemViewModel("Profile 14", new ExamplePageViewModel("Profile 14")),
        new MenuItemViewModel("Profile 15", new ExamplePageViewModel("Profile 15")),
        new MenuItemViewModel("Profile 16", new ExamplePageViewModel("Profile 16")),
    ];


    [MemberNotNull(nameof(MenuItems))]
    private void CreateMenu()
    {
        MenuItems = [
            new MenuItemViewModel("Overview", new ExamplePageViewModel($"Repo overview ({Name})")),
            new MenuItemViewModel("Setup", new ExamplePageViewModel($"Setup repo ({Name})"))
        ];
    }
}
