using ModsDude.WindowsClient.Model.Models;

namespace ModsDude.WindowsClient.ViewModel.Pages.StartPage;
public class RepoViewModelBuilder
{
    public RepoViewModel Build(CombinedRepo repo)
    {
        return repo switch
        {
            { LocalInstances: [] } => new NoInstanceRepoViewModel(repo),
            { LocalInstances: [_] } => new SingleInstanceRepoViewModel(repo),
            { LocalInstances: [..] } => new MultiInstanceRepoViewModel(repo)
        };
    }
}
