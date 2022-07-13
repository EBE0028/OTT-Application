using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using OTTUI.Models;
using Azure;
using System.Collections;

namespace OTTUI.Controllers
{
    public class RatedMovie
    {
        public List<Movie> FiveStar { set; get; }

        public List<Movie> FourStar { set; get; }

        public List<Movie> ThreeStar { set; get; }
    }
    public class MovieController : Controller
    {

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
            return View(movie);
        }

        public async Task<ActionResult> TopRated()
        {
            if (HttpContext.Session.GetString("CustomerName") != null && HttpContext.Session.GetInt32("remaningDays") != 0)
            {
                string Baseurl = "https://localhost:7290/";
                List<Movie> MovieInfo = new List<Movie>();
                RatedMovie ratedMovie = new RatedMovie();

                //Five star movie
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("getByRating/5");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var MovieResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        MovieInfo = JsonConvert.DeserializeObject<List<Movie>>(MovieResponse);

                    }


                    ratedMovie.FiveStar = MovieInfo;

                }

                //Four star movie
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("getByRating/4");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var MovieResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        MovieInfo = JsonConvert.DeserializeObject<List<Movie>>(MovieResponse);

                    }


                    ratedMovie.FourStar = MovieInfo;





                }
                //Three star movie
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("getByRating/3");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var MovieResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        MovieInfo = JsonConvert.DeserializeObject<List<Movie>>(MovieResponse);

                    }
                    ratedMovie.ThreeStar = MovieInfo;

                }
                return View(ratedMovie);
            }
            if (HttpContext.Session.GetInt32("remaningDays") == 0)
            {
                return RedirectToAction("Payment", "Customer");
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }








        }


        public async Task<ActionResult> TamilMovies()
        {
            if (HttpContext.Session.GetString("CustomerName") != null && HttpContext.Session.GetInt32("remaningDays") != 0)
            {
                string Baseurl = "https://localhost:7290/";
                List<Movie> MovieInfo = new List<Movie>();
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("GetByLanguage/Tamil");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var MovieResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        MovieInfo = JsonConvert.DeserializeObject<List<Movie>>(MovieResponse);

                    }
                }
                return View(MovieInfo);
            }

            if (HttpContext.Session.GetInt32("remaningDays") == 0)
            {
                return RedirectToAction("Payment", "Customer");
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }

        }

        public async Task<ActionResult> EnglishMovies()
        {
            if (HttpContext.Session.GetString("CustomerName") != null && HttpContext.Session.GetInt32("remaningDays") != 0)
            {
                string Baseurl = "https://localhost:7290/";
                List<Movie> MovieInfo = new List<Movie>();
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("GetByLanguage/English");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var MovieResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        MovieInfo = JsonConvert.DeserializeObject<List<Movie>>(MovieResponse);

                    }
                }
                return View(MovieInfo);
            }
            if (HttpContext.Session.GetInt32("remaningDays") == 0)
            {
                return RedirectToAction("Payment", "Customer");
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
        }

        public async Task<ActionResult> TeleguMovies()
        {
            if (HttpContext.Session.GetString("CustomerName") != null && HttpContext.Session.GetInt32("remaningDays") != 0)
            {
                string Baseurl = "https://localhost:7290/";
                List<Movie> MovieInfo = new List<Movie>();
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("GetByLanguage/Telgu");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var MovieResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        MovieInfo = JsonConvert.DeserializeObject<List<Movie>>(MovieResponse);

                    }
                }
                return View(MovieInfo);
            }
            if (HttpContext.Session.GetInt32("remaningDays") == 0)
            {
                return RedirectToAction("Payment", "Customer");
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }

        }

        public async Task<ActionResult> HindiMovies()
        {
            if (HttpContext.Session.GetString("CustomerName") != null && HttpContext.Session.GetInt32("remaningDays") != 0)
            {
                string Baseurl = "https://localhost:7290/";
                List<Movie> MovieInfo = new List<Movie>();
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("GetByLanguage/Hindi");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var MovieResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        MovieInfo = JsonConvert.DeserializeObject<List<Movie>>(MovieResponse);

                    }
                }
                return View(MovieInfo);
            }
            if (HttpContext.Session.GetInt32("remaningDays") == 0)
            {
                return RedirectToAction("Payment", "Customer");
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }

        }

        public async Task<ActionResult> HorrorMovies()
        {
            if (HttpContext.Session.GetString("CustomerName") != null && HttpContext.Session.GetInt32("remaningDays") != 0)
            {
                string Baseurl = "https://localhost:7290/";
                List<Movie> MovieInfo = new List<Movie>();
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("GetByCategory/Horror");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var MovieResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        MovieInfo = JsonConvert.DeserializeObject<List<Movie>>(MovieResponse);

                    }
                }
                return View(MovieInfo);
            }
                


            if (HttpContext.Session.GetInt32("remaningDays") == 0)
            {
                return RedirectToAction("Payment", "Customer");
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
        }

        public async Task<ActionResult> FantasyMovies()
        {
            if (HttpContext.Session.GetString("CustomerName") != null && HttpContext.Session.GetInt32("remaningDays") != 0)
            {
                string Baseurl = "https://localhost:7290/";
                List<Movie> MovieInfo = new List<Movie>();
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("GetByCategory/Fantacy");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var MovieResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        MovieInfo = JsonConvert.DeserializeObject<List<Movie>>(MovieResponse);

                    }
                }
                return View(MovieInfo);
            }
            if (HttpContext.Session.GetInt32("remaningDays") == 0)
            {
                return RedirectToAction("Payment", "Customer");
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }

        }

        public async Task<ActionResult> ActionMovies()
        {
            if (HttpContext.Session.GetString("CustomerName") != null && HttpContext.Session.GetInt32("remaningDays") != 0)
            {
                string Baseurl = "https://localhost:7290/";
                List<Movie> MovieInfo = new List<Movie>();
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("GetByCategory/Action");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var MovieResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        MovieInfo = JsonConvert.DeserializeObject<List<Movie>>(MovieResponse);

                    }
                }
                return View(MovieInfo);
            }
            if (HttpContext.Session.GetInt32("remaningDays") == 0)
            {
                return RedirectToAction("Payment", "Customer");
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }

        }

        public async Task<ActionResult> DocumentryMovies()
        {
            if (HttpContext.Session.GetString("CustomerName") != null && HttpContext.Session.GetInt32("remaningDays") != 0)
            {
                string Baseurl = "https://localhost:7290/";
                List<Movie> MovieInfo = new List<Movie>();
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("GetByCategory/Documentry");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var MovieResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        MovieInfo = JsonConvert.DeserializeObject<List<Movie>>(MovieResponse);

                    }
                }
                return View(MovieInfo);
            }
            if (HttpContext.Session.GetInt32("remaningDays") == 0)
            {
                return RedirectToAction("Payment", "Customer");
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }

        }



        [HttpPost]
        public async Task<ActionResult> SearchByName(string name)
        {

            string Baseurl = "https://localhost:7290/";

            List<Movie> movie = new List<Movie>();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("GetByAlphabets/" + name);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var MovieResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    movie = JsonConvert.DeserializeObject<List<Movie>>(MovieResponse);

                }
            }
            ViewBag.MovieStatus = "Pass";
            return View(movie);
        }


    }
}

