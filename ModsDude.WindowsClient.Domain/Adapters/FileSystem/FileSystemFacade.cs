using MoonSharp.Interpreter;

namespace ModsDude.WindowsClient.Domain.Adapters.FileSystem;

[MoonSharpUserData]
public static class FileSystemFacade
{
    public static bool FolderExists(AllowedPath path)
    {
        var folder = new DirectoryInfo(path.Value);
        return folder.Exists;
    }

    public static List<FileInfo>? GetFilesInFolder(AllowedPath? path, string? pattern = null)
    {
        if (path is null)
        {
            return null;
        }

        var folder = new DirectoryInfo(path.Value);

        if (!folder.Exists)
        {
            return null;
        }

        return (pattern is not null
            ? folder.EnumerateFiles(pattern)
            : folder.EnumerateFiles())
                .ToList();
    }

    public static List<DirectoryInfo>? GetFoldersInFolder(AllowedPath? path, string? pattern = null)
    {
        if (path is null)
        {
            return null;
        }

        var folder = new DirectoryInfo(path.Value);

        if (!folder.Exists)
        {
            return null;
        }

        return (pattern is not null
            ? folder.EnumerateDirectories(pattern)
            : folder.EnumerateDirectories())
                .ToList();
    }
}
