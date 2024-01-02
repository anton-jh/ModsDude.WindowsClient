using ModsDude.WindowsClient.Application.Adapters;
using ModsDude.WindowsClient.Application.Adapters.Configuration;
using System.Reflection;


using var scriptStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ModsDude.WindowsClient.Experiments.Scripts.farmsim.lua");
using var scriptReader = new StreamReader(scriptStream!);

var initializer = new AdapterInitializer(scriptReader.ReadToEnd());

var variables = initializer.GetVariables();

(variables["modsFolder"] as FolderPathConfigVariable)!.SetValue(new DirectoryInfo(@"C:\Users\anton\Documents\My Games\FarmingSimulator2022\mods"));
(variables["savegamesFolder"] as FolderPathConfigVariable)!.SetValue(new DirectoryInfo(@"C:\Users\anton\Documents\My Games\FarmingSimulator2022"));

var adapter = initializer.Initialize(variables);

var mods = adapter.GetAllMods();

Console.WriteLine();
