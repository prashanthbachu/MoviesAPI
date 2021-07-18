using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Repositories
{
    public interface IMovieRatingsRepository
    {
        Task<IQueryable<MovieRating>> GetMovieRatings();
        Task<int> AddorUpdateMovieRating(MovieRating movieRating);
    }
}
