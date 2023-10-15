using ModsDude.WindowsClient.ViewModel.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ModsDude.WindowsClient.Wpf.Pages;
public partial class LoginPage : Page
{
    public LoginPage()
    {
        InitializeComponent();
    }


    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is LoginPageViewModel viewModel && sender is PasswordBox passwordBox)
        {
            viewModel.Password = passwordBox.Password;
        }
    }
}
