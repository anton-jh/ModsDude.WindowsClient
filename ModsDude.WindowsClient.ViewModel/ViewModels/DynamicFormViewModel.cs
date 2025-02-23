using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace ModsDude.WindowsClient.ViewModel.ViewModels;

public partial class DynamicFormViewModel(IEnumerable<IInputViewModel> inputs)
    : ObservableObject
{
    private readonly ObservableCollection<IInputViewModel> _inputs = [.. inputs];


    public ObservableCollection<IInputViewModel> Inputs => _inputs;
}


public interface IInputViewModel
{
    string Label { get; }
    bool Required { get; }
    object? Value { get; }
}

public partial class SimpleInputViewModel
    : ObservableObject, IInputViewModel
{
    private object? _value;

    [ObservableProperty]
    private bool _isValid;


    public required Type Type { get; init; }
    public required string Label { get; init; }
    public required Action<object?> Setter { get; init; }
    public required bool Required { get; init; }
    public required object? Value
    {
        get => _value;
        set
        {
            OnPropertyChanging(nameof(Value));
            _value = value;
            Setter(value);
            OnPropertyChanged(nameof(Value));

            IsValid = !Required || value is not null;
        }
    }
}
