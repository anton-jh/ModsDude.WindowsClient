using ModsDude.WindowsClient.ViewModel.ViewModelFactories;

namespace ModsDude.WindowsClient.ViewModel.ViewModels;
public class MainWindowViewModel : ViewModel
{
    private readonly LoginPageViewModelFactory _loginPageViewModelFactory;


    public MainWindowViewModel(LoginPageViewModelFactory loginPageViewModelFactory)
    {
        _loginPageViewModelFactory = loginPageViewModelFactory;

        _page = _loginPageViewModelFactory.Create();
    }

    public MainWindowViewModel()
    {
        _loginPageViewModelFactory = null!;
        _page = new LoginPageViewModel();
    }


    private PageViewModel _page;
    public PageViewModel Page
    {
        get => _page;
        set
        {
            _page = value;
            OnPropertyChanged();
        }
    }
}
