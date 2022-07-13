using Microsoft.AspNetCore.Mvc;
using MovieAPI.MovieDB;


using MovieAPI.Repo;

namespace MovieAPI.Service
{
    public class ServiceRepo : IService<Movie>
    {
        private readonly IMovieRepo<Movie> movierepo;

        public ServiceRepo(IMovieRepo<Movie> movierepo)
        {
            this.movierepo = movierepo;
        }

        public List<Movie> getAllMovies()
        {
            return  movierepo.getAllMovies();

        }

        public List<Movie> GetMovieByCategory(string category)
        {
            return movierepo.GetMovieByCategory(category);
        }

       

        public List<Movie> GetMovieByLanguage(string Language)
        {
            return movierepo.GetMovieByLanguage(Language);
        }

        public List<Movie> GetMovieByName(string name)
        {
            return movierepo.GetMovieByName(name);
        }

        public Movie GetMovieByRandom()
        {
            return movierepo.GetMovieByRandom();
        }

        public List<Movie> GetMovieByRating(int rating)
        {
            return movierepo.GetMovieByRating(rating);
        }


    }
}
