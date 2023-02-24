using LibLab.Interview.Lotr.SDK.Models.Movie;

namespace LibLab.Interview.Lotr.SDK;

public interface ILotrMovieService
{
    /// <summary>
    /// Gets all LOTR movies.
    /// </summary>
    /// <returns>Collection of <see cref="Movie"/>.</returns>
    Task<IEnumerable<Movie>?> GetMoviesAsync();

    /// <summary>
    /// Return a movie by its Id.
    /// </summary>
    /// <param name="id">Movie identifier.</param>
    /// <returns>Instance of <see cref="Movie"/>.</returns>
    Task<Movie?> GetMovieByIdAsync(string id);

    /// <summary>
    /// Gets all <see cref="Quote"/>s for a specific movie.
    /// </summary>
    /// <param name="movieId">Movie identifier.</param>
    /// <returns>Collection of <see cref="Quote"/>.</returns>
    Task<IEnumerable<Quote>?> GetMovieQuotesAsync(string movieId);
}