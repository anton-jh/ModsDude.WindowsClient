using ModsDude.WindowsClient.Application.Adapters.Configuration;
using ModsDude.WindowsClient.Application.Adapters.Exceptions;
using ModsDude.WindowsClient.Application.Adapters.Mods;
using MoonSharp.Interpreter;

namespace ModsDude.WindowsClient.Application.Adapters;
public class Adapter
{
    private readonly Script _script;

    public Adapter(Script script, Dictionary<string, ConfigVariable> variables)
    {
        _script = script;

        var variablesTable = DynValue.NewTable(_script);

        foreach (var variable in variables)
        {
            var value = DynValue.FromObject(_script, variable.Value);
            variablesTable.Table.Set(variable.Key, value);
        }

        _script.Globals[Globals.Variables] = variablesTable;
    }


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
}
