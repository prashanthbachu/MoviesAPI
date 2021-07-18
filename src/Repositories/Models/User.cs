using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Repositories
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<MovieRating> MovieRatings { get; set; }
    }
}
