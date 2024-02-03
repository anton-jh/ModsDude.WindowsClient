using ModsDude.WindowsClient.Model.Services;
using ModsDude.WindowsClient.ViewModel.ViewModels;

namespace ModsDude.WindowsClient.ViewModel.ViewModelFactories;
public class StartPageViewModelFactory(
    RepoService repoService)
{
    public StartPageViewModel Create()
    {
        return new StartPageViewModel(repoService);
    }
}
