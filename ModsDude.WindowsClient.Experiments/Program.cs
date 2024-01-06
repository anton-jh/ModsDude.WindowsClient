using ModsDude.WindowsClient.Domain.Adapters;
using ModsDude.WindowsClient.Domain.Adapters.Configuration;
using System.Reflection;


using var scriptStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ModsDude.WindowsClient.Experiments.Scripts.farmsim.lua");
using var scriptReader = new StreamReader(scriptStream!);

var initializer = new AdapterInitializer(scriptReader.ReadToEnd());

var variables = initializer.GetVariables();

(variables["modsFolder"] as FolderPathConfigVariable)!.SetValue(new DirectoryInfo(@"C:\Users\anton\Documents\My Games\FarmingSimulator2022\mods"));
(variables["savegamesFolder"] as FolderPathConfigVariable)!.SetValue(new DirectoryInfo(@"C:\Users\anton\Documents\My Games\FarmingSimulator2022"));

var adapter = initializer.Initialize(variables);
using var scope = adapter.CreateScope();

var mods = scope.GetAllMods();

foreach (var mod in mods)
{
    Console.WriteLine($"{{0,-{mods.MaxBy(x => x.Id.Length)?.Id.Length ?? 20}}}\t{{1,-10}}\t{{2}}\t{{3}}",
        mod.Id, mod.Version, mod.Name,
        GetUntilOrEmpty(mod.Description, ['\n', '\r']));
}

Console.ReadLine();


static string GetUntilOrEmpty(string text, params char[] any)
{
    if (string.IsNullOrWhiteSpace(text))
    {
        return string.Empty;
    }

    var charLocation = text.IndexOfAny(any);

    if (charLocation > 100)
    {
        return text[..100];
    }

    if (charLocation > 0)
    {
        return text[..charLocation];
    }

    return string.Empty;
}
