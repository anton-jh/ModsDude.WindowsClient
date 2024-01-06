using MoonSharp.Interpreter;

namespace ModsDude.WindowsClient.Application.Adapters.Mods;

[MoonSharpUserData]
public class ModInfo
{
    private ModInfo(string id, string version, string name, string description, Func<Stream> getStream)
    {
        Id = id;
        Version = version;
        Name = name;
        Description = description;
        GetStream = getStream;
    }


    public string Id { get; }
    public string Version { get; }
    public string Name { get; }
    public string Description { get; }
    public Func<Stream> GetStream { get; }


    public static ModInfo FromFile(Script script, string id, string version, string name, string? description, FileInfo file)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(version);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(file);

        return new(id, version, name, description ?? "", () =>
        {
            var stream = file.OpenRead();
            script.TrackDisposable(stream);
            return stream;
        });
    }
}
