using ModsDude.WindowsClient.Model.Models;

namespace ModsDude.WindowsClient.ViewModel.Pages;
public class DesignTimeRepoPageViewModel
    : RepoPageViewModel
{
    public DesignTimeRepoPageViewModel()
        : base(new CombinedRepo()
        {
            Id = default,
            Name = "Test repo 123"
        })
    {
    }
}
