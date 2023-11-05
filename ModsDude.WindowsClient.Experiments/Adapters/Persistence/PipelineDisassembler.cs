using ModsDude.WindowsClient.Experiments.Adapters.Types;
using System.Reflection;

namespace ModsDude.WindowsClient.Experiments.Adapters.Persistence;
internal class PipelineDisassembler
{
    public IEnumerable<PipelineFunctionDescriptor> Disassemble(object? first)
    {
        var next = first;

        while (next is not null)
        {
            var type = next.GetType();

            if (type.GetGenericTypeDefinition() == typeof(PipelineEnd<>))
            {
                yield break;
            }

            yield return DescribeFunction(next);

            next = type.GetProperty("Next")?.GetValue(next);
        }
    }


    private PipelineFunctionDescriptor DescribeFunction(object function)
    {
        var type = function.GetType();

        var functionName = type.Name;
        var parameters = type
            .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Where(AcceptsPipelineFunction)
            .ToDictionary(x => x.Name, x => Disassemble(x.GetValue(function)));

        return new PipelineFunctionDescriptor(functionName, parameters);
    }

    private bool AcceptsPipelineFunction(PropertyInfo prop)
    {
        return prop.PropertyType.GetGenericTypeDefinition() == typeof(IPipelineConsumer<,>);
    }
}
