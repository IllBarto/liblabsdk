using System.Text.Json.Serialization;

namespace LibLab.Interview.Lotr.SDK.Models.Movie;

public class Movie
{
	[JsonPropertyName("_id")]
	public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("runtimeInMinutes")]
    public int RuntimeInMinutes { get; set; }

    [JsonPropertyName("budgetInMillions")]
    public float BudgetInMillions { get; set; }

    [JsonPropertyName("boxOfficeRevenueInMillions")]
    public float BoxOfficeRevenueInMillions { get; set; }

    [JsonPropertyName("academyAwardNominations")]
    public int AcademyAwardNominations { get; set; }

    [JsonPropertyName("academyAwardWins")]
    public int AcademyAwardWins { get; set; }

    [JsonPropertyName("rottenTomatoesScore")]
    public float RrottenTomatoesScore { get; set; }
}