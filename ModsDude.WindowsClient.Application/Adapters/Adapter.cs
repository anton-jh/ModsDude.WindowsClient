using ModsDude.WindowsClient.Application.Adapters.Configuration;
using ModsDude.WindowsClient.Application.Adapters.Exceptions;
using ModsDude.WindowsClient.Application.Adapters.FileSystem;
using ModsDude.WindowsClient.Application.Adapters.FileSystem.Xml;
using ModsDude.WindowsClient.Application.Adapters.FileSystem.ZipFiles;
using ModsDude.WindowsClient.Application.Adapters.Mods;
using MoonSharp.Interpreter;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Xml;

namespace ModsDude.WindowsClient.Application.Adapters;
public class Adapter
{
    private static bool _setupComplete = false;

    private readonly Script _script;


    public Adapter(string script, Dictionary<string, ConfigVariable>? variables)
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

        if (variables is not null)
        {
            SetVariablesGlobal(variables);
            Variables = variables;
        }
        else
        {
            GetVariablesGlobal();
        }
    }


    public Dictionary<string, ConfigVariable> Variables { get; private set; }


    public List<ModInfo> GetAllMods()
    {
        var function = _script.Globals.Get(Globals.GetAllMods);
        var result = _script.Call(function);

        if (result.Type is not DataType.Table)
        {
            throw InvalidAdapterException.ReturnType(Globals.GetAllMods);
        }

        var mods = result.Table.Values
            .Select(x =>
            {
                if (x.Type is DataType.UserData && x.UserData.Object is ModInfo mod)
                {
                    return mod;
                }

                throw InvalidAdapterException.ReturnType(Globals.GetAllMods);
            })
            .ToList();

        _script.DisposeTracked();

        return mods;
    }


    [MemberNotNull(nameof(Variables))]
    private void GetVariablesGlobal()
    {
        var table = _script.Globals.Get(Globals.Variables);
        Variables = table.Table.Pairs.ToDictionary(
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

    private void SetVariablesGlobal(Dictionary<string, ConfigVariable> variables)
    {
        var table = DynValue.NewTable(_script);

        foreach (var variable in variables)
        {
            var value = DynValue.FromObject(_script, variable.Value);
            table.Table.Set(variable.Key, value);
        }

        _script.Globals[Globals.Variables] = table;
    }


    private static void Setup()
    {
        UserData.RegisterAssembly(typeof(Adapter).Assembly);

        UserData.RegisterProxyType<FileProxy, FileInfo>(x => new(x));
        UserData.RegisterProxyType<ZipArchiveProxy, ZipArchive>(x => new(x));
        UserData.RegisterProxyType<ZipArchiveEntryProxy, ZipArchiveEntry>(x => new(x));
        UserData.RegisterProxyType<StreamProxy, Stream>(x => new(x));
        UserData.RegisterProxyType<XmlDocumentProxy, XmlDocument>(x => new(x));

        _setupComplete = true;
    }
}
