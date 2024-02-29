using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using Moq.EntityFrameworkCore;
using System.Numerics;
using System.Text;
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
        new() { Title = "By The Light of the Silvery Moon", Genre = "Romcom" },
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
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(null, 11)]
    [InlineData(0, 0)]
    [InlineData(11, 11)]
    [InlineData(12, 11)]
    public async void Ensure_search_limit_can_be_applied(
            int? searchLimit,
            int expectedCount)
    {

        //Arrange
        var mockContext = new Mock<MoviesDbContext>();
        mockContext
            .Setup(x => x.Movies)
            .ReturnsDbSet(_movieList);

        var movieRepository = new MovieRepository(mockContext.Object);

        //Act
        IEnumerable<Movie> movies = await movieRepository.SearchMovies(searchLimit: searchLimit);

        //Assert
        movies.Count().Should().Be(expectedCount);

    }

    public static IEnumerable<object[]> PagingTestParams
    {
        get
        {
            yield return new object[] 
            {   
                new PagingRequirements() { Page = 1 , PageSize = 1}, 
                new List<string>() { "Local Hero"} 
            };
            yield return new object[]
            {
                new PagingRequirements() { Page = 2 , PageSize = 2},
                new List<string>() { "The Terminator", "Toy Story"}
            };
            yield return new object[]
            {
                new PagingRequirements() { Page = 6 , PageSize = 2},
                new List<string>() { "Planes, Trains and Automobiles" }
            };
        }
    }

    [Theory()]
    [MemberData(nameof(PagingTestParams))]
    public async void Ensure_paging_requirements_can_be_applied(
        PagingRequirements pagingRequirements,
        List<string> expectedMovies
        )
    {

        //Arrange
        var mockContext = new Mock<MoviesDbContext>();
        mockContext
            .Setup(x => x.Movies)
            .ReturnsDbSet(_movieList);

        var movieRepository = new MovieRepository(mockContext.Object);

        //Act
        IEnumerable<Movie> movies = await movieRepository.SearchMovies(pagingRequirements: pagingRequirements);

        //Assert
        movies.Select(m => m.Title).Should().BeEquivalentTo(expectedMovies);

    }


}
