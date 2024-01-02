using MoonSharp.Interpreter;

namespace ModsDude.WindowsClient.Application.Adapters.Configuration;

[MoonSharpUserData]
public abstract class ConfigVariable(string displayName, bool required)
{
    public string DisplayName { get; } = displayName;
    public bool Required { get; } = required;
    public object? Value { get; [MoonSharpHidden] protected set; }
}
