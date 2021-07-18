using MoviesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public interface IMoviesSerivce
    {
        Task<Response<IEnumerable<Movie>>> GetMovies(string title = null, int yearOfrelease = 0, string genres = null);
        Task<Response<IEnumerable<Movie>>> GetHighRatedMovies(int countOfMoviesToReturn);
        Task<Response<IEnumerable<Movie>>> GetHighRatedMoviesByUser(int userId, int countOfMoviesToReturn);
        Task<Response<int>> AddorUpdateMovieRating(MovieRating movieRating);
    }
}
