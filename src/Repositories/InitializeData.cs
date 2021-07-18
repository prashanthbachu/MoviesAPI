namespace MoviesAPI.Repositories
{
    public static class InitializeData
    {
        public static void Initialize() 
        {
            using var dbContext = new MoviesContext();

            AddUsers(dbContext);
            dbContext.SaveChanges();
            AddMovies(dbContext);
            dbContext.SaveChanges();
            AddMovieRatings(dbContext);
            dbContext.SaveChanges();
        }

        private static void AddUsers(MoviesContext dbContext)
        {
            dbContext.Users.Add(new User { FirstName = "John", LastName = "Rizo" });
            dbContext.Users.Add(new User { FirstName = "Peter", LastName = "K" });
            dbContext.Users.Add(new User { FirstName = "Prashanth", LastName = "Bachu" });
            dbContext.Users.Add(new User { FirstName = "Mike", LastName = "Tiemens" });
            dbContext.Users.Add(new User { FirstName = "Jerry", LastName = "B" });

        }

        private static void AddMovies(MoviesContext dbContext)
        {
            dbContext.Movies.Add(new Movie { Title = "Lion King", Genres = "Drama, Comedy", RunningTime = 120, YearOfRelease = 2017 });
            dbContext.Movies.Add(new Movie { Title = "Fractured", Genres = "Action", RunningTime = 100, YearOfRelease = 2019 });
            dbContext.Movies.Add(new Movie { Title = "Ice Road", Genres = "Drama", RunningTime = 120, YearOfRelease = 2016 });
            dbContext.Movies.Add(new Movie { Title = "Fear Street", Genres = "Drama", RunningTime = 115, YearOfRelease = 2019 });
            dbContext.Movies.Add(new Movie { Title = "Bad Trip", Genres = "Action, Comedy", RunningTime = 90, YearOfRelease = 2016 });
            dbContext.Movies.Add(new Movie { Title = "The Little Love of Mine", Genres = "Comedy, Drama", RunningTime = 100, YearOfRelease = 2020 });
            dbContext.Movies.Add(new Movie { Title = "The Laundromat", Genres = "Drama", RunningTime = 105, YearOfRelease = 2020 });
            dbContext.Movies.Add(new Movie { Title = "Muder Mystery", Genres = "Action", RunningTime = 130, YearOfRelease = 2021 });
            dbContext.Movies.Add(new Movie { Title = "Midnight Sun", Genres = "Romance, Action", RunningTime = 110, YearOfRelease = 2021 });
            dbContext.Movies.Add(new Movie { Title = "Close", Genres = "Action", RunningTime = 115, YearOfRelease = 2017 });
            dbContext.Movies.Add(new Movie { Title = "Enola Holmes", Genres = "Adventure", RunningTime = 180, YearOfRelease = 2018 });
            dbContext.Movies.Add(new Movie { Title = "The Best of Enemies", Genres = "History", RunningTime = 140, YearOfRelease = 2016 });
            dbContext.Movies.Add(new Movie { Title = "America The Motion Picture", Genres = "Animation", RunningTime = 108, YearOfRelease = 2020 });
            dbContext.Movies.Add(new Movie { Title = "I Care a Lot", Genres = "Comedy", RunningTime = 125, YearOfRelease = 2021 });
            dbContext.Movies.Add(new Movie { Title = "The Outpost", Genres = "Action", RunningTime = 110, YearOfRelease = 2019 });
            dbContext.Movies.Add(new Movie { Title = "The Platform", Genres = "Fiction", RunningTime = 120, YearOfRelease = 2017 });
            dbContext.Movies.Add(new Movie { Title = "2 Hearts", Genres = "Drama", RunningTime = 110, YearOfRelease = 2018 });
            dbContext.Movies.Add(new Movie { Title = "London", Genres = "Action", RunningTime = 170, YearOfRelease = 2020 });
            dbContext.Movies.Add(new Movie { Title = "Wish Dragon", Genres = "Comedy", RunningTime = 120, YearOfRelease = 2021 });
            dbContext.Movies.Add(new Movie { Title = "Peppermint", Genres = "Action", RunningTime = 130, YearOfRelease = 2019 });

        }

        private static void AddMovieRatings(MoviesContext dbContext)
        {
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 1, UserId = 1, Rating = 3.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 2, UserId = 1, Rating = 3.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 3, UserId = 1, Rating = 3.4D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 4, UserId = 1, Rating = 3.16D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 5, UserId = 1, Rating = 2.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 6, UserId = 1, Rating = 4.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 7, UserId = 1, Rating = 3.776D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 1, UserId = 2, Rating = 3.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 2, UserId = 2, Rating = 3.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 3, UserId = 2, Rating = 3.4D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 4, UserId = 2, Rating = 3.16D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 8, UserId = 2, Rating = 2.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 9, UserId = 2, Rating = 4.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 10, UserId = 2, Rating = 3.776D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 1, UserId = 3, Rating = 3.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 2, UserId = 3, Rating = 3.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 3, UserId = 3, Rating = 3.4D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 8, UserId = 3, Rating = 3.16D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 9, UserId = 3, Rating = 2.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 10, UserId = 3, Rating = 4.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 11, UserId = 3, Rating = 3.776D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 11, UserId = 4, Rating = 3.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 12, UserId = 4, Rating = 3.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 13, UserId = 4, Rating = 3.4D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 14, UserId = 4, Rating = 3.16D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 15, UserId = 4, Rating = 2.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 16, UserId = 4, Rating = 4.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 17, UserId = 4, Rating = 3.776D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 11, UserId = 5, Rating = 3.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 12, UserId = 5, Rating = 3.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 13, UserId = 5, Rating = 3.4D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 14, UserId = 5, Rating = 3.16D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 15, UserId = 5, Rating = 2.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 16, UserId = 5, Rating = 4.86D });
            dbContext.MovieRatings.Add(new MovieRating { MovieId = 17, UserId = 5, Rating = 3.776D });
        }
    }
}
