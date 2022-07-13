using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubscriptionPlans.SubscriptionDB;

namespace SubscriptionPlans.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubPlanController : ControllerBase
    {
        private readonly SlingContext db;

        public SubPlanController(SlingContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPlans()
        {
            try
            {
                return Ok(db.SubPlans.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/GetById/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var plan = from plans in db.SubPlans select plans;
                if (id != null)
                {
                    plan = plan.Where(s => s.PlanId == id);
                }
                return Ok(plan);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpPut("/UpdatePlan/{id}")]
        public async Task<ActionResult> UpdatePlanPrice(int id, SubPlan plan)
        {
            if (id != plan.PlanId)
            {
                return BadRequest();
            }
            try
            {
                db.Entry(plan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
