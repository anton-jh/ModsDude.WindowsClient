using Microsoft.EntityFrameworkCore;
using ModsDude.WindowsClient.ApiClient.Generated;
using ModsDude.WindowsClient.Model.DbContexts;
using ModsDude.WindowsClient.Model.Models;

namespace ModsDude.WindowsClient.Model.Services;
public class RepoService(
    IRepoClient repoClient,
    ApplicationDbContext dbContext,
    Session session)
{
    public async Task<IEnumerable<CombinedRepo>> GetRepos(CancellationToken cancellationToken)
    {
        var repos = await repoClient.GetMyReposAsync(cancellationToken);
        var instances = await dbContext.LocalInstances
            .Where(x => x.UserId == session.UserId)
            .ToListAsync(cancellationToken);

        var combinedRepos = repos.Select(x => new CombinedRepo()
        {
            Id = x.Repo.Id,
            Name = x.Repo.Name,
            LocalInstances = instances.Where(i => i.RepoId == x.Repo.Id).ToList()
        });

        return combinedRepos;
    }
}
