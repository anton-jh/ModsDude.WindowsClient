using MoonSharp.Interpreter;
using System.IO.Compression;

namespace ModsDude.WindowsClient.Domain.Adapters.FileSystem.ZipFiles;
public class ZipArchiveEntryProxy(ZipArchiveEntry entry)
{
    public string Name => entry.Name;
    public string FullName => entry.FullName;


    public Stream Open(Script script)
    {
        var stream = entry.Open();
        script.TrackDisposable(stream);

        return stream;
    }
}
