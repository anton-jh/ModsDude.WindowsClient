using ModsDude.WindowsClient.Experiments.Adapters.Types;

namespace ModsDude.WindowsClient.Experiments.Adapters.PipelineFunctions;

internal class MapList<TIn, TOut, TReturn> : PipelineFunction<IEnumerable<TIn>, IEnumerable<TOut>, TReturn>
{
    public MapList(AdapterContext context, IPipelineConsumer<IEnumerable<TOut>, TReturn> next)
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
