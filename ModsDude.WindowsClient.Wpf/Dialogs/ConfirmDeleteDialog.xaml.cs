using System.Windows;
using System.Windows.Input;

namespace ModsDude.WindowsClient.Wpf.Dialogs;
public partial class ConfirmDeleteDialog : Window
{
    private readonly string _repoName;


    public ConfirmDeleteDialog(string repoName)
    {
        InitializeComponent();
        _repoName = repoName;
    }


    private void ConfirmTextBox_TextInput(object sender, TextCompositionEventArgs e)
    {
        confirmButton.IsEnabled = CheckConfirmed();
    }

    private void ButtonCancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }

    private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = CheckConfirmed();
    }

    private bool CheckConfirmed()
    {
        return confirmTextBox.Text == _repoName;
    }
}
