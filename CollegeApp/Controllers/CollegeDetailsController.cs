using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CollegeApp.Models;
using System.Collections.Generic;
using CollegeApp.ModelsDb;
using System.Net.Http;

namespace CollegeApp.Controllers
{
    public class CollegeDetailsController : ApiController
    {
        private CollegeDbEntities3 db = new CollegeDbEntities3();
        //GET:api/CollegeDetails/5
        public async Task<IHttpActionResult> GetCollegeDetails(int id) 
        {
             //db.Configuration.LazyLoadingEnabled = false;
            CollegeDetail college = new CollegeDetail();
            college = db.CollegeDetails.Where(e => e.CollegeID == id).FirstOrDefault();
            //college = db.CollegeDetails.Where(e => e.CollegeID == id).FirstOrDefault();

            if (college == null)
            {
                return NotFound();
            }
            CollegeData cdata = new CollegeData();
            List<Comments> list = new List<Comments>();

            foreach (var item in college.Comments)
            {
                Comments comment = new Comments();
                comment.CollegeID = item.CollegeID;
                comment.CommentDescription = item.CommentDescription;
                comment.CommentID = item.CommentID;
                comment.CreatedDate = item.CreatedDate;
                list.Add(comment);
            }
            cdata.comments = list;
            return Ok(cdata);
        }

        // PUT: api/CollegeDetails/5  
        public async Task<IHttpActionResult> PutCollegeDetail(int id, CollegeDetail collegeDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != collegeDetail.CollegeID)
            {
                return BadRequest();
            }
            db.Entry(collegeDetail).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollegeDetailExists(id))
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

        // POST: api/CollegeDetails

        public async Task<IHttpActionResult> PostCollegeDetail(CollegeDetail collegeDetail)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }
             db.CollegeDetails.Add(collegeDetail);
             await db.SaveChangesAsync();
             return CreatedAtRoute("DefaultApi", new { id = collegeDetail.CollegeID }, collegeDetail);
         } 
        [HttpPost]
        public IHttpActionResult Edit(CollegeDetail collegeDetail)
            {
            try
            {
                if (ModelState.IsValid)
                {   
                    db.Entry(collegeDetail).State=EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (System.Exception /* dex */)
           {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return Ok(collegeDetail);
        } 

        // DELETE: api/CollegeDetails/5  
        /* [ResponseType(typeof(CollegeDetail))]
            public async Task<IHttpActionResult> DeleteCollegeDetail(int id)
             {
                 CollegeDetail collegeDetail = await db.CollegeDetails.FindAsync(id);
                 if (collegeDetail == null)
                 {
                     return NotFound();
                 }

                 db.CollegeDetails.Remove(collegeDetail);
                 await db.SaveChangesAsync();
                 return Ok(collegeDetail);
             } */
        [HttpGet]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                CollegeDetail student = db.CollegeDetails.Find(id);
                db.CollegeDetails.Remove(student);
                db.SaveChanges();
            }
            catch (System.Exception e)
            {
                // uncomment dex and log error. 
               // return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return Request.CreateResponse(HttpStatusCode.OK,"Deleted Successfully");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool CollegeDetailExists(int id)
        {
            return db.CollegeDetails.Count(e => e.CollegeID == id) > 0;
        }
    }
}