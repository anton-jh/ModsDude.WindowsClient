namespace ModsDude.WindowsClient.Experiments.Adapters.Types;

internal class PipelineEnd<TReturn> : IPipelineConsumer<TReturn, TReturn>
{
    public Task<TReturn> ExecuteAsync(TReturn input)
    {
        return Task.FromResult(input);
    }
}
