using ModsDude.WindowsClient.ViewModel.Commands;
using System.Security;
using System.Windows.Input;

namespace ModsDude.WindowsClient.ViewModel.ViewModels;
public class LoginPageViewModel : PageViewModel
{
    public LoginPageViewModel()
    {
        LoginCommand = new RelayCommand(() => { });
    }


    private string _username = "";
    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged();
        }
    }

    private string _password = "";
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoginCommand { get; }
}
