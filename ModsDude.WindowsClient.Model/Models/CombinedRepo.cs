namespace ModsDude.WindowsClient.Model.Models;
public class CombinedRepo
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }
    public List<LocalInstance> LocalInstances { get; init; } = [];
}
