using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recru_it.Data;
using recru_it.Models;
using recru_it.Extensions;

namespace recru_it.Controllers
{
    [Produces("application/json")]
    [Route("api/JobPosts")]
    public class JobPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/JobPosts
        [HttpGet]
        public IEnumerable<JobPost> GetJobPosts()
        {
            return _context.JobPosts;
        }

        // GET: api/JobPosts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobPost([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobPost = await _context.JobPosts.SingleOrDefaultAsync(m => m.Id == id);

            if (jobPost == null)
            {
                return NotFound();
            }

            return Ok(jobPost);
        }


        //api/JobPosts/tagId=?? WIP
        /*[HttpPost]
        public void GetAllJobPostsByTagIds([FromBody] string[] tagIds)
        {
            //get tags from ids

            List<Tag> searchTags = new List<Tag>();
            foreach(var id in tagIds)
            {
                Tag tag = _context.Tags.Where(t => t.Id == id).First();
                searchTags.Add(tag);
            }
            GetAllJobPostsByTags(searchTags);

        }
        */
        /*[HttpGet("{jobPostId}")]
        public void GetJobPostsByJobPostTags([FromRoute] string jobPostId)
        {
            JobPost searchJobPost = _context.JobPosts.Where(jp => jp.Id == jobPostId).First();
            GetAllJobPostsByTags(searchJobPost.Tags.ToList());
        }*/


        // PUT: api/JobPosts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobPost([FromRoute] string id, [FromBody] JobPost jobPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobPost.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobPostExists(id))
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

        // POST: api/JobPosts
        [HttpPost]
        public async Task<IActionResult> PostJobPost([FromBody] JobPost jobPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.JobPosts.Add(jobPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobPost", new { id = jobPost.Id }, jobPost);
        }


        [HttpPost]
        [Route("GetAllJobPostsByTags")]
        public ActionResult GetAllJobPostsByTags([FromBody] List<Tag> tags)
        {
            tags = _context.Tags.ToList();
            CompareTags compareTags = new CompareTags(_context);

            List<JobPost> jp = compareTags.GetJobPostsByTags(tags);
            List<JobPost> distictJp = jp.Distinct().ToList();
            if (jp.Count() > 0)
            {
                return Ok(distictJp);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        [Route("GetAllJobPostsByJobApplication")]
        public void GetAllJobPostsByJobApplication([FromBody] JobApplication jobApplication)
        {
            GetAllJobPostsByTags(jobApplication.Tags);
        }


        // DELETE: api/JobPosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobPost([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobPost = await _context.JobPosts.SingleOrDefaultAsync(m => m.Id == id);
            if (jobPost == null)
            {
                return NotFound();
            }

            _context.JobPosts.Remove(jobPost);
            await _context.SaveChangesAsync();

            return Ok(jobPost);
        }


        



        private bool JobPostExists(string id)
        {
            return _context.JobPosts.Any(e => e.Id == id);
        }
    }
}