using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recru_it.Data;
using recru_it.Models;

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