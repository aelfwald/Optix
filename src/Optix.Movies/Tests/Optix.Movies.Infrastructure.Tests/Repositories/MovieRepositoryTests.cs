using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Optix.Movies;

public class MovieRepositoryTests
{

	private readonly List<Movie> _movieList =
    [
        new() { Title = "Local Hero", Genre = "Drama" },
        new() { Title = "The Wickerman", Genre = "Horror" },
        new() { Title = "The Terminator", Genre = "Thriller" },
        new() { Title = "Toy Story", Genre = "Cartoon" },
        new() { Title = "Back To The Future", Genre = "Comedy" },
        new() { Title = "Jurassic Park", Genre = "Science Fiction" },
        new() { Title = "By the Light of the Silvery Moon", Genre = "Romcom" },
        new() { Title = "The Italian Job", Genre = "Caper" },
        new() { Title = "School For Scoundrels", Genre = "Comedy" },
        new() { Title = "Make Mine Minx", Genre = "Comedy" },
        new() { Title = "Planes, Trains and Automobiles", Genre = "Comedy"  }
    ];

    [Theory]
	[InlineData("Local", 1)]
    [InlineData("PLANES", 1)]
    [InlineData("THE", 5)]
    [InlineData(" Job", 1)]
    [InlineData("", 11)]
    public async void Ensure_can_search_by_title(
        string title, 
        int expectedCount)
    {

        //Arrange
        var mockContext = new Mock<MoviesDbContext>();
        mockContext
            .Setup(x => x.Movies)
            .ReturnsDbSet(_movieList);

        var movieRepository = new MovieRepository(mockContext.Object);

		//Act
		IEnumerable<Movie> movies = await movieRepository.SearchMovies( title: title, genre: "");

		//Assert
		movies.Count().Should().Be(expectedCount);

    }

    [Theory]
    [InlineData("DRAMA", 1)]
    [InlineData("drama", 1)]
    [InlineData("", 11)]
    [InlineData("SCIENCE ", 1)]
    [InlineData(" FICTION", 1)]
    [InlineData("Action", 0)]
    public async void Ensure_can_search_by_genre(
            string genre,
            int expectedCount)
    {

        //Arrange
        var mockContext = new Mock<MoviesDbContext>();
        mockContext
            .Setup(x => x.Movies)
            .ReturnsDbSet(_movieList);

        var movieRepository = new MovieRepository(mockContext.Object);

        //Act
        IEnumerable<Movie> movies = await movieRepository.SearchMovies(title: "", genre: genre);

        //Assert
        movies.Count().Should().Be(expectedCount);

    }

    [Theory]
    [InlineData("Planes", "Comedy", 1)]
    [InlineData("", "", 11)]
    [InlineData("PLANES, ", "COMEDY", 1)]
    [InlineData("ne", "Comedy", 2)]
    public async void Ensure_can_search_by_title_and_genre(
            string title,
            string genre,
            int expectedCount)
    {

        //Arrange
        var mockContext = new Mock<MoviesDbContext>();
        mockContext
            .Setup(x => x.Movies)
            .ReturnsDbSet(_movieList);

        var movieRepository = new MovieRepository(mockContext.Object);

        //Act
        IEnumerable<Movie> movies = await movieRepository.SearchMovies(title: title, genre: genre);

        //Assert
        movies.Count().Should().Be(expectedCount);

    }

}
