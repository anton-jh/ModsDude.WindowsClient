namespace ModsDude.WindowsClient.Model.Helpers;
internal static class FileSystemHelper
{
    public static string GetDbDirectory()
    {
        var localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        return Path.Combine(localAppDataPath, "ModsDude");
    }
}
