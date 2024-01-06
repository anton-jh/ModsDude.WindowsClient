namespace ModsDude.WindowsClient.Domain.Adapters.FileSystem;

public class FileProxy(FileInfo fileInfo)
{
    public string Name { get; } = fileInfo.Name;
    public string FullName { get; } = fileInfo.FullName;
    public string Extension { get; } = fileInfo.Extension;
}
