using AutoMapper;

namespace MoviesAPI.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Models.Movie, Repositories.Movie>().ReverseMap();
        }
    }
}
