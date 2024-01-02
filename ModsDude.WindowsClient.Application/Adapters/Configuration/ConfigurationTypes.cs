using MoonSharp.Interpreter;

namespace ModsDude.WindowsClient.Application.Adapters.Configuration;

[MoonSharpUserData]
public static class ConfigurationTypes
{
    public static FolderPathConfigVariable FolderPath(string displayName, bool required = true)
    {
        return new FolderPathConfigVariable(displayName, required);
    }
}
