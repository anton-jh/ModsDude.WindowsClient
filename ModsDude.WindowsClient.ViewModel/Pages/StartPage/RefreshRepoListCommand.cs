using ModsDude.WindowsClient.Model.Models;
using ModsDude.WindowsClient.Model.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ModsDude.WindowsClient.ViewModel.Pages.StartPage;
public class RefreshRepoListCommand(
    RepoService repoService,
    ObservableCollection<StartPageRepoViewModel> list,
    Action<Exception> errorHandler)
    : ICommand
{
    private bool _canExecute = true;


    public event EventHandler? CanExecuteChanged;


    public bool CanExecute(object? parameter) => _canExecute;

    public async void Execute(object? parameter)
    {
        SetCanExecute(false);

        IEnumerable<CombinedRepo> repos;

        try
        {
            repos = await repoService.GetRepos(default);
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

        SetCanExecute(true);
    }


    private void SetCanExecute(bool canExecute)
    {
        _canExecute = canExecute;
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}
