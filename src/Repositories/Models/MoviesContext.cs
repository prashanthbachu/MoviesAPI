using Microsoft.EntityFrameworkCore;
using System;

namespace MoviesAPI.Repositories
{
    public class MoviesContext : DbContext, IMoviesContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            optionbuilder.UseSqlite("Data Source=.\\MovieRatings.db");
            #if DEBUG
                optionbuilder.LogTo(Console.WriteLine);
                optionbuilder.EnableSensitiveDataLogging();
            #endif
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasIndex(m => m.Title).IsUnique();
            modelBuilder.Entity<MovieRating>().HasOne(p => p.Movie).WithMany(b => b.MovieRatings).HasForeignKey(p => p.MovieId);
            modelBuilder.Entity<MovieRating>().HasOne(p => p.User).WithMany(b => b.MovieRatings).HasForeignKey(p => p.UserId);
        }
    }
}
