using ModsDude.WindowsClient.Model.Models;
using ModsDude.WindowsClient.ViewModel.Pages;

namespace ModsDude.WindowsClient.ViewModel.ViewModels;
public class RepoItemViewModel(CombinedRepo repo)
    : IMenuItemViewModel
{
    public string Title => repo.Name;

    public PageViewModel GetPage()
    {
        return new RepoPageViewModel(repo);
    }
}
