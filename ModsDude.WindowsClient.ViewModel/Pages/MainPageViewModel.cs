using System.Collections.ObjectModel;
using ModsDude.WindowsClient.ViewModel.ViewModels;

namespace ModsDude.WindowsClient.ViewModel.Pages;
public partial class MainPageViewModel
    : PageViewModel
{
    public MainPageViewModel()
    {
        _selectedMenuItem = MenuItems.First();
    }


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


    public ObservableCollection<IMenuItemViewModel> MenuItems { get; } = [
        new MenuItemViewModel("Home", new ExamplePageViewModel("Home page")),
        new MenuItemViewModel("Test", new ExamplePageViewModel("Test page"))
    ];

    public ObservableCollection<IMenuItemViewModel> Repos { get; } = [
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
}
