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
    [Route("api/JobApplications")]
    public class JobApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/JobApplications
        [HttpGet]
        public IEnumerable<JobApplication> GetJobApplications()
        {
            return _context.JobApplications;
        }

        // GET: api/JobApplications/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobApplication([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobApplication = await _context.JobApplications.SingleOrDefaultAsync(m => m.Id == id);

            if (jobApplication == null)
            {
                return NotFound();
            }

            return Ok(jobApplication);
        }

        // PUT: api/JobApplications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobApplication([FromRoute] string id, [FromBody] JobApplication jobApplication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobApplication.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobApplication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobApplicationExists(id))
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

        // POST: api/JobApplications
        [HttpPost]
        public async Task<IActionResult> PostJobApplication([FromBody] JobApplication jobApplication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.JobApplications.Add(jobApplication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobApplication", new { id = jobApplication.Id }, jobApplication);
        }

        // DELETE: api/JobApplications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobApplication([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobApplication = await _context.JobApplications.SingleOrDefaultAsync(m => m.Id == id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            _context.JobApplications.Remove(jobApplication);
            await _context.SaveChangesAsync();

            return Ok(jobApplication);
        }

        private bool JobApplicationExists(string id)
        {
            return _context.JobApplications.Any(e => e.Id == id);
        }
    }
}