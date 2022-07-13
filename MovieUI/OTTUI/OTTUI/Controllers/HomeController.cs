using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OTTUI.Models;
using System.Diagnostics;

namespace OTTUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        string Baseurl = "https://localhost:44302/";
        public async Task<IActionResult> Index()
        {
            Movie movie = new Movie();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:7290/GetByRandom");
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    movie = JsonConvert.DeserializeObject<Movie>(apiResponse);
                }
            }
            ViewBag.Image = movie.MovieImage;
            ViewBag.Name = movie.MovieName;
            ViewBag.Rating = movie.MovieRating;
            ViewBag.Cat = movie.MovieCategory;
            ViewBag.Link = movie.MovieContent;
            return View(movie);
        }

        public async Task<ActionResult> NotFound()
        {
            return View();
        }
    }
}
