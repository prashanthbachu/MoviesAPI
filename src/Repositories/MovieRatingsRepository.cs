using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Repositories
{
    public class MovieRatingsRepository : IMovieRatingsRepository
    {
        private readonly IMoviesContext _moviesContext;
        public MovieRatingsRepository(IMoviesContext moviesContext)
        {
            _moviesContext = moviesContext;
        }

        public Task<int> AddorUpdateMovieRating(MovieRating movieRating)
        {
            _moviesContext.MovieRatings.Update(movieRating);
            return _moviesContext.SaveChangesAsync();
        }

        public Task<IQueryable<MovieRating>> GetMovieRatings()
        {
            return Task.Run(() => _moviesContext.MovieRatings.AsQueryable());
        }
    }
}
