using Moq;
using MoviesAPI.Repositories;
using MoviesAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MoviesAPI.UnitTests.Services
{
    public class RatingHelperTests
    {
        private readonly RatingHelper ratingHelper;
        private readonly Mock<IMoviesRepository> _moviesRepository;
        private readonly Mock<IMovieRatingsRepository> _movieRatingsRepository;
        private readonly IEnumerable<Movie> movies;
        private readonly IEnumerable<MovieRating> movieRatings;
        public RatingHelperTests()
        {
            movies = new List<Movie>
            {
                new Movie {Id = 1, Title = "Movie 1", Genres = "Action", RunningTime = 120, YearOfRelease = 2016},
                new Movie {Id = 2, Title = "Movie 2", Genres = "Fiction", RunningTime = 135, YearOfRelease = 2017},
                new Movie {Id = 3, Title = "Movie 3", Genres = "Comedy", RunningTime = 145, YearOfRelease = 2017}
            };

            movieRatings = new List<MovieRating>
            {
                new MovieRating { Id = 1, MovieId = 1, UserId =1, Rating = 3.5},
                new MovieRating { Id = 2, MovieId = 1, UserId =2, Rating = 3.25},
                new MovieRating { Id = 3, MovieId = 1, UserId =3, Rating = 4.5},
                new MovieRating { Id = 4, MovieId = 2, UserId =1, Rating = 3.0},
                new MovieRating { Id = 5, MovieId = 2, UserId =2, Rating = 3.25},
                new MovieRating { Id = 6, MovieId = 2, UserId =3, Rating = 4.56}
            };

            _moviesRepository = new Mock<IMoviesRepository>();
            _movieRatingsRepository = new Mock<IMovieRatingsRepository>();
            ratingHelper = new RatingHelper(_moviesRepository.Object, _movieRatingsRepository.Object);
        }

        #region CalculateAverageRatingTests
        [Fact]
        public async void CalculateAverageRating_should_behave_as_expected()
        {
            _moviesRepository.Setup(m => m.GetMovies()).ReturnsAsync(movies.AsQueryable());
            _movieRatingsRepository.Setup(mr => mr.GetMovieRatings()).ReturnsAsync(movieRatings.AsQueryable());
            _moviesRepository.Setup(m => m.UpdateMovie(It.IsAny<Movie>()));
            await ratingHelper.CalculateAverageRating();
            _moviesRepository.Verify(m => m.UpdateMovie(It.Is<Movie>(mv => mv.AverageRating == 3.75 && mv.Id == 1)), Times.Once);
            _moviesRepository.Verify(m => m.UpdateMovie(It.Is<Movie>(mv => Math.Round(mv.AverageRating,1) == 3.6 && mv.Id == 2)), Times.Once);

        }
        #endregion

        #region UpdateAverageRatingTests
        [Fact]
        public async void UpdateAverageRating_should_behave_as_expected()
        {
            _moviesRepository.Setup(m => m.GetMovies()).ReturnsAsync(movies.AsQueryable());
            _movieRatingsRepository.Setup(mr => mr.GetMovieRatings()).ReturnsAsync(movieRatings.AsQueryable());
            _moviesRepository.Setup(m => m.UpdateMovie(It.IsAny<Movie>()));
            await ratingHelper.UpdateAverageRating(1);
            _moviesRepository.Verify(m => m.GetMovies(), Times.Once);
            _moviesRepository.Verify(m => m.UpdateMovie(It.Is<Movie>(mv => mv.AverageRating == 3.75 && mv.Id == 1)), Times.Once);
        }
        #endregion

    }
}
