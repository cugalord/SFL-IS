using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sfl.Data;
using sfl.Models;

namespace sfl.Controllers_Api
{
    [Route("api/v1/Jobs")]
    [ApiController]
    public class JobsApiController : ControllerBase
    {
        private readonly CompanyContext _context;

        public JobsApiController(CompanyContext context)
        {
            _context = context;
        }

        // GET: api/JobsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            if (_context.Jobs == null)
            {
                return NotFound();
            }
            return await _context.Jobs.ToListAsync();
        }

        // GET: api/JobsApi/5
        /*[HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            if (_context.Jobs == null)
            {
                return NotFound();
            }
            var job = await _context.Jobs.FindAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }*/

        [HttpGet("{username}")]
        public async Task<ActionResult<IEnumerable<Job>>> GetJob(string username)
        {
            if (_context.Jobs == null)
            {
                return NotFound();
            }
            var jobs = _context.Jobs.Select(j => j).Where(j => j.StaffUsername == username).ToList();

            foreach (var job in jobs)
            {
                job.ParcelIDs = _context.JobsParcels.Select(pj => pj).Where(pj => pj.JobID == job.ID).Select(pj => pj.ParcelID).ToList();
                job.JobsParcels = _context.JobsParcels.Select(pj => pj).Where(pj => pj.JobID == job.ID).ToList();
            }

            if (jobs == null)
            {
                return NotFound();
            }

            return jobs;
        }

        // PUT: api/JobsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int id, Job job)
        {
            if (id != job.ID)
            {
                return BadRequest();
            }

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
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

        // POST: api/JobsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(Job job)
        {
            if (_context.Jobs == null)
            {
                return Problem("Entity set 'CompanyContext.Jobs'  is null.");
            }
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJob", new { id = job.ID }, job);
        }

        // DELETE: api/JobsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            if (_context.Jobs == null)
            {
                return NotFound();
            }
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobExists(int id)
        {
            return (_context.Jobs?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
