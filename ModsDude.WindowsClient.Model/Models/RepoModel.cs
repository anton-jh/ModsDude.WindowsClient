namespace ModsDude.WindowsClient.Model.Models;
public class RepoModel
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }
    public required string? ModsScript { get; init; }
    public required string? SavegamesScript { get; init; }

    public List<LocalInstance> LocalInstances { get; init; } = [];
}
