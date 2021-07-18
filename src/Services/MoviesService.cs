using AutoMapper;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using m = MoviesAPI.Models;
using r = MoviesAPI.Repositories;

namespace MoviesAPI.Services
{
    public class MoviesService : IMoviesSerivce
    {
        private readonly IMediator _mediator;
        private readonly r.IMoviesRepository _moviesRepository;
        private readonly r.IMovieRatingsRepository _movieRatingsRepository;
        private readonly r.IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public MoviesService(IMediator mediator,
                              r.IMoviesRepository moviesRepository,
                              r.IMovieRatingsRepository movieRatingsRepository,
                              r.IUserRepository userRepository,
                              IMapper mapper)
        {
            _mediator = mediator;
            _moviesRepository = moviesRepository;
            _movieRatingsRepository = movieRatingsRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async  Task<Response<int>> AddorUpdateMovieRating(m.MovieRating movieRating)
        {
            var movies = await _moviesRepository.GetMovies();
            var users = await _userRepository.GetUsers();
            if (!movies.Any(m => m.Id == movieRating.MovieId))
                return new Response<int> { StatusCode = HttpStatusCode.NotFound, ErrorMessage = "Move not found." };
            if (!users.Any(u => u.Id == movieRating.UserId))
                return new Response<int> { StatusCode = HttpStatusCode.NotFound, ErrorMessage = "User not found." };
            
            var movieRatings = await _movieRatingsRepository.GetMovieRatings();
            var movieRatingdao = _mapper.Map<r.MovieRating>(movieRating);
            movieRatingdao.Id = movieRatings.Where(mr => mr.MovieId == movieRating.MovieId && mr.UserId == movieRating.UserId).Select(mr => mr.Id).FirstOrDefault();
            var key = await _movieRatingsRepository.AddorUpdateMovieRating(movieRatingdao);
            _mediator.Nofity(movieRating.MovieId);
            return new Response<int> { Data = key, StatusCode = HttpStatusCode.OK };

        }

        public async Task<Response<IEnumerable<m.Movie>>> GetHighRatedMovies(int countOfMoviesToReturn)
        {
            var movies = await _moviesRepository.GetMovies();
            var highRatedMovies = movies.OrderByDescending(m => m.AverageRating).ThenBy(m => m.Title).Take(countOfMoviesToReturn).ToList();
            highRatedMovies.ForEach(m => m.AverageRating = RoundRating(m.AverageRating));
            return new Response<IEnumerable<m.Movie>>
            {
                Data = _mapper.Map<IEnumerable<m.Movie>>(highRatedMovies),
                StatusCode = HttpStatusCode.OK
            };

        }

        public async Task<Response<IEnumerable<m.Movie>>> GetHighRatedMoviesByUser(int userId, int countOfMoviesToReturn)
        {
            var users = await _userRepository.GetUsers();
            if (!users.Any(u => u.Id == userId))
                return new Response<IEnumerable<m.Movie>> { StatusCode = HttpStatusCode.NotFound, ErrorMessage = "User not found." };
            
            var movieRatings = await _movieRatingsRepository.GetMovieRatings();
            var movies = await _moviesRepository.GetMovies();
            var highRatedMoviesByUser = movieRatings.Where(r => r.UserId == userId)
                                        .Join(movies, r => r.MovieId, m => m.Id, (r, m) => new { r.Rating, m })
                                        .OrderByDescending(m => m.Rating).ThenBy(o => o.m.Title).Select(o => o.m).Take(countOfMoviesToReturn).ToList();

            highRatedMoviesByUser.ForEach(m => m.AverageRating = RoundRating(m.AverageRating));

            return new Response<IEnumerable<m.Movie>>
            {
                Data = _mapper.Map<IEnumerable<m.Movie>>(highRatedMoviesByUser),
                StatusCode = HttpStatusCode.OK
            };

        }

        public async Task<Response<IEnumerable<m.Movie>>> GetMovies(string title, int yearOfrelease, string genres)
        {
            var expressions = CreateSearchWhereClause(title, yearOfrelease, genres);
            var query = await _moviesRepository.GetMovies();
            expressions.ForEach(exp => query = query.Where(exp));
            var movies = query.ToList();
            movies.ForEach(m => m.AverageRating = RoundRating(m.AverageRating));
            var response = new Response<IEnumerable<m.Movie>>
            {
                Data = _mapper.Map<IEnumerable<m.Movie>>(movies),
                StatusCode = HttpStatusCode.OK
            };
            if (movies.Count == 0) response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        #region private methods
        private double RoundRating(double value)
        {
            var floor = Math.Floor(value);
            var decimalValue = value - floor;
            if (decimalValue <= 0.249)
                value = floor;
            else if (decimalValue >= 0.25 && decimalValue <= 0.749)
                value = floor + 0.5;
            else
                value = floor + 1;

            return value;
        }

        private List<Expression<Func<r.Movie, bool>>> CreateSearchWhereClause(string title, int yearOfrelease, string genres)
        {
            var expressions = new List<Expression<Func<r.Movie, bool>>>();
            Expression<Func<r.Movie, bool>> baseExpression = PredicateBuilder.New<r.Movie>(true);
          
            if (!string.IsNullOrEmpty(title)) expressions.Add(baseExpression.And(m => m.Title.ToLower().Contains(title.ToLower())));
            if (yearOfrelease > 0) expressions.Add(baseExpression.And(m => m.YearOfRelease == yearOfrelease));
            if (!string.IsNullOrEmpty(genres))
            {
                baseExpression = PredicateBuilder.New<r.Movie>();
                var genresList = genres.Split(",").ToList();
                genresList.ForEach(searchString => baseExpression = baseExpression.Or(m => m.Genres.ToLower().Contains(searchString.ToLower())));
                expressions.Add(baseExpression);
            }

            return expressions;
        }
        #endregion
    }
}