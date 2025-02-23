using ModsDude.WindowsClient.Model.DynamicForms;
using ModsDude.WindowsClient.ViewModel.ViewModels;
using System.Reflection;

namespace ModsDude.WindowsClient.ViewModel.ViewModelFactories;
public class DynamicFormViewModelFactory
{
    public DynamicFormViewModel Create(object model)
    {
        var type = model.GetType();

        var inputs = new List<IInputViewModel>();

        foreach (var prop in type.GetProperties())
        {
            inputs.Add(new SimpleInputViewModel()
            {
                Label = GetLabel(prop),
                Required = CheckIsRequired(prop),
                Setter = x => prop.SetValue(model, x),
                Type = prop.PropertyType,
                Value = prop.GetValue(model)
            });
        }

        return new DynamicFormViewModel(inputs);
    }


    private static string GetLabel(PropertyInfo prop)
    {
        return prop.GetCustomAttribute<LabelAttribute>()
            ?.Text
            ?? prop.Name;
    }

    private static bool CheckIsRequired(PropertyInfo prop)
    {
        return prop.GetCustomAttribute<RequiredAttribute>()
            is not null;
    }
}
