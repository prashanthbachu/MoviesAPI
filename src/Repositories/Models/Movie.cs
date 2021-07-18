using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Repositories
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int RunningTime { get; set; }
        public string Genres { get; set; }
        public double AverageRating { get; set; }
        public List<MovieRating> MovieRatings { get; set; }
    }
}