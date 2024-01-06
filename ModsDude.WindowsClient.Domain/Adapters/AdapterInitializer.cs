using ModsDude.WindowsClient.Domain.Adapters.Configuration;
using ModsDude.WindowsClient.Domain.Adapters.Exceptions;
using ModsDude.WindowsClient.Domain.Adapters.FileSystem;
using ModsDude.WindowsClient.Domain.Adapters.FileSystem.Xml;
using ModsDude.WindowsClient.Domain.Adapters.FileSystem.ZipFiles;
using ModsDude.WindowsClient.Domain.Adapters.Mods;
using MoonSharp.Interpreter;
using System.IO.Compression;
using System.Xml;

namespace ModsDude.WindowsClient.Domain.Adapters;
public class AdapterInitializer
{
    private static bool _setupComplete = false;

    private readonly Script _script;


    public AdapterInitializer(string script)
    {
        if (!_setupComplete)
        {
            Setup();
        }

        _script = new Script(CoreModules.Preset_HardSandbox);

        _script.Globals["config"] = typeof(ConfigurationTypes);
        _script.Globals["fs"] = typeof(FileSystemFacade);
        _script.Globals["mod"] = typeof(ModInfo);
        _script.Globals["zip"] = typeof(ZipFacade);
        _script.Globals["xml"] = typeof(XmlFacade);

        _script.DoString(script);
        _script.DisposeTracked();

        Globals.ValidateScript(_script);
    }


    public Dictionary<string, ConfigVariable> GetVariables()
    {
        var table = _script.Globals.Get(Globals.Variables);

        if (table.IsNil())
        {
            return [];
        }

        return table.Table.Pairs.ToDictionary(
            pair => pair.Key.String,
            pair =>
            {
                if (pair.Value.Type is DataType.UserData && pair.Value.UserData.Object is ConfigVariable variable)
                {
                    return variable;
                }
                throw new InvalidAdapterException($"Global '{Globals.Variables}' contains an invalid value");
            });
    }

    public Adapter Initialize(Dictionary<string, ConfigVariable> variables)
    {
        var missingRequired = variables.Values.FirstOrDefault(x => x.Value is null && x.Required);

        if (missingRequired is not null)
        {
            throw new ArgumentException($"Missing required config variable '{missingRequired.DisplayName}'", nameof(variables));
        }

        return new Adapter(_script, variables);
    }


    private static void Setup()
    {
        UserData.RegisterAssembly(typeof(AdapterInitializer).Assembly);

        UserData.RegisterProxyType<FileProxy, FileInfo>(x => new(x));
        UserData.RegisterProxyType<ZipArchiveProxy, ZipArchive>(x => new(x));
        UserData.RegisterProxyType<ZipArchiveEntryProxy, ZipArchiveEntry>(x => new(x));
        UserData.RegisterProxyType<StreamProxy, Stream>(x => new(x));
        UserData.RegisterProxyType<XmlDocumentProxy, XmlDocument>(x => new(x));

        _setupComplete = true;
    }
}
