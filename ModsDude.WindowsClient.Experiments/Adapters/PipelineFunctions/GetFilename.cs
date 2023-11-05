using ModsDude.WindowsClient.Experiments.Adapters.FileSystem;
using ModsDude.WindowsClient.Experiments.Adapters.Types;

namespace ModsDude.WindowsClient.Experiments.Adapters.PipelineFunctions;

internal class GetFilename<TReturn> : PipelineFunction<FileAbstraction, string, TReturn>
{
    public GetFilename(AdapterContext context, IPipelineConsumer<string, TReturn> next)
        : base(context, next)
    {
    }


    public override async Task<TReturn> ExecuteAsync(FileAbstraction input)
    {
        return await Next.ExecuteAsync(input.GetFilename());
    }
}
