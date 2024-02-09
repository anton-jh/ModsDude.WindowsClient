using CommunityToolkit.Mvvm.ComponentModel;
using ModsDude.WindowsClient.ViewModel.Pages;

namespace ModsDude.WindowsClient.ViewModel.ViewModels;
public partial class NewRepoItemViewModel
    : ObservableObject, IMenuItemViewModel
{
    [ObservableProperty]
    private string _title = "New repo";


    public PageViewModel GetPage()
    {
        return new CreateRepoPageViewModel();
    }
}
