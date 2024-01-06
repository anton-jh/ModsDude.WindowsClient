using ModsDude.WindowsClient.Application.Adapters.FileSystem;
using MoonSharp.Interpreter;

namespace ModsDude.WindowsClient.Application.Adapters.Configuration;

[MoonSharpUserData]
public class FolderPathConfigVariable(string displayName, string? description, bool required)
    : ConfigVariable(displayName, description, required)
{
    public void SetValue(DirectoryInfo value)
    {
        Value = new AllowedPath(value.FullName);
    }
}
