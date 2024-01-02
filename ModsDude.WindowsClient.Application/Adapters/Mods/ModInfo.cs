using MoonSharp.Interpreter;

namespace ModsDude.WindowsClient.Application.Adapters.Mods;

[MoonSharpUserData]
public record ModInfo(string Id, string Name, string Version, FileInfo File);
