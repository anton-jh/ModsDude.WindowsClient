using MoonSharp.Interpreter;

namespace ModsDude.WindowsClient.Application.Adapters.Configuration;

[MoonSharpUserData]
public class Config
{
    private List<ConfigVariable> _variables = new();


    [MoonSharpHidden]
    public IEnumerable<ConfigVariable> Variables => _variables;


    public FolderPathConfigVariable FolderPath(string displayName)
    {
        var variable = new FolderPathConfigVariable(displayName);
        _variables.Add(variable);
        return variable;
    }
}
