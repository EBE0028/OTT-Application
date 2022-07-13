using MovieAPI.MovieDB;
using System.Web.Helpers;

namespace MovieAPI.Repo
{
    public class RepoClass : IMovieRepo<Movie>
    {
        private readonly SlingContext db;
        public RepoClass(SlingContext db)
        {
            this.db = db;
        }

        public List<Movie> getAllMovies()
        {
            return db.Movies.ToList();

        }

        public List<Movie> GetMovieByCategory(string category)
        {
            var movies = from movie in db.Movies
                         select movie;
            if (!String.IsNullOrEmpty(category))
            {
                movies = movies.Where(s => s.MovieCategory!.Contains(category));
            }

            return movies.ToList();
        }

        

        public List<Movie> GetMovieByLanguage(string Language)
        {
            var movies = from movie in db.Movies
                         select movie;
            if (!String.IsNullOrEmpty(Language))
            {
                movies = movies.Where(s => s.MovieLanguage!.Contains(Language));
            }

            return movies.ToList();
        }

        public List<Movie> GetMovieByName(string name)
        {
            var movies = from movie in db.Movies select movie;
            if (!String.IsNullOrEmpty(name))
            {
                movies = movies.Where(s => s.MovieName!.Contains(name));
            }
            return movies.ToList();
        }

        public Movie GetMovieByRandom()
        {
            
            Random rnd = new Random();
            int num = rnd.Next(1, 24);
            Movie movies = db.Movies.Find(num);
            return movies;
        }

        public List<Movie> GetMovieByRating(int rating)
        {
            var movies = from movie in db.Movies select movie;
            if (rating != null)
            {
                movies = movies.Where(s => s.MovieRating.Equals(rating));
            }
            return movies.ToList();
        }

        //public List<Movie> getAllCrime()
        //{
        //    var movies = from movie in db.Movies
        //                 select movie;
        //    if (!String.IsNullOrEmpty("Crime thriller"))
        //    {
        //        movies = movies.Where(s => s.MovieCategory!.Contains("Crime thriller"));
        //    }

        //    return movies.ToList();
        //}
    }
}
