using ModsDude.WindowsClient.Application.Adapters.Configuration;
using ModsDude.WindowsClient.Application.Adapters.Exceptions;
using ModsDude.WindowsClient.Application.Adapters.FileSystem;
using ModsDude.WindowsClient.Application.Adapters.Mods;
using MoonSharp.Interpreter;

namespace ModsDude.WindowsClient.Application.Adapters;
public class Adapter
{
    private static bool _setupComplete = false;

    private readonly Script _script;


    public Adapter(string script)
    {
        if (!_setupComplete)
        {
            Setup();
        }

        _script = new Script(CoreModules.Preset_HardSandbox);

        Config = new();
        _script.Globals["config"] = Config;

        _script.Globals["fs"] = typeof(FileSystemFacade);

        _script.Globals["mod"] = typeof(ModInfo);

        _script.DoString(script);

        Globals.ValidateScript(_script);
    }


    public Config Config { get; }


    public List<ModInfo> GetAllMods()
    {
        var function = _script.Globals.Get(Globals.GetAllMods);
        var result = _script.Call(function);

        if (result.Type is not DataType.Table)
        {
            throw InvalidAdapterException.ReturnType(Globals.GetAllMods);
        }

        return result.Table.Values
            .Select(x =>
            {
                if (x.Type is not DataType.UserData || x.UserData.Object is not ModInfo mod)
                {
                    throw InvalidAdapterException.ReturnType(Globals.GetAllMods);
                }

                return mod;
            })
            .ToList();
    }


    private static void Setup()
    {
        UserData.RegisterAssembly(typeof(Adapter).Assembly);

        UserData.RegisterProxyType<FileProxy, FileInfo>(x => new FileProxy(x));

        _setupComplete = true;
    }
}
