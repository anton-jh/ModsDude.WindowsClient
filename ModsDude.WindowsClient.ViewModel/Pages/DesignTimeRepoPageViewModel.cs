using ModsDude.WindowsClient.Model.Models;

namespace ModsDude.WindowsClient.ViewModel.Pages;
public class DesignTimeRepoPageViewModel
    : RepoPageViewModel
{
    public DesignTimeRepoPageViewModel()
        : base(new RepoModel()
        {
            Id = default,
            Name = "Test repo 123",
            ModsScript = "modsmodsmodsmods",
            SavegamesScript = "savegamessavegames"
        }, null!)
    {
    }
}
