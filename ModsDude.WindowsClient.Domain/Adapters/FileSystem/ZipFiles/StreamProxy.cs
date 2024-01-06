namespace ModsDude.WindowsClient.Domain.Adapters.FileSystem.ZipFiles;
public class StreamProxy(Stream stream)
{
    public void Close()
    {
        stream.Close();
    }
}
