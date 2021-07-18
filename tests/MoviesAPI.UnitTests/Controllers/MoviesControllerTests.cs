using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MoviesAPI.Controllers;
using MoviesAPI.Services;
using System.Collections.Generic;
using System.Net;
using Xunit;
namespace MoviesAPI.UnitTests.Controllers
{
    public class MoviesControllerTests
    {
        private readonly Mock<IMoviesSerivce> _moviesService;
        private readonly MoviesController moviesController; 
        public MoviesControllerTests()
        {
            _moviesService = new Mock<IMoviesSerivce>();
            moviesController = new MoviesController(_moviesService.Object);
        }

        #region GetTests
        [Fact]
        public async void Get_should_behave_as_expected_in_case_of_bad_request()
        {
            
            var actual = await moviesController.Get(null, 0, null);
            actual.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            
        }

        [Theory]
        [InlineData(null, 1, null, StatusCodes.Status200OK)]
        [InlineData("Test", 1, null, StatusCodes.Status200OK)]
        [InlineData(null, 1, "Action", StatusCodes.Status200OK)]
        public async void Get_should_behave_as_expected(string title, int yearOfRelease, string genres, int statusCode)
        {
            _moviesService.Setup(m => m.GetMovies(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new Response<IEnumerable<Models.Movie>>
            {
                StatusCode = HttpStatusCode.OK,
                Data = new List<Models.Movie>()
            });
            var actual = await moviesController.Get(title, yearOfRelease, genres);
            actual.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(statusCode);

        }
        #endregion

        #region GetHighRatedMovies
        [Fact]
        public async void GetHighRatedMovies_should_behave_as_expected()
        {
            _moviesService.SetupSequence(m => m.GetHighRatedMovies(It.IsAny<int>())).ReturnsAsync(new Response<IEnumerable<Models.Movie>>
            {
                StatusCode = HttpStatusCode.OK,
                Data = new List<Models.Movie>()
            }).ReturnsAsync(new Response<IEnumerable<Models.Movie>>
            {
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessage = "Error Occured"
            });
            var actual = await moviesController.GetHighRatedMovies(5);
            actual.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status200OK);

            actual = await moviesController.GetHighRatedMovies(5);
            actual.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

        }
        #endregion

        #region GetHighRatedMoviesByUser
        [Fact]
        public async void GetHighRatedMoviesByUser_should_behave_as_expected()
        {
            _moviesService.SetupSequence(m => m.GetHighRatedMoviesByUser(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new Response<IEnumerable<Models.Movie>>
            {
                StatusCode = HttpStatusCode.OK,
                Data = new List<Models.Movie>()
            }).ReturnsAsync(new Response<IEnumerable<Models.Movie>>
            {
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessage = "User not found."
            });
            var actual = await moviesController.GetHighRatedMoviesByUser(5, 5);
            actual.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status200OK);

            actual = await moviesController.GetHighRatedMoviesByUser(8, 5);
            actual.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

        }
        #endregion

        #region AddorUpdateMovieRating
        [Fact]
        public async void AddorUpdateMovieRating_should_behave_as_expected()
        {
            _moviesService.SetupSequence(m => m.AddorUpdateMovieRating(It.IsAny<Models.MovieRating>())).ReturnsAsync(new Response<int>
            {
                StatusCode = HttpStatusCode.OK
            }).ReturnsAsync(new Response<int>
            {
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessage = "User not found."
            });

            var actual = await moviesController.AddorUpdateMovieRating(new Models.MovieRating { UserId = 1, MovieId = 1, Rating = 0});
            actual.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

            actual = await moviesController.AddorUpdateMovieRating(new Models.MovieRating { UserId = 1, MovieId = 1, Rating = 1 }); ;
            actual.Should().BeOfType<OkResult>().Which.StatusCode.Should().Be(StatusCodes.Status200OK);

            actual = await moviesController.AddorUpdateMovieRating(new Models.MovieRating { UserId = 1, MovieId = 1, Rating = 2 });
            actual.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

        }
        #endregion
    }
}
