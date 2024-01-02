namespace ModsDude.WindowsClient.Application.Adapters.Exceptions;
public class InvalidAdapterException(string devMessage)
    : Exception(devMessage)
{
    public static InvalidAdapterException MissingGlobal(string name)
        => new($"Missing global '{name}'");

    public static InvalidAdapterException ReturnType(string globalName)
        => new($"Incorrect return type from '{globalName}'");
}
