using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace MoviesAPI.Repositories
{
    public interface IMoviesContext
    {
        DbSet<Movie> Movies { get; set; }
        DbSet<MovieRating> MovieRatings { get; set; }
        DbSet<User> Users { get; set; }
        DatabaseFacade Database { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
