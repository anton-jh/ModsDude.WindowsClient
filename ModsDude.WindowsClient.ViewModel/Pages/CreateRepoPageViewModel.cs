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
    private bool _useModsFeature = true;

    [ObservableProperty]
    private bool _useSavegamesFeature = true;

    [ObservableProperty]
    private string _modsScript = "";

    [ObservableProperty]
    private string _savegamesScript = "";


    partial void OnUseModsFeatureChanged(bool value)
    {
        if (value == false)
        {
            UseSavegamesFeature = true;
        }
    }

    partial void OnUseSavegamesFeatureChanged(bool value)
    {
        if (value == false)
        {
            UseModsFeature = true;
        }
    }


    [RelayCommand]
    private async Task Submit(CancellationToken cancellationToken)
    {
        await repoService.CreateRepo(
            Name,
            UseModsFeature ? ModsScript : null,
            UseSavegamesFeature ? SavegamesScript : null,
            cancellationToken);
    }
}
