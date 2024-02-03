namespace ModsDude.WindowsClient.ViewModel.ViewModels;
public class MainWindowViewModel
    : ViewModel
{
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
}
