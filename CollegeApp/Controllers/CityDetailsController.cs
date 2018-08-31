using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CollegeApp.Models;

namespace CollegeApp.Controllers
{
    public class CityDetailsController : ApiController
    {
        private CollegeDbEntities3 db = new CollegeDbEntities3();

        // GET: api/CityDetails  
        public IQueryable<City> GetCities()
        {
            return db.Cities;
        }

        // GET: api/CityDetails/5
        [ResponseType(typeof(City))]
        public  async Task<IHttpActionResult> GetCity(int id)
        {
            City city = await db.Cities.FindAsync(id);
            if(city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }

        // PUT: api/CityDetails/5  
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCity(int id, City city)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id != city.City_ID)
            {
                return BadRequest();
            }
            db.Entry(city).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!CityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CityDetails 
        [ResponseType(typeof(City))]
        public async Task<IHttpActionResult> PostCity(City city)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Cities.Add(city);
            await db.SaveChangesAsync();
            return CreatedAtRoute("DefaultApi", new { id = city.City_ID }, city);
        }

        // DELETE: api/CityDetails/5
        [ResponseType(typeof(City))]  
        public async Task<IHttpActionResult>DeleteCity(int id)
        {
            City city = await db.Cities.FindAsync(id);
            if(city == null)
            {
                return NotFound();
            }
            db.Cities.Remove(city);
            await db.SaveChangesAsync();
            return Ok(city);
        }
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool CityExists(int id)
        {
            return db.Cities.Count(e => e.City_ID == id) > 0;
        }

    }
}
/*List<Comment> comment = new List<Comment>();
comment = db.Comments.Include("CollegeDetails").Where(e => e.CommentID == id).ToList(); */