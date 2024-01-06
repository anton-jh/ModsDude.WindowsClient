using ModsDude.WindowsClient.ViewModel.ViewModelFactories;

namespace ModsDude.WindowsClient.ViewModel.ViewModels;
public class MainWindowViewModel(StartPageViewModelFactory startPageViewModelFactory)
    : ViewModel
{
    public MainWindowViewModel()
        : this(new StartPageViewModelFactory())
    {
    }


    private PageViewModel _page = startPageViewModelFactory.Create();
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
