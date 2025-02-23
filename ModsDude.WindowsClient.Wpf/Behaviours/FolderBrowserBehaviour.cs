using ModsDude.WindowsClient.Model.Models.ValueTypes;
using System.Windows;
using System.Windows.Forms;

namespace ModsDude.WindowsClient.Wpf.Behaviours
{
    public static class FolderBrowserBehavior
    {
        public static readonly DependencyProperty FolderPathTargetProperty =
            DependencyProperty.RegisterAttached(
                "FolderPathTarget",
                typeof(DirectoryPath),
                typeof(FolderBrowserBehavior),
                new PropertyMetadata(default(DirectoryPath), OnFolderPathTargetChanged));

        public static DirectoryPath GetFolderPathTarget(DependencyObject obj) =>
            (DirectoryPath)obj.GetValue(FolderPathTargetProperty);

        public static void SetFolderPathTarget(DependencyObject obj, DirectoryPath value) =>
            obj.SetValue(FolderPathTargetProperty, value);

        private static void OnFolderPathTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is System.Windows.Controls.Button button)
            {
                button.Click -= Button_Click;
                button.Click += Button_Click;
            }
        }

        private static void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.Button button)
            {
                var folderPath = GetFolderPathTarget(button);

                using var dialog = new FolderBrowserDialog { SelectedPath = folderPath.ToString() };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var newPath = new DirectoryPath(dialog.SelectedPath);
                    SetFolderPathTarget(button, newPath);

                    if (button.DataContext is not null)
                    {
                        var folderPathProperty = button.DataContext.GetType().GetProperty("FolderPath");
                        folderPathProperty?.SetValue(button.DataContext, newPath);
                    }
                }
            }
        }
    }
}
