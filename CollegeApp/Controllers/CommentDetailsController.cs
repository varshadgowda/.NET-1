using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CollegeApp.Models;
using System.Collections;
using System.Collections.Generic;

namespace CollegeApp.Controllers
{
    public class CommentDetailsController : ApiController
    {
        private CollegeDbEntities3 db = new CollegeDbEntities3();

        // GET: api/CommentDetails
        public IQueryable<Comment> GetComments()
        {
            return db.Comments;
        }

        // GET: api/CommentDetails/5  
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> GetComment(int id)
        {
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        // PUT: api/CommentDetails/5  
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult>PutComment(int id, Comment comment)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id != comment.CommentID)

            {
                return BadRequest();
            }
            db.Entry(comment).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!CommentExists(id))
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
        
        // POST: api/CommentDetails 
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> PostComment(Comment comment)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Comments.Add(comment);
            await db.SaveChangesAsync();
            return CreatedAtRoute("DefaultApi", new { id = comment.CommentID }, comment);
        }

        // DELETE: api/CommentDetails/5
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult>DeleteComment(int id)
        {
            Comment comment = await db.Comments.FindAsync(id);
            if(comment == null)
            {
                return NotFound();
            }
            db.Comments.Remove(comment);
            await db.SaveChangesAsync();
            return Ok(comment);
        }
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool CommentExists(int id)
        {
            return db.Comments.Count(e => e.CommentID == id) > 0;
        }
    }
}
//college = db.CollegeDetails.Include("Comments")Where(e => e.CollegeID == id).FirstOrDefault();