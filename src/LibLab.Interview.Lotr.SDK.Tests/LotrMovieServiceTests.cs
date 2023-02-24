using System.Net;
using LibLab.Interview.Lotr.SDK.Models.Movie;
using RichardSzalay.MockHttp;

namespace LibLab.Interview.Lotr.SDK.Tests
{
    public class LotrMovieServiceTests
    {
        [Fact]
        public async void GetMovies_WhenCallIsSuccessful_ThenReturnsMovieList()
        {
            // arrange
            var response = @"
            {
	            ""docs"": [
		            {
			            ""_id"": ""123"",
			            ""name"": ""movie name 123"",
			            ""runtimeInMinutes"": 558,
			            ""budgetInMillions"": 281,
			            ""boxOfficeRevenueInMillions"": 2917,
			            ""academyAwardNominations"": 30,
			            ""academyAwardWins"": 17,
			            ""rottenTomatoesScore"": 94
		            },
		            {
			            ""_id"": ""345"",
			            ""name"": ""movie name 345"",
			            ""runtimeInMinutes"": 462,
			            ""budgetInMillions"": 675,
			            ""boxOfficeRevenueInMillions"": 2932,
			            ""academyAwardNominations"": 7,
			            ""academyAwardWins"": 1,
			            ""rottenTomatoesScore"": 66.33333333
		            }]
            }";

            var mockHandler = new MockHttpMessageHandler();
            mockHandler
                .When(Constants.ApiAccess.ApiURL + Constants.ApiEndpoints.Movie)
                .Respond(HttpStatusCode.OK, "application/json", response);

            // act
            using var lotrMovieService = new LotrMovieService(GetMockClient(mockHandler));
            var movies = await lotrMovieService.GetMoviesAsync();

            // assert
            Assert.Equal(2, movies?.Count());
            Assert.NotNull(movies!.FirstOrDefault(m => m.Id.Equals("123")));
            Assert.NotNull(movies!.FirstOrDefault(m => m.Id.Equals("345")));
        }

        [Fact]
        public async void GetMovies_WhenCallIsSuccessfulAndJsonIsAnotherFormat_ThenReturnsEmptyList()
        {
            // arrange

            // current response is under "docs" node
            var response = @"
            {
	            ""movies"": [
		            {
			            ""_id"": ""123"",
			            ""name"": ""movie name 123"",
			            ""runtimeInMinutes"": 558,
			            ""budgetInMillions"": 281,
			            ""boxOfficeRevenueInMillions"": 2917,
			            ""academyAwardNominations"": 30,
			            ""academyAwardWins"": 17,
			            ""rottenTomatoesScore"": 94
		            }]
            }";

            var mockHandler = new MockHttpMessageHandler();
            mockHandler
                .When(Constants.ApiAccess.ApiURL + Constants.ApiEndpoints.Movie)
                .Respond(HttpStatusCode.OK, "application/json", response);

            // act
            var lotrMovieService = new LotrMovieService(GetMockClient(mockHandler));
            var movies = await lotrMovieService.GetMoviesAsync();

            // assert
            Assert.Empty(movies!);
        }

        [Fact]
        public async void GetMovies_WhenCallFailed_ThenCustomErrorIsThrown()
        {
            // arrange
            var mockHandler = new MockHttpMessageHandler();
            mockHandler
                .When(Constants.ApiAccess.ApiURL + Constants.ApiEndpoints.Movie)
                .Respond(HttpStatusCode.Unauthorized, "application/json", string.Empty);

            // act
            var lotrMovieService = new LotrMovieService(GetMockClient(mockHandler));
            var exception = await Assert.ThrowsAsync<MovieServiceException>(() => lotrMovieService.GetMoviesAsync());

            // assert
            Assert.IsType<HttpRequestException>(exception.InnerException);
            Assert.True((exception.InnerException as HttpRequestException)?.StatusCode == HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetMovieById_WhenCallIsSuccessful_ThenReturnsSingleResult()
        {
            // arrange
            var response = @"
            {
	            ""docs"": [
		            {
			            ""_id"": ""123"",
			            ""name"": ""movie name 123"",
			            ""runtimeInMinutes"": 558,
			            ""budgetInMillions"": 281,
			            ""boxOfficeRevenueInMillions"": 2917,
			            ""academyAwardNominations"": 30,
			            ""academyAwardWins"": 17,
			            ""rottenTomatoesScore"": 94
		            }]
            }";

            var movieId = "123";
            var mockHandler = new MockHttpMessageHandler();
            mockHandler
                .When(Constants.ApiAccess.ApiURL + string.Format(Constants.ApiEndpoints.MovieById, movieId))
                .Respond(HttpStatusCode.OK, "application/json", response);

            // act
            var lotrMovieService = new LotrMovieService(GetMockClient(mockHandler));
            var movie = await lotrMovieService.GetMovieByIdAsync(movieId);

            // assert
            Assert.NotNull(movie);
            Assert.True(movie.Id.Equals(movieId));
        }

        [Fact]
        public async Task GetMovieQuotes_WhenCallIsSuccessful_ThenReturnsListOfQuotes()
        {
            // arrange
            var response = @"
            {
	            ""docs"": [
		            {
			            ""_id"": ""123"",
			            ""dialog"": ""movie dialog 123"",
			            ""movie"": ""345"",
			            ""character"": ""567567""
		            },
                    {
			            ""_id"": ""234"",
			            ""dialog"": ""movie dialog 234"",
			            ""movie"": ""345"",
			            ""character"": ""567""
		            }]
            }";

            var movieId = "345";
            var mockHandler = new MockHttpMessageHandler();
            mockHandler
                .When(Constants.ApiAccess.ApiURL + string.Format(Constants.ApiEndpoints.MovieQuotes, movieId))
                .Respond(HttpStatusCode.OK, "application/json", response);

            // act
            var lotrMovieService = new LotrMovieService(GetMockClient(mockHandler));
            var quotes = await lotrMovieService.GetMovieQuotesAsync(movieId);

            // assert
            Assert.NotEmpty(quotes!);
            Assert.NotNull(quotes?.FirstOrDefault(q => q.Id.Equals("123")));
            Assert.NotNull(quotes?.FirstOrDefault(q => q.Id.Equals("234")));
        }

        private HttpClient GetMockClient(MockHttpMessageHandler handler)
        {
            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(Constants.ApiAccess.ApiURL);

            return client;
        }
    }
}