
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public interface IRatingHelper
    {
        Task UpdateAverageRating(int movieId);
        Task CalculateAverageRating();
    }
}
