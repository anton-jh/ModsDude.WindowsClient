using Microsoft.EntityFrameworkCore;
using ModsDude.WindowsClient.ApiClient.Generated;
using ModsDude.WindowsClient.Model.DbContexts;
using ModsDude.WindowsClient.Model.Exceptions;
using ModsDude.WindowsClient.Model.Models;

namespace ModsDude.WindowsClient.Model.Services;
public class RepoService(
    IRepoClient repoClient,
    ApplicationDbContext dbContext,
    SessionService sessionService)
{
    public delegate void RepoCreatedEventHandler();
    public event RepoCreatedEventHandler? RepoCreated;


    public async Task<IEnumerable<CombinedRepo>> GetRepos(CancellationToken cancellationToken)
    {
        var repos = await repoClient.GetMyReposAsync(cancellationToken);
        var instances = await dbContext.LocalInstances
            .Where(x => x.UserId == sessionService.UserId)
            .ToListAsync(cancellationToken);

        var combinedRepos = repos.Select(x => new CombinedRepo()
        {
            Id = x.Repo.Id,
            Name = x.Repo.Name,
            LocalInstances = instances.Where(i => i.RepoId == x.Repo.Id).ToList()
        });

        return combinedRepos;
    }

    public async Task CreateRepo(string name, string? modAdapterScript, string? savegameAdapterScript, CancellationToken cancellationToken)
    {
        var request = new CreateRepoRequest()
        {
            Name = name,
            ModAdapterScript = modAdapterScript,
            SavegameAdapterScript = savegameAdapterScript,
        };
        try
        {
            await repoClient.CreateRepoAsync(request, cancellationToken);
        }
        catch (ApiException ex) when (ex.StatusCode == 409)
        {
            throw new UserFriendlyException("Name taken", null, ex);
        }
        OnRepoCreated();
    }


    private void OnRepoCreated()
    {
        RepoCreated?.Invoke();
    }
}
