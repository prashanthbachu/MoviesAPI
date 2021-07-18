using AutoMapper;

namespace MoviesAPI.Profiles
{
    public class UserProfile :Profile
    {
        public UserProfile()
        {
            CreateMap<Models.User, Repositories.User>().ReverseMap();
        }
    }
}
