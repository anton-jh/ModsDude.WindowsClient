using System.IO.Compression;

namespace ModsDude.WindowsClient.Application.Adapters.FileSystem.ZipFiles;
public class ZipArchiveProxy(ZipArchive archive)
{
    public ZipArchiveEntry? Get(string path)
    {
        return archive.GetEntry(path);
    }
}
