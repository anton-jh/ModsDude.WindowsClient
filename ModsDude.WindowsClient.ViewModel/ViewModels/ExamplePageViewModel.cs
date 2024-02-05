namespace ModsDude.WindowsClient.ViewModel.ViewModels;
public class ExamplePageViewModel(string text) : PageViewModel
{
    public string Text { get; } = text;
}
