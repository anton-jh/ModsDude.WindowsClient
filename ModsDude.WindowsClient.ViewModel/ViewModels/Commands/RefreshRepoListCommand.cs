using ModsDude.WindowsClient.Model.Models;
using ModsDude.WindowsClient.Model.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ModsDude.WindowsClient.ViewModel.ViewModels.Commands;
public class RefreshRepoListCommand(
    RepoService repoService,
    ObservableCollection<StartPageRepoViewModel> list,
    Action<Exception> errorHandler)
    : ICommand
{
    private bool _loading = false;
    private CancellationToken _cancellationToken;


    public event EventHandler? CanExecuteChanged;


    public bool CanExecute(object? parameter)
    {
        return _loading == false;
    }

    public async void Execute(object? parameter)
    {
        _loading = true;

        IEnumerable<CombinedRepo> repos;

        try
        {
            repos = await repoService.GetRepos(_cancellationToken);
        }
        catch (Exception ex)
        {
            errorHandler(ex);
            return;
        }

        list.Clear();

        foreach (var repo in repos)
        {
            list.Add(new()
            {
                Name = repo.Name
            });
        }

        _loading = false;
    }
}
