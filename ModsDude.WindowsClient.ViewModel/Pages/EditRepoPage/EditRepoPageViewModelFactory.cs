using ModsDude.WindowsClient.Model.Models;

namespace ModsDude.WindowsClient.ViewModel.Pages.EditRepoPage;
public class EditRepoPageViewModelFactory
{
    public EditRepoPageViewModel Create(CombinedRepo repo)
    {
        return new EditRepoPageViewModel(repo);
    }
}
