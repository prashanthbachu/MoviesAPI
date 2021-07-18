using AutoMapper;
using Moq;
using MoviesAPI.Repositories;
using MoviesAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace MoviesAPI.UnitTests.Services
{
    public class MoviesServiceTests
    {

        private readonly MoviesService moviesService;
        private readonly Mock<IMoviesRepository> _moviesRepository;
        private readonly Mock<IMovieRatingsRepository> _movieRatingsRepository;
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IUserRepository> _userRepository;
        private IEnumerable<Movie> movies;
        private IEnumerable<MovieRating> movieRatings;
        private IEnumerable<User> users;

        public MoviesServiceTests()
        {
            _moviesRepository = new Mock<IMoviesRepository>();
            _movieRatingsRepository = new Mock<IMovieRatingsRepository>();
            _mediator = new Mock<IMediator>();
            _userRepository = new Mock<IUserRepository>();
            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<Models.Movie, Movie>().ReverseMap();
                opts.CreateMap<Models.MovieRating, MovieRating>().ReverseMap();
                opts.CreateMap<Models.User, User>().ReverseMap();
            });
            var mapper = config.CreateMapper();
            moviesService = new MoviesService(_mediator.Object, _moviesRepository.Object, _movieRatingsRepository.Object, _userRepository.Object, mapper);
            SetUpMockMovies();
            SetupMockMovieRatings();
            SetupMockUsers();
        }
        #region AddorUpdateMovieRatingTests
        [Theory]
        [InlineData(11, 4)]
        [InlineData(1, 4)]
        public async void AddorUpdateMovieRating_should_return_if_either_movie_or_user_not_found(int movieId, int userId)
        {
            var actual = await moviesService.AddorUpdateMovieRating(new Models.MovieRating { MovieId = movieId, Rating = 3.5, UserId = userId });
            Assert.Equal(HttpStatusCode.NotFound, actual.StatusCode);
        }

        [Theory]
        [InlineData(1,1)]
        [InlineData(10, 0)]
        public async void AddorUpdateMovieRating_should_work_as_expected_for_a_valid_request(int movieId, int expectedRatingId)
        {
            _mediator.Setup(m => m.Nofity(It.IsAny<int>()));
            _movieRatingsRepository.Setup(mr => mr.AddorUpdateMovieRating(It.IsAny<MovieRating>()));
            var actual = await moviesService.AddorUpdateMovieRating(new Models.MovieRating { MovieId = movieId, Rating = 3.5, UserId = 1 });
            Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
            _mediator.Verify(m => m.Nofity(It.Is<int>( i => i == movieId)), Times.Once);
            _movieRatingsRepository.Verify(mr => mr.AddorUpdateMovieRating(It.Is<MovieRating>(mr => mr.Id == expectedRatingId)), Times.Once);

        }
        #endregion

        #region GetHighRatedMoviesTests
        [Fact]
        public async void GetHighRatedMovies_should_work_as_expected()
        {
            var actual = await moviesService.GetHighRatedMovies(5);
            var data = actual.Data.ToArray();
            Assert.Equal(5, actual.Data.Count());
            Assert.Equal(4.0, data[0].AverageRating);
            Assert.Equal(3.5, data[1].AverageRating);
            Assert.Equal(3.5, data[2].AverageRating);
            Assert.Equal(3.0, data[3].AverageRating);
            Assert.Equal(3.0, data[4].AverageRating);
            Assert.Equal("ABC Movie", data[1].Title);
        }
        #endregion


        #region GetHighRatedMoviesByUserTests
        [Fact]
        public async void GetHighRatedMoviesByUser_should_validate_user()
        {
            var actual = await moviesService.GetHighRatedMoviesByUser(10, 5);
            Assert.Equal(HttpStatusCode.NotFound, actual.StatusCode);
        }

        [Fact]
        public async void GetHighRatedMoviesByUser_should_work_as_expected()
        {
            var actual = await moviesService.GetHighRatedMoviesByUser(1, 5);
            var data = actual.Data.ToArray();
            Assert.Equal(5, actual.Data.Count());
            Assert.Equal(4.0, data[0].AverageRating);
            Assert.Equal(3.5, data[1].AverageRating);
            Assert.Equal(3.5, data[2].AverageRating);
            Assert.Equal(3.0, data[3].AverageRating);
            Assert.Equal(3.0, data[4].AverageRating);
            Assert.Equal("ABC Movie", data[1].Title);
        }
        #endregion

        #region GetMoviesTests
        [Theory]
        [InlineData("1", 0, "", new int[] {1, 10 })]
        [InlineData("Movie 10", 0, "", new int[] { 10 })]
        [InlineData("", 2017, "", new int[] { 4, 8 })]
        [InlineData("4", 2017, "", new int[] { 4})]
        [InlineData("", 0, "Comedy", new int[] { 3, 7 })]
        [InlineData("3", 0, "Comedy", new int[] { 3 })]
        [InlineData("1", 2016, "Action", new int[] { 1 })]
        [InlineData("", 2016, "Fiction", new int[] { 2, 6, 10 })]
        public async void GetMovies_should_work_as_expected(string title, int yearOfrelease, string geners, int[] expectedMovieIds)
        {
            var actual = await moviesService.GetMovies(title, yearOfrelease, geners);
            Assert.Equal(expectedMovieIds, actual.Data.Select(m=>m.Id).ToArray());
        }
        #endregion

        #region private methods
        private void SetUpMockMovies() 
        {
            movies = new List<Movie>
            {
                new Movie {Id = 1, Title = "Movie 1", Genres = "Action", RunningTime = 120, YearOfRelease = 2016, AverageRating = 2.91},
                new Movie {Id = 2, Title = "Movie 2", Genres = "Fiction", RunningTime = 135, YearOfRelease = 2016, AverageRating = 3.24},
                new Movie {Id = 3, Title = "Movie 3", Genres = "Comedy", RunningTime = 145, YearOfRelease = 2016, AverageRating = 3.25},
                new Movie {Id = 4, Title = "Movie 4", Genres = "Horror", RunningTime = 175, YearOfRelease = 2017, AverageRating = 3.75},
                new Movie {Id = 5, Title = "ABC Movie", Genres = "Action", RunningTime = 120, YearOfRelease = 2016, AverageRating = 3.25},
                new Movie {Id = 6, Title = "Movie 6", Genres = "Fiction", RunningTime = 135, YearOfRelease = 2016},
                new Movie {Id = 7, Title = "Movie 7", Genres = "Action,Comedy", RunningTime = 145, YearOfRelease = 2016},
                new Movie {Id = 8, Title = "Movie 8", Genres = "Action", RunningTime = 175, YearOfRelease = 2017},
                new Movie {Id = 9, Title = "Movie 9", Genres = "Action", RunningTime = 120, YearOfRelease = 2016},
                new Movie {Id = 10, Title = "Movie 10", Genres = "Fiction", RunningTime = 135, YearOfRelease = 2016},
            };
            _moviesRepository.Setup(m => m.GetMovies()).ReturnsAsync(movies.AsQueryable());
        }

        private void SetupMockMovieRatings()
        {
            movieRatings = new List<MovieRating>
            {
                new MovieRating { Id = 1, MovieId = 1, UserId =1, Rating = 2.91},
                new MovieRating { Id = 1, MovieId = 2, UserId =1, Rating = 3.24},
                new MovieRating { Id = 1, MovieId = 3, UserId =1, Rating = 3.25},
                new MovieRating { Id = 1, MovieId = 4, UserId =1, Rating = 3.75},
                new MovieRating { Id = 1, MovieId = 5, UserId =1, Rating = 3.25},
                new MovieRating { Id = 2, MovieId = 1, UserId =2, Rating = 3.25},
                new MovieRating { Id = 3, MovieId = 1, UserId =3, Rating = 4.5},
                new MovieRating { Id = 5, MovieId = 2, UserId =2, Rating = 3.25},
                new MovieRating { Id = 6, MovieId = 2, UserId =3, Rating = 4.56}
            };
            _movieRatingsRepository.Setup(mr => mr.GetMovieRatings()).ReturnsAsync(movieRatings.AsQueryable());
        }

        private void SetupMockUsers()
        {
            users = new List<User>
            {
                new User { Id = 1, FirstName = "FN1", LastName = "LN1"},
                new User { Id = 2, FirstName = "FN2", LastName = "LN2"},
                new User { Id = 3, FirstName = "FN3", LastName = "LN3"}
            };
            _userRepository.Setup(u => u.GetUsers()).ReturnsAsync(users.AsQueryable());
        }

        #endregion

    }
}
