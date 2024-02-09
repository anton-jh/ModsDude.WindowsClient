namespace ModsDude.WindowsClient.Model.Helpers;
public static class FileSystemHelper
{
    public static string GetDbDirectory()
    {
        var localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        return Path.Combine(localAppDataPath, "ModsDude");
    }
}
