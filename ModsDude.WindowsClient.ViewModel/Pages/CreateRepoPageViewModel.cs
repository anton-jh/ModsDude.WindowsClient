using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModsDude.WindowsClient.Model.Services;

namespace ModsDude.WindowsClient.ViewModel.Pages;
public partial class CreateRepoPageViewModel(
    RepoService repoService)
    : PageViewModel
{
    [ObservableProperty]
    private string _name = "New repo";

    [ObservableProperty]
    private string _adapterId = "";

    [ObservableProperty]
    private string _adapterConfiguration = "";


    [RelayCommand]
    private async Task Submit(CancellationToken cancellationToken)
    {
        await repoService.CreateRepo(
            Name,
            AdapterId,
            AdapterConfiguration,
            cancellationToken);
    }
}
