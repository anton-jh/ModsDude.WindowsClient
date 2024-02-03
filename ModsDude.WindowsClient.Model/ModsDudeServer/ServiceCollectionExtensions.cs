using Microsoft.Extensions.DependencyInjection;
using ModsDude.WindowsClient.ApiClient.Generated;

namespace ModsDude.WindowsClient.ApiClient;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddModsDudeClient(this IServiceCollection services)
    {
        services.AddHttpClient<IRepoClient, RepoClient>();

        return services;
    }
}
