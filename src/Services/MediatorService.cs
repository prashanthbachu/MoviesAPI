
namespace MoviesAPI.Services
{
    public class MediatorService : IMediator
    {
        private readonly IRatingHelper _ratingHelper;
        public MediatorService(IRatingHelper ratingHelper)
        { 
            _ratingHelper = ratingHelper;
        }
        public void Nofity(int movieId)
        {
            _ratingHelper.UpdateAverageRating(movieId);
        }
    }
}
