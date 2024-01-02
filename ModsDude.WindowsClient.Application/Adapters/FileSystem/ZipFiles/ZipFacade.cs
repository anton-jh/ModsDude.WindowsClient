using MoonSharp.Interpreter;
using System.IO.Compression;

namespace ModsDude.WindowsClient.Application.Adapters.FileSystem.ZipFiles;

[MoonSharpUserData]
public static class ZipFacade
{
    public static ZipArchive? Read(Script script, FileInfo fileInfo)
    {
        if (!fileInfo.Exists)
        {
            return null;
        }

        var archive = ZipFile.OpenRead(fileInfo.FullName);
        script.TrackDisposable(archive);

        return archive;
    }
}
