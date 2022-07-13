using Amazon.EC2.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NPOI.SS.Util;
using OTTUI.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace OTTUI.Controllers
{
    public class UserObj
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPassword { get; set; }
        public string planame { get; set; }
        public int planprice { get; set; }
        public int RemaningDays { get; set; }
        public string Status { get; set; }

        public string PayDate { get; set; }
    }
    public class Plan
    {
        public int PlanId { get; set; }

        public string PlanName { get; set; }

        public int PlanPrice { get; set; }

        public int PlanDays { get; set; }
    }
    public class UpdateCustomer
    {
        public int CustID { get; set; }
        public string custName { get; set; }
        public string custEmail { get; set; }
        public string custPassword { get; set; }
        public int custPlan { get; set; }
        public string payDate { get; set; }
    }
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Customer customer)
        {
            try
            {
                var userObj = new UserObj();
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync("https://localhost:7178/CustomerById/" + customer.CustId);
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        //JsonOnj result = JsonConvert.DeserializeObject<JsonOnj>(apiResponse);
                        userObj = JsonConvert.DeserializeObject<UserObj>(apiResponse);
                    }
                }
                if (userObj == null)
                {
                    ViewBag.LoginStatus = "Fail";
                    return View();
                }
                if (userObj.CustomerPassword == customer.CustPassword)
                {
                    HttpContext.Session.SetString("CustomerName", userObj.CustomerName);
                    HttpContext.Session.SetString("customerEmail", userObj.CustomerEmail);
                    HttpContext.Session.SetString("customerPassword", userObj.CustomerPassword);
                    HttpContext.Session.SetString("planame", userObj.planame);
                    HttpContext.Session.SetInt32("planprice", userObj.planprice);
                    HttpContext.Session.SetInt32("remaningDays", userObj.RemaningDays);
                    HttpContext.Session.SetString("status", userObj.Status);
                    HttpContext.Session.SetInt32("CustID", customer.CustId);
                    HttpContext.Session.SetString("PaymentDate", Convert.ToString(userObj.PayDate));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.LoginStatus = "Fail";
                    return View();
                }
            }
            catch
            {
                return RedirectToAction("NotFound", "Home");
            }


        }


        public async Task<IActionResult> CustomerDetails()
        {
            try
            {
                if (HttpContext.Session.GetInt32("CustID") != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Customer");
                }
            }
            catch
            {
                return RedirectToAction("NotFound", "Home");
            }



        }

        public async Task<IActionResult> Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Customer");
            }
            catch
            {
                return RedirectToAction("NotFound", "Home");
            }

        }

        [HttpGet]
        public async Task<IActionResult> Payment()
        {
            try
            {
                List<Plan> plan = new List<Plan>();
                string Baseurl = "https://localhost:7011/";

                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("api/SubPlan");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var PlanResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        plan = JsonConvert.DeserializeObject<List<Plan>>(PlanResponse);

                    }
                }


                return View(plan);
            }
            catch
            {
                return RedirectToAction("NotFound", "Home");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Payment(int id)
        {
            try
            {


                if (HttpContext.Session.GetInt32("CustID") != 0)
                {
                    var customerID = HttpContext.Session.GetInt32("CustID");

                    using (var httpClient = new HttpClient())
                    {
                        UpdateCustomer customer = new UpdateCustomer();
                        customer.CustID = (int)customerID;
                        customer.custName = HttpContext.Session.GetString("CustomerName");
                        customer.custEmail = HttpContext.Session.GetString("customerEmail");
                        customer.custPassword = HttpContext.Session.GetString("customerPassword");
                        customer.custPlan = id;
                        customer.payDate = DateTime.Now.ToString("yyyy-MM-dd");


                        StringContent content1 = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PutAsync("https://localhost:7178/CustomerById/" + customerID, content1))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            ViewBag.Result = "Success";
                            customer = JsonConvert.DeserializeObject<UpdateCustomer>(apiResponse);
                        }

                    }
                    if (ViewBag.Result == "Success")
                    {

                        return RedirectToAction("PaymentSucessful");
                    }
                    else
                    {
                        HttpContext.Session.SetString("PaymentStatus", "Fail");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch
            {
                return RedirectToAction("NotFound", "Home");
            }

        }

        public async Task<ActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(Customer customer)
        {
            try
            {
                UserObj customerObj = new UserObj();
                var todayDate = DateTime.Now.ToString("yyyy-MM-dd");
                customer.PayDate = Convert.ToDateTime(todayDate);
                if (customer.CustPlan == 0)
                {
                    customer.CustPlan = 1;
                }
                using (var httpClient = new HttpClient())
                {

                    StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("https://localhost:7178/NewCustomer", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        customerObj = JsonConvert.DeserializeObject<UserObj>(apiResponse);
                    }
                }
                return RedirectToAction("RegSuc");
            }
            catch
            {
                return RedirectToAction("NotFound", "Home");
            }
            
        }


        [HttpGet]
        public async Task<ActionResult> UpdateCustomer()
        {
            UserObj customer = new UserObj();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7178/CustomerById/" + HttpContext.Session.GetInt32("CustID")))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customer = JsonConvert.DeserializeObject<UserObj>(apiResponse);
                }
            }
            return View();

        }
        [HttpPost]
        public async Task<ActionResult> UpdateCustomer(Customer customer)
        {
            UpdateCustomer updateuser = new UpdateCustomer();

            using (var httpClient = new HttpClient())
            {
                if (HttpContext.Session.GetString("planame") == "Gold")
                {
                    updateuser.custPlan = 1;
                }
                if (HttpContext.Session.GetString("planame") == "Silver")
                {
                    updateuser.custPlan = 2;
                }
                if (HttpContext.Session.GetString("planame") == "Bronze")
                {
                    updateuser.custPlan = 3;
                }
                var payDate = Convert.ToDateTime((HttpContext.Session.GetString("PaymentDate")));
                SimpleDateFormat newFormat = new SimpleDateFormat("MM-dd-yyyy");
                updateuser.payDate = newFormat.Format(payDate);
                
                
                updateuser.custEmail = customer.CustEmail;
                updateuser.custName = customer.CustName;
                updateuser.custPassword = customer.CustPassword;
                updateuser.CustID = (int)HttpContext.Session.GetInt32("CustID");
                var custID = updateuser.CustID;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(updateuser), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:7178/CustomerById/"+custID, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    updateuser = JsonConvert.DeserializeObject<UpdateCustomer>(apiResponse);
                }
                return RedirectToAction("Index", "Home");

            }
        }


        public async Task<ActionResult> ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(int id, string name, string email)
        {
            var userObj = new UserObj();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:7178/CustomerById/" + id);
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //JsonOnj result = JsonConvert.DeserializeObject<JsonOnj>(apiResponse);
                    userObj = JsonConvert.DeserializeObject<UserObj>(apiResponse);
                }
            }
            if (userObj.CustomerEmail == email && userObj.CustomerName == name)
            {
                ViewBag.Password = userObj.CustomerPassword;
            }
            else
            {
                ViewBag.Password = "Fail";
            }

            return View();
        }

        public async Task<ActionResult> PaymentSucessful()
        {
            try
            {
                HttpContext.Session.Clear();
                return View();
            }
            
            catch
            {
                return RedirectToAction("NotFound", "Home");
            }
        }

        public async Task<ActionResult> RegSuc()
        {
            try
            {
                HttpContext.Session.Clear();
                return View();
            }

            catch
            {
                return RedirectToAction("NotFound", "Home");
            }
        }
    }
}
