using FluentAssertions;
using Moq;
using Xunit;

namespace Optix.Movies;

public class MovieServiceTests
{

	private readonly List<Movie> _movieList =
    [
        new() { Title = "Local Hero" },
        new() { Title = "The Wickerman" },
        new() { Title = "The Terminator" },
        new() { Title = "Toy Story" },
        new() { Title = "Back To The Future" },
        new() { Title = "Jurassic Park" },
        new() { Title = "By the Light of the Silvery Moon" },
        new() { Title = "The Italian Job" },
        new() { Title = "School For Scoundrels" },
        new() { Title = "Make Mine Minx" },
        new() { Title = "Planes, Trains, and Automobiles" }
    ];

    [Theory]
	[InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 3)]
    [InlineData(4, 4)]
    [InlineData(5, 5)]
    [InlineData(10, 10)]
    [InlineData(15, 11)]
    [InlineData(20, 11)]
    public async void Ensure_maximum_no_of_rows_can_be_applied_to_search_results(
        int searchLimit, 
        int expectedResult)
    {

		//Arrange
        var movieRepository = new Mock<IMovieRepository>();
		movieRepository
			.Setup(m => m.SearchMovies("", ""))
        .Returns(
                (Task.FromResult<IEnumerable<Movie>>(_movieList)));

		var movieSevice = new MovieService(movieRepository.Object);

		//Act
		IEnumerable<Movie> movies = await movieSevice.GetMovies(limit: searchLimit);


		//Assert
		movies.Count().Should().Be(expectedResult);

    }

    public static IEnumerable<object[]> PagingTestParams
    {
        get
        {
            yield return new object[]
            {
                1,
                2,
                new List<Movie>
                {
                        new() { Title = "Local Hero" },
                        new() { Title = "The Wickerman" }
                }
            };
            yield return new object[]
            {
                3,
                3,
                new List<Movie>
                {
                    new() { Title = "By the Light of the Silvery Moon" },
                    new() { Title = "The Italian Job" },
                    new() { Title = "School For Scoundrels" }
                }
            };
            yield return new object[]
            {
                4,
                3,
                new List<Movie>
                {
                    new() { Title = "Make Mine Minx" },
                    new() { Title = "Planes, Trains, and Automobiles" },
                }
            };
        }
    }

    [Theory]
    [MemberData(nameof(PagingTestParams))]
    public async void Ensure_paging_can_be_applied_to_search_results(
        int? pageNumber, int? pageSize, List<Movie> expectedResult )
    {

        //Arrange
        var movieRepository = new Mock<IMovieRepository>();
        movieRepository
            .Setup(m => m.SearchMovies("", ""))
        .Returns(
                (Task.FromResult<IEnumerable<Movie>>(_movieList)));

        var movieSevice = new MovieService(movieRepository.Object);

        //Act
        IEnumerable<Movie> movies = await movieSevice.GetMovies(page: pageNumber, pageSize: pageSize);

        //Assert
        movies.Should().BeEquivalentTo(expectedResult);

    }

    [Fact()]
    public async void When_no_page_size_defined_a_default_of_10_is_used()
    {

        //Arrange
        var movieRepository = new Mock<IMovieRepository>();
        movieRepository
            .Setup(m => m.SearchMovies("", ""))
        .Returns(
                (Task.FromResult<IEnumerable<Movie>>(_movieList)));

        var movieSevice = new MovieService(movieRepository.Object);

        //Act
        IEnumerable<Movie> movies = await movieSevice.GetMovies(page: 1);

        //Assert
        movies.Count().Should().Be(10);
    }
}
