using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Repositories
{
    public interface IMoviesRepository
    {
        Task<IQueryable<Movie>> GetMovies();
        Task UpdateMovie(Movie movie);
    }
}
