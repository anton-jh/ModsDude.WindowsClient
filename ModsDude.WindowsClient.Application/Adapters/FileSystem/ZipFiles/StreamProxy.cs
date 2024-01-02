namespace ModsDude.WindowsClient.Application.Adapters.FileSystem.ZipFiles;
public class StreamProxy(Stream stream)
{
    public void Close()
    {
        stream.Close();
    }
}
