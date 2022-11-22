using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sfl.Data;
using sfl.Models;

namespace sfl.Controllers
{
    public class JobsController : Controller
    {
        private readonly CompanyContext _context;
        private readonly string[] _navigationProperties;

        public JobsController(CompanyContext context)
        {
            _context = context;
            _navigationProperties = new string[] { "JobsParcels", "Staff", "JobType", "JobStatus", "JobsParcels" };
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            return _context.Jobs != null ?
                        View(await _context.Jobs.ToListAsync()) :
                        Problem("Entity set 'CompanyContext.Jobs'  is null.");
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            ViewData["Parcels"] = new SelectList(_context.Parcels, "ID", "ID");
            ViewData["StaffUsername"] = new SelectList(_context.Staff, "Username", "Username");
            ViewData["JobTypeID"] = new SelectList(_context.JobTypes, "ID", "Name");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StaffUsername,JobTypeID,ParcelIDs")] Job job)
        {
            var ids = Request.Form["ParcelIDs"].ToList();
            RemoveNavigationProperties();
            if (ModelState.IsValid)
            {
                job.DateCreated = DateTime.Now;
                job.JobStatusID = _context.JobStatuses.Select(j => j.ID).Where(j => j == 0).ToList()[0];
                // First create job.
                _context.Add(job);
                await _context.SaveChangesAsync();

                // Create JobParcel records from selected parcels and current job.
                var parcels = new List<JobParcel>();
                foreach (var pID in Request.Form["ParcelIDs"].ToList())
                {
                    parcels.Add(new JobParcel { ParcelID = pID, JobID = job.ID });
                }

                // Link jobs and selected parcels in table JobParcel.
                _context.JobsParcels.AddRange(parcels);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["Parcels"] = new SelectList(_context.Parcels, "ID", "ID");
            ViewData["StaffUsername"] = new SelectList(_context.Staff, "Username", "Username");
            ViewData["JobTypeID"] = new SelectList(_context.JobTypes, "ID", "Name");
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            ViewData[index: "JobStatusID"] = new SelectList(_context.JobStatuses, "ID", "Name");

            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DateCreated,DateCompleted,StaffUsername")] Job job)
        {
            if (id != job.ID)
            {
                return NotFound();
            }

            if (job.DateCompleted < job.DateCreated)
            {
                ViewData[index: "JobStatusID"] = new SelectList(_context.JobStatuses, "ID", "Name");
                return View(job);
            }

            RemoveNavigationProperties();
            ModelState.Remove("StaffUsername");
            if (ModelState.IsValid)
            {
                Console.WriteLine("Editing job.");
                try
                {
                    job.StaffUsername = _context.Jobs.Select(j => new { j.StaffUsername, j.ID })
                        .Where(j => j.ID == job.ID).ToList()[0].StaffUsername;
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Job updated.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData[index: "JobStatusID"] = new SelectList(_context.JobStatuses, "ID", "Name");
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }
            var job = await _context.Jobs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Jobs == null)
            {
                return Problem("Entity set 'CompanyContext.Jobs'  is null.");
            }
            var job = await _context.Jobs.FindAsync(id);
            if (job != null)
            {
                _context.Jobs.Remove(job);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return (_context.Jobs?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        private void RemoveNavigationProperties()
        {
            foreach (var key in _navigationProperties)
            {
                ModelState.Remove(key);
            }
        }
    }
}
