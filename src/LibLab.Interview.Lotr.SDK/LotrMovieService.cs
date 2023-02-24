using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text.Json;
using LibLab.Interview.Lotr.SDK.Models.Movie;

[assembly: InternalsVisibleTo("LibLab.Interview.Lotr.SDK.Tests")]
namespace LibLab.Interview.Lotr.SDK;

public class LotrMovieService : ILotrMovieService, IDisposable
{
    private static HttpClient? httpClient;

    [ExcludeFromCodeCoverage]
	public LotrMovieService()
	{
		httpClient = new HttpClient
		{
			BaseAddress = new Uri(Constants.ApiAccess.ApiURL),
		};

        if (httpClient is null)
            throw new InvalidOperationException("Could not create a client for the API");

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.ApiAccess.ApiToken);
	}

    internal LotrMovieService(HttpClient client)
    {
        httpClient = client;
    }

    public void Dispose()
    {
        httpClient?.Dispose();
    }

    public async Task<Movie?> GetMovieByIdAsync(string id)
    {
        var json = await GetApiResponseJsonAsync(string.Format(Constants.ApiEndpoints.MovieById, id));
        var result = ParseJsonArray<Movie>(json);

        return result?.FirstOrDefault();
    }

    public async Task<IEnumerable<Quote>?> GetMovieQuotesAsync(string movieId)
    {
        var json = await GetApiResponseJsonAsync(string.Format(Constants.ApiEndpoints.MovieQuotes, movieId));
        var result = ParseJsonArray<Quote>(json);

        return result;
    }

    public async Task<IEnumerable<Movie>?> GetMoviesAsync()
    {
        var json = await GetApiResponseJsonAsync(Constants.ApiEndpoints.Movie);
        var result = ParseJsonArray<Movie>(json);

        return result;
    }

    private async Task<JsonElement> GetApiResponseJsonAsync(string endpoint)
    {
        var response = await httpClient!.GetAsync(endpoint);
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new MovieServiceException($"An error occured while accessing {endpoint} endpoint.", ex);
        }

        var content = await response.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonElement>(content);

        return json;
    }

    private IEnumerable<T>? ParseJsonArray<T>(JsonElement json, string nodePath = "docs")
    {
        var result = new List<T>();
        if (json.TryGetProperty(nodePath, out JsonElement obj) && obj.ValueKind == JsonValueKind.Array)
        {
            result = JsonSerializer.Deserialize<List<T>>(obj.GetRawText());
        }

        return result;
    }
}
