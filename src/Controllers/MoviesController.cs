using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using MoviesAPI.Services;
using System.Net;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MoviesController : Controller
    {
        private readonly IMoviesSerivce _moviesService;
        public MoviesController(IMoviesSerivce moviesService)
        {
            _moviesService = moviesService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Get(string title = null, int yearOfrelease = 0, string genres = null)
        {
            if(string.IsNullOrEmpty(title) && string.IsNullOrEmpty(genres) && yearOfrelease == 0)
            {
               return BadRequest("Please speicfy atleast one critera.");
            }
            var response = await _moviesService.GetMovies(title, yearOfrelease, genres);
            if (response.StatusCode == HttpStatusCode.OK) return Ok(response.Data);
            return StatusCode((int)response.StatusCode, response.ErrorMessage);
        }

        [HttpGet]
        [Route("highratedmovies/{countOfMoviesToReturn?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetHighRatedMovies([FromRoute]int countOfMoviesToReturn = 5)
        {
            var response = await _moviesService.GetHighRatedMovies(countOfMoviesToReturn);
            if (response.StatusCode == HttpStatusCode.OK) return Ok(response.Data);
            return StatusCode((int)response.StatusCode, response.ErrorMessage);
        }

        [HttpGet]
        [Route("highratedmovies/{userId}/{countOfMoviesToReturn?}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetHighRatedMoviesByUser([FromRoute]int userId, [FromRoute]int countOfMoviesToReturn = 5)
        {
            var response = await _moviesService.GetHighRatedMoviesByUser(userId, countOfMoviesToReturn);
            if (response.StatusCode == HttpStatusCode.OK) return Ok(response.Data);
            return StatusCode((int)response.StatusCode, response.ErrorMessage);
        }

        [HttpPost]
        [Route("ratemovie")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> AddorUpdateMovieRating(MovieRating movieRating)
        {
            if (movieRating?.Rating < 1 || movieRating?.Rating > 5)
                return BadRequest("Movie Rating should be in between 1 and 5.");
            var response =await _moviesService.AddorUpdateMovieRating(movieRating);
            if(response.StatusCode == HttpStatusCode.OK) return Ok();
            return StatusCode((int)response.StatusCode, response.ErrorMessage);
        }
    }
}
