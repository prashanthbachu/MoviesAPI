using MoviesAPI.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public class RatingHelper : IRatingHelper
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly IMovieRatingsRepository _movieRatingsRepository;

        public RatingHelper(IMoviesRepository moviesRepository, IMovieRatingsRepository movieRatingsRepository)
        {
            _moviesRepository = moviesRepository;
            _movieRatingsRepository = movieRatingsRepository;

        }
        public async Task CalculateAverageRating()
        {
            var movies = await _moviesRepository.GetMovies();
            movies.ToList().ForEach( movie =>
            {
                 CalculateAvgRatingForMovie(movie).Wait();
            });
        }

        public async Task UpdateAverageRating(int movieId)
        {
            var movies = await _moviesRepository.GetMovies();
            var movie = movies.Where(m=>m.Id == movieId).FirstOrDefault();
            await CalculateAvgRatingForMovie(movie);
        }

        #region private methods
        private async Task CalculateAvgRatingForMovie(Movie movie)
        {
            var movieRatings = await _movieRatingsRepository.GetMovieRatings();
            var ratingsForMovie = movieRatings.Where(r => r.MovieId == movie.Id).Select(r => r.Rating).ToList();
            var averageRating = ratingsForMovie.Sum() / ratingsForMovie.Count;
            if (!double.IsNaN(averageRating)) movie.AverageRating = averageRating;
            await _moviesRepository.UpdateMovie(movie);
        }
        #endregion
    }
}
