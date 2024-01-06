using ModsDude.WindowsClient.Application.Adapters.Exceptions;
using ModsDude.WindowsClient.Application.Adapters.Mods;
using MoonSharp.Interpreter;

namespace ModsDude.WindowsClient.Application.Adapters;
public class AdapterScope(Script script) : IDisposable
{
    public void Dispose()
    {
        script.DisposeTracked();
        GC.SuppressFinalize(this);
    }

    public List<ModInfo> GetAllMods()
    {
        var function = script.Globals.Get(Globals.GetAllMods);
        var result = script.Call(function);

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

        return mods;
    }
}
