using ModsDude.WindowsClient.Experiments.Adapters.FileSystem;
using ModsDude.WindowsClient.Experiments.Adapters.PipelineFunctions;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace ModsDude.WindowsClient.Experiments.Adapters.Persistence;

internal class PipelineAssembler
{
    public object Assemble(Type tIn, IEnumerable<PipelineFunctionDescriptor> descriptors)
    {
        var first = descriptors.FirstOrDefault();
        var rest = descriptors.Skip(1);

        if (first is null)
        {
            throw new NotImplementedException();
        }

        var ns = typeof(CreateModInfo<,>).Namespace;
        var type = Type.GetType(ns + "." + first.FunctionName)
            ?? throw new AssemblingException("Cannot find type");

        foreach (var typeParam in type.GetGenericArguments())
        {
            // check TIn
            var baseTIn = type.BaseType!.GetGenericArguments().First();

            
        }

        var type2 = typeof(MapList<,,>);

        // this seems to work!
        var fromTIn = FindTypeParamInTIn(type2.GetGenericArguments()[0], type2.BaseType!.GetGenericArguments()[0], typeof(List<FileAbstraction>));
        // TODO: if the previous fails, go looking in all pipeline properties' TReturns

        return new { };
    }


    private Type? FindTypeParamInTIn(Type typeParam, Type baseTIn, Type actualTIn)
    {
        if (baseTIn == typeParam)
        {
            return actualTIn;
        }

        if (baseTIn.ContainsGenericParameters == false)
        {
            return null;
        }

        // this will allways throw because baseTIn is not a closed type
        //if (actualTIn.IsAssignableTo(baseTIn) == false)
        //{
        //    throw new AssemblingException("Invalid input type");
        //}

        foreach (var innerTypeParam in baseTIn.GetGenericArguments())
        {
            if (innerTypeParam == typeParam)
            {
                var alignedActualTIn = AlignWithTypeDefinition(actualTIn, baseTIn);
                if (alignedActualTIn is null)
                {
                    // could not find typeParam in baseTIn
                    return null;
                }

                return alignedActualTIn.GetGenericArguments()[innerTypeParam.GenericParameterPosition];
            }
        }

        throw new NotImplementedException();
    }

    private Type? AlignWithTypeDefinition(Type type, Type target)
    {
        if (type.IsGenericType && (type.GetGenericTypeDefinition() == target.GetGenericTypeDefinition()))
        {
            return type;
        }

        var interfaces = type.GetInterfaces();

        foreach (var iface in interfaces)
        {
            var alignedWithInterface = AlignWithTypeDefinition(iface, target);
            if (alignedWithInterface is not null)
            {
                return alignedWithInterface;
            }
        }

        if (type.BaseType is null)
        {
            return null;
        }

        return AlignWithTypeDefinition(type.BaseType, target);
    }
}


// https://learn.microsoft.com/en-us/dotnet/api/system.type.getgenericarguments?view=net-7.0
