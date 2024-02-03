using ModsDude.WindowsClient.Model.Models;

namespace ModsDude.WindowsClient.ViewModel.Pages.StartPage;

public abstract class RepoViewModel(CombinedRepo repo)
{
    public Guid Id { get; } = repo.Id;
    public string Name { get; } = repo.Name;
    public CombinedRepo Repo { get; } = repo;
}


public class NoInstanceRepoViewModel(CombinedRepo repo)
    : RepoViewModel(repo);


public class SingleInstanceRepoViewModel(CombinedRepo repo)
    : RepoViewModel(repo);


public class MultiInstanceRepoViewModel(CombinedRepo repo)
    : RepoViewModel(repo);
