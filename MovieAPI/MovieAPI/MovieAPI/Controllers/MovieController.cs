using Microsoft.AspNetCore.Mvc;
using MovieAPI.MovieDB;
using MovieAPI.Service;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IService<Movie> db; 

        public MovieController(IService<Movie> db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Movie>>> GetAllMovies()
        {
            if(db.getAllMovies()==null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(db.getAllMovies());
            }
        }



        [HttpGet("/GetByLanguage/{Language}")]
        public async Task<ActionResult<List<Movie>>> GetByLanguage(string Language)
        {
            if (db.GetMovieByLanguage(Language) == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(db.GetMovieByLanguage(Language));
            }
        }

        [HttpGet("/GetByCategory/{Category}")]
        public async Task<ActionResult<List<Movie>>> GetByCategory(string Category)
        {
            if (db.GetMovieByCategory(Category) == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(db.GetMovieByCategory(Category));
            }
        }


        [HttpGet("/GetByAlphabets/{Alphabets}")]
        public async Task<ActionResult<List<Movie>>> GetByName(string Alphabets)
        {
            if (db.GetMovieByName(Alphabets) == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(db.GetMovieByName(Alphabets));
            }
        }

        
        [HttpGet("/getByRating/{rating}")]        
        public async Task<ActionResult<List<Movie>>> GetByRating(int rating)
        {
            if (db.GetMovieByRating(rating) == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(db.GetMovieByRating(rating));
            }
        }

        [HttpGet("/GetByRandom")]
        
        public async Task<ActionResult> GetRandomMovie()
        {
            
            
            if (db.GetMovieByRandom() == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(db.GetMovieByRandom());
            }
        }

    }
}
