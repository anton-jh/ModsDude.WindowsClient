using ModsDude.WindowsClient.Application.Adapters.Exceptions;
using MoonSharp.Interpreter;

namespace ModsDude.WindowsClient.Application.Adapters;
internal static class Globals
{
    public static string Variables => "variables";
    public static string GetAllMods => "getAllMods";


    public static void ValidateScript(Script script)
    {
        ValidateGlobal(script, GetAllMods, DataType.Function);
        ValidateGlobal(script, Variables, DataType.Table, optional: true);
    }


    private static void ValidateGlobal(Script script, string name, DataType type, bool optional = false)
    {
        var value = script.Globals.Get(name);

        if (value.IsNil() && optional)
        {
            return;
        }


        if (value.IsNil() || value.Type != type)
        {
            throw InvalidAdapterException.MissingGlobal(name);
        }
    }
}
