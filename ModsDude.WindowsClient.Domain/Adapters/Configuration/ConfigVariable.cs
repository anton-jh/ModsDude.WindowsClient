using MoonSharp.Interpreter;

namespace ModsDude.WindowsClient.Domain.Adapters.Configuration;

[MoonSharpUserData]
public abstract class ConfigVariable(string displayName, string? description, bool required)
{
    public string DisplayName { get; } = displayName;
    public string? Description { get; } = description;
    public bool Required { get; } = required;
    public object? Value { get; [MoonSharpHidden] protected set; }
}
