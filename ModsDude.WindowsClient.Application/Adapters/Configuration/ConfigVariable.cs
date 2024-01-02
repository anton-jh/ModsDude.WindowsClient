using MoonSharp.Interpreter;

namespace ModsDude.WindowsClient.Application.Adapters.Configuration;

[MoonSharpUserData]
public abstract class ConfigVariable(string displayName)
{
    public string DisplayName { get; } = displayName;
}
