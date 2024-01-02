using MoonSharp.Interpreter;

namespace ModsDude.WindowsClient.Application.Adapters.Configuration;

[MoonSharpUserData]
public static class ConfigurationTypes
{
    public static FolderPathConfigVariable FolderPath(string displayName)
    {
        return new FolderPathConfigVariable(displayName);
    }
}
