using MoonSharp.Interpreter;

namespace ModsDude.WindowsClient.Application.Adapters.Savegames;

[MoonSharpUserData]
public class SavegameInfo
{
    private SavegameInfo(string id, string name, Func<Stream> getStream)
    {
        Id = id;
        Name = name;
        GetStream = getStream;
    }


    public string Id { get; }
    public string Name { get; }
    public Func<Stream> GetStream { get; }


    public static SavegameInfo FromFile(Script script, string id, string name, FileInfo file)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(file);

        return new(id, name, () =>
        {
            var stream = file.OpenRead();
            script.TrackDisposable(stream);
            return stream;
        });
    }
}
