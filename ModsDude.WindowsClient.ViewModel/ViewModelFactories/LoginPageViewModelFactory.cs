using ModsDude.WindowsClient.Application.Authentication;
using ModsDude.WindowsClient.ViewModel.ViewModels;

namespace ModsDude.WindowsClient.ViewModel.ViewModelFactories;
public class LoginPageViewModelFactory
{
    private readonly LoginService _loginService;


    public LoginPageViewModelFactory(LoginService loginService)
    {
        _loginService = loginService;
    }


    public LoginPageViewModel Create()
    {
        return new LoginPageViewModel(_loginService);
    }
}
