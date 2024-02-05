using CommunityToolkit.Mvvm.ComponentModel;
using ModsDude.WindowsClient.Model.Models;

namespace ModsDude.WindowsClient.ViewModel.ViewModels;
public partial class RepoPageViewModel(
    CombinedRepo repo)
    : PageViewModel
{
    [ObservableProperty]
    private string _name = repo.Name;
}
