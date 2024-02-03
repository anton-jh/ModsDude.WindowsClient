using ModsDude.WindowsClient.Model.Services;
using ModsDude.WindowsClient.ViewModel.Pages.EditRepoPage;
using ModsDude.WindowsClient.ViewModel.Services;
using ModsDude.WindowsClient.ViewModel.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ModsDude.WindowsClient.ViewModel.Pages.StartPage;
public class StartPageViewModel
    : PageViewModel
{
    private readonly RepoService _repoService;


    public StartPageViewModel(
        RepoService repoService,
        RepoViewModelBuilder repoViewModelBuilder,
        EditRepoPageViewModelFactory editRepoPageViewModelFactory,
        NavigationService navigationService)
    {
        _repoService = repoService;

        RefreshCommand = new RefreshRepoListCommand(repoService, Repos, ThrowAsyncException, repoViewModelBuilder);
        EditRepoCommand = new StartEditRepoCommand(this, editRepoPageViewModelFactory, navigationService);
    }

    public StartPageViewModel()
    {
        _repoService = null!;
        RefreshCommand = null!;
        EditRepoCommand = null!;

        Repos = [
            new NoInstanceRepoViewModel(new Model.Models.CombinedRepo()
            {
                Id = Guid.NewGuid(),
                Name = "Test repo",
                LocalInstances = []
            })
        ];
    }


    public ObservableCollection<RepoViewModel> Repos { get; } = [];

    private RepoViewModel? _selectedRepo;
    public RepoViewModel? SelectedRepo
    {
        get => _selectedRepo;
        set
        {
            _selectedRepo = value;
            OnPropertyChanged();
        }
    }

    public ICommand RefreshCommand { get; private set; }
    public ICommand EditRepoCommand { get; private set; }


    public override void Init()
    {
        RefreshCommand.Execute(null);
    }


    private static void ThrowAsyncException(Exception exception)
    {
        throw exception;
    }
}
