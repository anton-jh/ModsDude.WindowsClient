using ModsDude.WindowsClient.Experiments.Adapters.FileSystem;
using ModsDude.WindowsClient.Experiments.Adapters.Types;

namespace ModsDude.WindowsClient.Experiments.Adapters.PipelineFunctions;

internal class GetFilesInDirectory<TReturn> : PipelineFunction<object, IEnumerable<FileAbstraction>, TReturn>
{
    public GetFilesInDirectory(AdapterContext context, IPipelineConsumer<IEnumerable<FileAbstraction>, TReturn> next)
        : base(context, next)
    {
    }


    public override async Task<TReturn> ExecuteAsync(object input)
    {
        var files = Enumerable.Empty<FileAbstraction>(); // Get files in folder specified in Context

        return await Next.ExecuteAsync(files);
    }
}
