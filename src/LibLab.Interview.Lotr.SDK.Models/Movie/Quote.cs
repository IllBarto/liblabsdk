using System.Text.Json.Serialization;

namespace LibLab.Interview.Lotr.SDK.Models.Movie;

public class Quote
{
    [JsonPropertyName("_id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("dialog")]
    public string Dialog { get; set; } = string.Empty;

    [JsonPropertyName("movie")]
    public string MovieId { get; set; } = string.Empty;

    [JsonPropertyName("character")]
    public string CharachterId { get; set; } = string.Empty;
}