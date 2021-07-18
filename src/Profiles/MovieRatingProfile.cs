using AutoMapper;

namespace MoviesAPI.Profiles
{
    public class MovieRatingProfile : Profile
    {
        public MovieRatingProfile()
        {
            CreateMap<Models.MovieRating, Repositories.MovieRating>().ReverseMap();
        }
    }
}
