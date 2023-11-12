using ModsDude.WindowsClient.Experiments.Adapters.FileSystem;
using ModsDude.WindowsClient.Experiments.Adapters.Persistence;
using ModsDude.WindowsClient.Experiments.Adapters.PipelineFunctions;
using ModsDude.WindowsClient.Experiments.Adapters.Types;
using System.Reflection;
using System.Text.Json;
using System.Xml.Linq;

namespace ModsDude.WindowsClient.Experiments.Adapters;

internal static class AdaptersTest
{
    public static async Task Run()
    {
        var context = new AdapterContext();

        var getModFilename =
            new GetFilename<string>(
                context,
                new PipelineEnd<string>()
            );

        var modInfoCreator =
            new CreateModInfo<FileAbstraction, ModInfo>(
                context,
                new PipelineEnd<ModInfo>()
            )
            {
                GetId = getModFilename
            };

        var modFileMapper =
            new MapList<FileAbstraction, ModInfo, IEnumerable<ModInfo>>(
                context,
                new PipelineEnd<IEnumerable<ModInfo>>()
            )
            {
                Mapper = modInfoCreator
            };

        var getModFiles =
            new GetFilesInDirectory<IEnumerable<ModInfo>>(
                context,
                modFileMapper
            );

        IPipelineConsumer<object, IEnumerable<ModInfo>> getMods = getModFiles;
            

        await getMods.ExecuteAsync(new { });



        //var type = modInfoCreator.GetType();
        //var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
        //var prop1 = props.First();
        //var propType = prop1.PropertyType;


        var description = new PipelineDisassembler().Disassemble(getMods);

        var json = JsonSerializer.Serialize(description, new JsonSerializerOptions()
        {
            WriteIndented = true
        });
        Console.WriteLine(json);


        //var assembled = new PipelineAssembler().Assemble<Unit, IEnumerable<ModInfo>>(description);

        var functionName = description.First().FunctionName;
        var namespaceName = typeof(CreateModInfo<,>).Namespace;
        var type = Type.GetType(namespaceName + "." + functionName)!;
        var constructed = type.MakeGenericType(typeof(FileAbstraction));

    }
}

//type.BaseType.GetGenericArguments()
//{System.Type[3]}
    //[0]: { Name = "Object" FullName = "System.Object"}
//[1]: { Name = "IEnumerable`1" FullName = "System.Collections.Generic.IEnumerable`1[[ModsDude.WindowsClient.Experiments.Adapters.FileSystem.FileAbstraction, ModsDude.WindowsClient.Experiments, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]"}
//[2]: { Name = "TReturn" FullName = null}