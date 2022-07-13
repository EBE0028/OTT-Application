using MovieAPI.MovieDB;

namespace MovieAPI.Service
{
    public interface IService <Movie>
    {
        List<Movie> getAllMovies();

        

        List<Movie> GetMovieByName(string name);

        List<Movie> GetMovieByRating(int rating);

        List<Movie> GetMovieByCategory(string category);

        List<Movie> GetMovieByLanguage(string Language);

        Movie GetMovieByRandom();

        
    }
}
