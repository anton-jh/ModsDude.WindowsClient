namespace ModsDude.WindowsClient.Experiments.Adapters.Persistence;

internal record PipelineFunctionDescriptor(string FunctionName, Dictionary<string, IEnumerable<PipelineFunctionDescriptor>> Parameters);
