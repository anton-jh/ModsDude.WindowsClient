using ModsDude.WindowsClient.Model.Models;
using ModsDude.WindowsClient.ViewModel.ViewModels;

namespace ModsDude.WindowsClient.ViewModel.Pages.EditRepoPage;
public class EditRepoPageViewModel : PageViewModel
{
    private readonly Guid? _repoId;


    public EditRepoPageViewModel()
    {

    }

    public EditRepoPageViewModel(CombinedRepo repo)
    {
        _repoId = repo.Id;
        _name = repo.Name;
    }


    private string _name = "";
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    private bool _modsEnabled = true;
    public bool ModsEnabled
    {
        get => _modsEnabled;
        set
        {
            _modsEnabled = value;
            OnPropertyChanged();

            if (_modsEnabled == false && _savegamesEnabled == false)
            {
                SavegamesEnabled = true;
            }
        }
    }

    private string _modsScript = "";
    public string ModsScript
    {
        get => _modsScript;
        set
        {
            _modsScript = value;
            OnPropertyChanged();
        }
    }

    private bool _savegamesEnabled = true;
    public bool SavegamesEnabled
    {
        get => _savegamesEnabled;
        set
        {
            _savegamesEnabled = value;
            OnPropertyChanged();

            if (_savegamesEnabled == false && _modsEnabled == false)
            {
                ModsEnabled = true;
            }
        }
    }

    private string _savegamesScript = "";
    public string SavegamesScript
    {
        get => _savegamesScript;
        set
        {
            _savegamesScript = value;
            OnPropertyChanged();
        }
    }

    public bool IsEditMode => _repoId is not null;
    public bool IsCreateMode => !IsEditMode;


    public override void Init()
    {
    }
}
