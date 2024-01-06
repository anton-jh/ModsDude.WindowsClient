using MoonSharp.Interpreter;

namespace ModsDude.WindowsClient.Domain.Adapters.Configuration;

[MoonSharpUserData]
public static class ConfigurationTypes
{
    public static FolderPathConfigVariable FolderPath(string displayName, string? description, bool required = true)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(displayName);

        return new FolderPathConfigVariable(displayName, description, required);
    }
}
