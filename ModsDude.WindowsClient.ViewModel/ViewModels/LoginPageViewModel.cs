using ModsDude.WindowsClient.Application.Authentication;
using ModsDude.WindowsClient.ViewModel.Commands;
using System.Windows.Input;

namespace ModsDude.WindowsClient.ViewModel.ViewModels;
public class LoginPageViewModel : PageViewModel
{
    private readonly LoginService _loginService;


    public LoginPageViewModel(LoginService loginService)
    {
        _loginService = loginService;

        LoginCommand = new AsyncRelayCommand(Login);
    }

    public LoginPageViewModel()
    {
        _loginService = null!;
        LoginCommand = null!;
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


    private Task Login()
    {
        return _loginService.Login();
    }
}
