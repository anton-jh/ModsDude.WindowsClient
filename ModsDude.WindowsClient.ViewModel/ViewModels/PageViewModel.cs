namespace ModsDude.WindowsClient.ViewModel.ViewModels;
public abstract class PageViewModel : ViewModel
{
    public string PageTypeName => GetType().Name;
    public abstract void Init();
}
