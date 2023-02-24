using System.Diagnostics.CodeAnalysis;

namespace LibLab.Interview.Lotr.SDK.Models.Movie;

[ExcludeFromCodeCoverage]
public class MovieServiceException : Exception
{
	public MovieServiceException()
	{
	}

	public MovieServiceException(string message)
		: base(message)
	{
	}

	public MovieServiceException(string message, Exception inner)
		: base(message, inner)
	{
	}
}
