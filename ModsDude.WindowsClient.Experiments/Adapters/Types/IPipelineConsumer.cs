namespace ModsDude.WindowsClient.Experiments.Adapters.Types;

internal interface IPipelineConsumer<in TIn, TReturn>
{
    Task<TReturn> ExecuteAsync(TIn input);
}
