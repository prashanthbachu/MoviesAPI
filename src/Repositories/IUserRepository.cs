using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Repositories
{
    public interface IUserRepository
    {
        Task<IQueryable<User>> GetUsers();
    }
}
