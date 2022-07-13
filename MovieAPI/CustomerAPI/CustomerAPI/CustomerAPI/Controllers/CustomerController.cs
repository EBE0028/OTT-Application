using CustomerAPI.CustomerDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace CustomerAPI.Controllers
{
    public class JsonObj
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

    

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly SlingContext db;

        public CustomerController(SlingContext db)
        {
            this.db = db;
        }

        [HttpGet("/GetAllPlan")]
        public async Task<ActionResult> GetAllPlan()
        {
            try
            {
                var responseString = "";
                var request = (HttpWebRequest)WebRequest.Create("https://localhost:7011/api/SubPlan");
                request.Method = "GET";
                request.ContentType = "application/json";

                using (var response1 = request.GetResponse())
                {
                    using (var reader = new StreamReader(response1.GetResponseStream()))
                    {
                        responseString = reader.ReadToEnd();
                    }
                   
                }
                
                return Ok(responseString);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public static string GetPlanById(int id)
        {
            try
            {
                var responseString = "";
                var request = (HttpWebRequest)WebRequest.Create("https://localhost:7011/GetById/" + id);
                request.Method = "GET";
                request.ContentType = "application/json";

                using (var response1 = request.GetResponse())
                {
                    using (var reader = new StreamReader(response1.GetResponseStream()))
                    {
                        responseString = reader.ReadToEnd();
                    }

                }

                return responseString;
            }
            catch (Exception ex)
            {
                return "null";
            }

        }

        [HttpGet("/CustomerById/{id}")]
        public async Task<ActionResult> getByCustomerId(int id)
        {
            try
            {
                Customer customer = db.Customers.Find(id);
                int planID = customer.CustPlan;
                string Status = "";

                if (planID != null)
                {
                    if (GetPlanById(planID) != null)
                    {
                        string GetByPlanResponce = GetPlanById(planID);
                        string[] ResponceIntoList = GetByPlanResponce.Split(",", StringSplitOptions.RemoveEmptyEntries);

                        string[] planameIntoList = ResponceIntoList[1].Split(":");
                        string planame = planameIntoList[1];
                        planame = planame.Replace("\"", "");

                        string[] planPricenameIntoList = ResponceIntoList[2].Split(":");
                        string planprice = planPricenameIntoList[1];

                        string[] planDaynameIntoList = ResponceIntoList[3].Split(":");
                        string[] planDayList = planDaynameIntoList[1].Split("}");
                        string plandays = planDayList[0];

                        string CustomerName = customer.CustName;

                        string CustomerEmail = customer.CustEmail;

                        string CustomerPassword = customer.CustPassword;

                        DateTime CustomerDate = customer.PayDate;

                        var TodaysDate = DateTime.Now.ToString("yyyy-mm-dd");
                        int RemaningDays = 0;
                        int numberOfDays = Convert.ToInt32((DateTime.Today - CustomerDate).TotalDays);
                        if (numberOfDays <= Convert.ToInt32(plandays))
                        {
                            Status = "Active";
                            RemaningDays = Convert.ToInt32(plandays) - numberOfDays;
                        }
                        else
                        {
                            Status = "InActive";
                            RemaningDays = 0;
                        }

                        

                        JsonObj responceObj = new JsonObj();
                        responceObj.Status = Status;
                        responceObj.planprice = Convert.ToInt32( planprice);
                        responceObj.planame = planame;
                        responceObj.CustomerName = CustomerName;
                        responceObj.CustomerEmail = CustomerEmail;
                        responceObj.CustomerPassword = CustomerPassword;
                        responceObj.RemaningDays = RemaningDays;
                        responceObj.PayDate = Convert.ToString(CustomerDate);



                        string output = JsonConvert.SerializeObject(responceObj);
                        JsonObj deserializedProduct = JsonConvert.DeserializeObject<JsonObj>(output);


                        return Ok(deserializedProduct);
                    }
                    else
                    {
                        return BadRequest("null");
                    }
                }
                else
                {
                    return BadRequest("null");
                }

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPut("/CustomerById/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id,Customer customer)
        {
            if (id != customer.CustId)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
                return Ok(customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (customer.CustId!=(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }

        [HttpPost("/NewCustomer")]
        public async Task<IActionResult> NewCutomer(Customer customer)
        {
            try
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return Ok(customer);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        
    }


}
