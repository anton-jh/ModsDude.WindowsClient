using CommunityToolkit.Mvvm.Input;
using ModsDude.WindowsClient.Model.Services;
using ModsDude.WindowsClient.ViewModel.Services;

namespace ModsDude.WindowsClient.ViewModel.Pages;
public partial class RepoAdminPageViewModel(
    RepoService repoService,
    IDialogService dialogService,
    Guid repoId)
{
    [RelayCommand]
    private async Task StartDeleteRepo()
    {
        var dialogResult = dialogService.ConfirmDelete() // todo use repo id and reposervice to get name and so on.
    }
}
