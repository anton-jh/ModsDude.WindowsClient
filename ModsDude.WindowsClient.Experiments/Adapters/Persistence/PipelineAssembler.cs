using ModsDude.WindowsClient.Experiments.Adapters.PipelineFunctions;
using ModsDude.WindowsClient.Experiments.Adapters.Types;

namespace ModsDude.WindowsClient.Experiments.Adapters.Persistence;

internal class PipelineAssembler
{
    private static readonly Dictionary<string, PipelineFunctionMetaData> _pipelineFunctions =
        typeof(PipelineFunctionMetaData)
        .Assembly
        .GetTypes()
        .Where(x => x.IsAssignableTo(typeof(PipelineFunctionMetaData)) && x != typeof(PipelineFunctionMetaData))
        .Select(x => (PipelineFunctionMetaData?)Activator.CreateInstance(x))
        .OfType<PipelineFunctionMetaData>()
        .ToDictionary(x => x.FunctionName);


    public IPipelineConsumer<TIn, TReturn> Assemble<TIn, TReturn>(IEnumerable<PipelineFunctionDescriptor> descriptors)
    {
        var next = new PipelineEnd<TReturn>();

        foreach (var function in descriptors.Reverse())
        {
            var metaData = _pipelineFunctions.GetValueOrDefault(function.FunctionName)
                ?? throw new AssemblingException($"Invalid function type {function.FunctionName}");

            var constructedType = metaData.FunctionType.MakeGenericType(metaData.GetTypeParameters<TIn, TReturn>().ToArray());
        }

        throw new NotImplementedException();
    }
}


internal abstract class PipelineFunctionMetaData
{
    public PipelineFunctionMetaData(Type type)
    {
        FunctionName = type.Name;
        FunctionType = type;
    }


    public string FunctionName { get; }
    public Type FunctionType { get; }


    public abstract IEnumerable<Type> GetTypeParameters<TIn, TReturn>();
}


internal class CreateModInfoMetaData : PipelineFunctionMetaData
{
    public CreateModInfoMetaData() : base(typeof(CreateModInfo<,>))
    {
    }

    public override IEnumerable<Type> GetTypeParameters<TIn, TReturn>()
    {
        yield return typeof(TIn);
        yield return typeof(TReturn);
    }
}

internal class GetFilenameMetaData : PipelineFunctionMetaData
{
    public GetFilenameMetaData() : base(typeof(GetFilename<>))
    {
    }

    public override IEnumerable<Type> GetTypeParameters<TIn, TReturn>()
    {
        yield return typeof(TReturn);
    }
}

internal class GetFilesInDirectoryMetaData : PipelineFunctionMetaData
{
    public GetFilesInDirectoryMetaData() : base(typeof(GetFilesInDirectory<>))
    {
    }

    public override IEnumerable<Type> GetTypeParameters<TIn, TReturn>()
    {
        yield return typeof(TReturn);
    }
}

internal class MapListMetaData : PipelineFunctionMetaData
{
    public MapListMetaData() : base(typeof(MapList<,,>))
    {
    }

    public override IEnumerable<Type> GetTypeParameters<TIn, TReturn>()
    {
        yield break;
    }
}
