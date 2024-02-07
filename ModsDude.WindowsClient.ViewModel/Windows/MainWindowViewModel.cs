using CommunityToolkit.Mvvm.ComponentModel;
using ModsDude.WindowsClient.ViewModel.Pages;

namespace ModsDude.WindowsClient.ViewModel.Windows;
public partial class MainWindowViewModel
    : ObservableObject
{
    [ObservableProperty]
    private PageViewModel _currentPage = new MainPageViewModel();
}
