using CommunityToolkit.Mvvm.ComponentModel;
using ModsDude.WindowsClient.Model.Models;
using System.Collections.ObjectModel;

namespace ModsDude.WindowsClient.ViewModel.ViewModels;
public partial class RepoPageViewModel(
    CombinedRepo repo)
    : PageViewModel
{
    [ObservableProperty]
    private string _name = repo.Name;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentPage))]
    private MenuItemViewModel? _selectedMenuItem;


    public PageViewModel? CurrentPage => SelectedMenuItem?.Page;


    public ObservableCollection<MenuItemViewModel> MenuItems { get; } = [
        new("Setup", new ExamplePageViewModel("Setup repo"))
    ];
}
