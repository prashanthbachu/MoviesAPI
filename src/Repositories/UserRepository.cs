using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMoviesContext _moviesContext;
        public UserRepository(IMoviesContext moviesContext)
        {
            _moviesContext = moviesContext;
        }
        public Task<IQueryable<User>> GetUsers()
        {
            return Task.Run(() => _moviesContext.Users.AsQueryable());
        }
    }
}
