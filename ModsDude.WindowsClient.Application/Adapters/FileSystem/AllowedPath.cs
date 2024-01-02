using MoonSharp.Interpreter;

namespace ModsDude.WindowsClient.Application.Adapters.FileSystem;

[MoonSharpUserData]
public class AllowedPath(string upperLimit, string? value = null)
{
    [MoonSharpHidden]
    public string UpperLimit { get; } = upperLimit;
    public string Value { get; } = string.IsNullOrWhiteSpace(value)
        ? upperLimit
        : value;


    [MoonSharpUserDataMetamethod("__concat")]
    public static AllowedPath Concat(AllowedPath basePath, string relativePath)
    {
        var cleanedRelativePath = relativePath.TrimStart('/', '\\');
        var cleanedUpperLimit = basePath.UpperLimit.TrimEnd('/', '\\') + "\\";
        var newPath = Path.GetFullPath(cleanedRelativePath, basePath.Value);

        if (!newPath.StartsWith(cleanedUpperLimit))
        {
            throw new ScriptRuntimeException("The adapter tried to access a folder/file outside of your configured folders");
        }

        return new AllowedPath(basePath.UpperLimit, newPath.ToString());
    }
}
