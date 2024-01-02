using ModsDude.WindowsClient.Application.Adapters;
using ModsDude.WindowsClient.Application.Adapters.Configuration;
using System.Reflection;


using var scriptStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ModsDude.WindowsClient.Experiments.Scripts.farmsim.lua");
using var scriptReader = new StreamReader(scriptStream!);

var adapter = new Adapter(scriptReader.ReadToEnd());

(adapter.Config.Variables.First() as FolderPathConfigVariable)!.Value = new(@"C:\Users\anton\Documents\My Games\FarmingSimulator2022");


adapter.GetAllMods();

Console.WriteLine();
