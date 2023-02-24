using System.Diagnostics.CodeAnalysis;
using LibLab.Interview.Lotr.SDK;
using Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the <see cref="ILotrMovieService"/> to the DI container.
    /// </summary>
    /// <param name="apiToken">Optional API access token. If not provided, an internal token will be used.</param>
    /// <returns></returns>
    public static IServiceCollection AddLotrMovieService(this IServiceCollection services, string apiToken = "")
    {
        services.AddScoped<ILotrMovieService, LotrMovieService>();
        
        if (!string.IsNullOrWhiteSpace(apiToken))
        {
            Constants.ApiAccess.ApiToken = apiToken;
        }

        return services;
    }
}
