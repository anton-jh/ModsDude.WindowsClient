using System.Collections.ObjectModel;

namespace ModsDude.WindowsClient.ViewModel.ViewModels;
public class StartPageViewModel
    : PageViewModel
{
    public StartPageViewModel()
    {
        Instances = [
            new("test 1")
        ];
    }


    public ObservableCollection<LocalInstanceViewModel> Instances { get; }
}


public class LocalInstanceViewModel(string name)
{
    public string Name { get; } = name;
}
