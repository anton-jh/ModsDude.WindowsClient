namespace ModsDude.WindowsClient.ViewModel.ViewModels;
public class MenuItemViewModel(string title, PageViewModel page)
{
    public string Title { get; } = title;
    public PageViewModel Page { get; } = page;
}
