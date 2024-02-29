using Moq;
using Xunit;

namespace Optix.Movies;

public class MovieServiceTests
{
    [Fact]
    public async void When_no_pagesize_provided_page_size_should_be_ten()
    {

        //Arrange
        var movieRepository = new Mock<IMovieRepository>();
        var movieSevice = new MovieService(movieRepository.Object);
        var pagingRequirement = new PagingRequirements()
        {
            Page = 3
        };

        //Act
        IEnumerable<Movie> movies = await movieSevice.GetMovies(page: 3);



        //Assert
        movieRepository.Verify(m => 
                m.SearchMovies("", "", null, It.Is<PagingRequirements>( m => m.PageSize == 10 )));
    }

}
