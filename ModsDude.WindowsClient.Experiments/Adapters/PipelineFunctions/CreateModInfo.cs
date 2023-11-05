using ModsDude.WindowsClient.Experiments.Adapters.Types;

namespace ModsDude.WindowsClient.Experiments.Adapters.PipelineFunctions;

internal class CreateModInfo<TIn, TReturn> : PipelineFunction<TIn, ModInfo, TReturn>
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
