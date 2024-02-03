using ModsDude.WindowsClient.Model.Services;

namespace ModsDude.WindowsClient.ViewModel.Pages.StartPage;
public class StartPageViewModelFactory(
    RepoService repoService)
{
    public StartPageViewModel Create()
    {
        return new StartPageViewModel(repoService);
    }
}
