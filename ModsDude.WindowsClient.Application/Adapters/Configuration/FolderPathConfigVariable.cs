using ModsDude.WindowsClient.Application.Adapters.FileSystem;
using MoonSharp.Interpreter;

namespace ModsDude.WindowsClient.Application.Adapters.Configuration;

[MoonSharpUserData]
public class FolderPathConfigVariable(string displayName)
    : ConfigVariable(displayName)
{
    public AllowedPath? Value { get; [MoonSharpHidden] set; }
}
