using CommunityToolkit.Mvvm.ComponentModel;

namespace ModsDude.WindowsClient.ViewModel.Pages;
public partial class CreateRepoPageViewModel
    : PageViewModel
{
    [ObservableProperty]
    private string _name = "New repo";
}
