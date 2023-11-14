using ModsDude.WindowsClient.Experiments.Adapters.PipelineFunctions;
using System.Reflection;

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
        var functionType = Type.GetType(ns + "." + first.FunctionName)
            ?? throw new AssemblingException("Cannot find type");

        var baseTIn = functionType.BaseType!.GetGenericArguments().First();

        var unsolvedTypeParams = functionType.GetGenericArguments().ToList();
        var solvedTypeParams = new Type[unsolvedTypeParams.Count];

        // TODO: Exclude any type param related to TReturn.
        // TReturn will be taken from TReturn of Next when solved completely. Possible?

        while (unsolvedTypeParams.Any())
        {
            for (int i = unsolvedTypeParams.Count - 1; i >= 0; i--)
            {
                Type? unsolved = unsolvedTypeParams[i];
                var paramType =
                    FindTypeParamInTIn(unsolved, baseTIn, tIn) ??
                    FindTypeParamInPipelineProperties(unsolved, functionType);

                if (paramType is not null)
                {
                    solvedTypeParams[i] = paramType;
                    unsolvedTypeParams.RemoveAt(i);
                }
            }
        }
        

        return new { };
    }


    private Type? FindTypeParamInPipelineProperties(Type typeParam, Type functionType)
    {
        var props = functionType.GetProperties(
            BindingFlags.Public |
            BindingFlags.Instance |
            BindingFlags.DeclaredOnly);

        foreach (var prop in props)
        {
            var type = prop.PropertyType;

            throw new NotImplementedException();
        }

        return null;
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

        // this will always throw because baseTIn is not a closed type
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

    /// <summary>
    /// Finds generic definition for base type or interface specified by <paramref name="target"/>.
    /// </summary>
    /// <example>
    /// List<string> aligned to IEnumerable<> gives string.
    /// </example>
    /// <param name="type"></param>
    /// <param name="target"></param>
    /// <returns></returns>
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
