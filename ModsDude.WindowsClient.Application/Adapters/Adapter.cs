using ModsDude.WindowsClient.Application.Adapters.Configuration;
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


    public AdapterScope CreateScope()
    {
        return new(_script);
    }
}
