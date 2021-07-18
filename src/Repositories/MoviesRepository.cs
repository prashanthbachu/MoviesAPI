using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly IMoviesContext _moviesContext;
        public MoviesRepository(IMoviesContext moviesContext)
        {
            _moviesContext = moviesContext;
        }
        public Task<IQueryable<Movie>> GetMovies()
        {
            return Task.Run(() => _moviesContext.Movies.AsQueryable());
        }

        public Task UpdateMovie(Movie movie)
        {
            _moviesContext.Movies.Update(movie);
            return _moviesContext.SaveChangesAsync();
        }
    }
}
