namespace ModsDude.WindowsClient.Model.Exceptions;
public class GraphqlNullDataException : Exception
{
    public GraphqlNullDataException()
        : base("Data property was null, no errors present")
    {
    }
}
