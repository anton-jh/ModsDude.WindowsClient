namespace ModsDude.WindowsClient.Experiments.Adapters.Types;

internal abstract class PipelineFunction<TIn, TOut, TReturn> : IPipelineConsumer<TIn, TReturn>
{
    public PipelineFunction(AdapterContext context, IPipelineConsumer<TOut, TReturn> next)
    {
        Context = context;
        Next = next;
    }


    protected AdapterContext Context { get; }
    public IPipelineConsumer<TOut, TReturn> Next { get; }


    public abstract Task<TReturn> ExecuteAsync(TIn input);
}
