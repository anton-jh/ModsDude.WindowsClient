using ModsDude.WindowsClient.ViewModel.ViewModels;

namespace ModsDude.WindowsClient.ViewModel.Services;
public class NavigationService(
    MainWindowViewModel mainWindowViewModel)
{
    public void Navigate(PageViewModel pageViewModel)
    {
        mainWindowViewModel.Page = pageViewModel;
        pageViewModel.Init();
    }
}
