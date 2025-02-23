using ModsDude.WindowsClient.Model.Models.ValueTypes;
using ModsDude.WindowsClient.ViewModel.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ModsDude.WindowsClient.Wpf.DynamicForms;
public class FormInputDataTemplateSelector : DataTemplateSelector
{
    public required DataTemplate DirectoryPathTemplate { get; set; }


    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is SimpleInputViewModel simpleInput && simpleInput.Type == typeof(DirectoryPath))
        {
            return DirectoryPathTemplate;
        }

        return base.SelectTemplate(item, container);
    }
}
