
namespace MoviesAPI.Models
{
    public class MovieRating
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public double Rating { get; set; }
    }
}
