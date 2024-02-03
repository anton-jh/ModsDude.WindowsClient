using ModsDude.WindowsClient.ViewModel.ViewModelFactories;

namespace ModsDude.WindowsClient.ViewModel.ViewModels;
public class MainWindowViewModel(StartPageViewModelFactory startPageViewModelFactory)
    : ViewModel
{
    public MainWindowViewModel()
        : this(null!)
    {
    }


    private PageViewModel _page = new LoginPageViewModel();
    public PageViewModel Page
    {
        get => _page;
        set
        {
            _page = value;
            OnPropertyChanged();
        }
    }


    public void NavigateToStartPage()
    {
        Page = startPageViewModelFactory.Create();
    }
}
