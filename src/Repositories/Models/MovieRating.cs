using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Repositories
{
    public class MovieRating
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public double Rating { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public User User { get; set; }
    }
}
