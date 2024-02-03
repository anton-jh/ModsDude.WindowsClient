using ModsDude.WindowsClient.Model.Services;
using ModsDude.WindowsClient.ViewModel.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ModsDude.WindowsClient.ViewModel.ViewModels;
public class StartPageViewModel : PageViewModel
{
    private readonly RepoService _repoService;


    public StartPageViewModel(
        RepoService repoService)
    {
        _repoService = repoService;

        RefreshCommand = new RefreshRepoListCommand(repoService, Repos, ThrowAsyncException);
    }


    public ObservableCollection<StartPageRepoViewModel> Repos { get; } = [];

    public ICommand RefreshCommand { get; private set; }



    private static void ThrowAsyncException(Exception exception)
    {
        throw exception;
    }
}


public class StartPageRepoViewModel
{
    public required string Name { get; init; }
}
