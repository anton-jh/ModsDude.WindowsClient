namespace ModsDude.WindowsClient.Domain.Adapters;


internal abstract class AdapterFunction<TIn, TOut, TReturn> : IPipelineConsumer<TIn, TReturn>
{
    public AdapterFunction(AdapterContext context, IPipelineConsumer<TOut, TReturn> next)
    {
        Context = context;
        Next = next;
    }


    protected AdapterContext Context { get; }
    public IPipelineConsumer<TOut, TReturn> Next { get; }


    public abstract Task<TReturn> ExecuteAsync(TIn input);
}


internal interface IPipelineConsumer<in TIn, TReturn>
{
    Task<TReturn> ExecuteAsync(TIn input);
}


internal class AdapterContext { }
internal record ModInfo(string Id);
internal interface IFile
{
    string GetFilename();
}


internal class MapAdapter<TIn, TOut, TReturn> : AdapterFunction<IEnumerable<TIn>, IEnumerable<TOut>, TReturn>
{
    public MapAdapter(AdapterContext context, IPipelineConsumer<IEnumerable<TOut>, TReturn> next)
        : base(context, next)
    {
    }


    public required IPipelineConsumer<TIn, TOut> Mapper { get; init; }


    public override async Task<TReturn> ExecuteAsync(IEnumerable<TIn> input)
    {
        var result = new List<TOut>();

        foreach (var item in input)
        {
            result.Add(await Mapper.ExecuteAsync(item));
        }

        return await Next.ExecuteAsync(result);
    }
}


internal class GetFilesInDirectory<TReturn> : AdapterFunction<object, IEnumerable<IFile>, TReturn>
{
    public GetFilesInDirectory(AdapterContext context, IPipelineConsumer<IEnumerable<IFile>, TReturn> next)
        : base(context, next)
    {
    }


    public override async Task<TReturn> ExecuteAsync(object input)
    {
        var files = Enumerable.Empty<IFile>(); // Get files in folder specified in Context

        return await Next.ExecuteAsync(files);
    }
}


internal class PipelineEnd<TReturn> : IPipelineConsumer<TReturn, TReturn>
{
    public Task<TReturn> ExecuteAsync(TReturn input)
    {
        return Task.FromResult(input);
    }
}


internal class GetFilename<TReturn> : AdapterFunction<IFile, string, TReturn>
{
    public GetFilename(AdapterContext context, IPipelineConsumer<string, TReturn> next)
        : base(context, next)
    {
    }


    public override async Task<TReturn> ExecuteAsync(IFile input)
    {
        return await Next.ExecuteAsync(input.GetFilename());
    }
}

internal class CreateModInfo<TIn, TReturn> : AdapterFunction<TIn, ModInfo, TReturn>
{
    public CreateModInfo(AdapterContext context, IPipelineConsumer<ModInfo, TReturn> next)
        : base(context, next)
    {
    }


    public required IPipelineConsumer<TIn, string> GetId { get; init; }


    public override async Task<TReturn> ExecuteAsync(TIn input)
    {
        var mod = new ModInfo(await GetId.ExecuteAsync(input));

        return await Next.ExecuteAsync(mod);
    }
}


internal class Test
{
    public async Task Run()
    {
        var context = new AdapterContext();

        IPipelineConsumer<object, IEnumerable<ModInfo>> getMods =
            new GetFilesInDirectory<IEnumerable<ModInfo>>(
                context,
                new MapAdapter<IFile, ModInfo, IEnumerable<ModInfo>>(
                    context,
                    new PipelineEnd<IEnumerable<ModInfo>>()
                )
                {
                    Mapper = new CreateModInfo<IFile, ModInfo>(
                        context,
                        new PipelineEnd<ModInfo>()
                    )
                    {
                        GetId = new GetFilename<string>(
                            context,
                            new PipelineEnd<string>()
                        )
                    }
                }
            );

        await getMods.ExecuteAsync(new { });
    }
}

// TODO: check if this can be (de)serialized,
//       figure out how to make a configurator that can build this at runtime...
//       OR make the configurator create JSON that then gets deserialized to this...?
