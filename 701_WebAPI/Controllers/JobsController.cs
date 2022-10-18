using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _701_WebAPI.Data;
using _701_WebAPI.Models;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace _701_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly _701_WebAPIContext _context;

        public JobsController(_701_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Jobs
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Job>>> GetJob()
        {
            if (_context.Job == null) return NotFound();
            return await _context.Job.ToListAsync();
        }

        // GET: api/Jobs/5
        [HttpGet("{accountID}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Job>>> GetJob(string accountID)
        {
            if (_context.Job == null) return NotFound();

            List<Job> job;

            try
            {
                job = await _context.Job.Where(j => j.AccountID == accountID && j.IsCompleted).ToListAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }
            
            if (job == null) return NotFound();

            return job;
        }

        // PUT: api/Jobs/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutJob(int id, Job job)
        {
            if (id != job.JobID)
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

        // POST: api/Jobs
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Job>> PostJob(Job job)
        {
            if (_context.Job == null || job == null) return BadRequest();

            job.StartTime = job.StartTime.Replace("-", "/");
            job.FinishTime = job.FinishTime.Replace("-", "/");

            if (job.IsCompleted)
            {
                if (_context.FinancialPeriod == null) return BadRequest();

                if (job.StartTime != null && job.FinishTime != null)
                {
                    var fList = await _context.FinancialPeriod.ToListAsync();
                    string fmt = "dd/MM/yyyy HH:mm";
                    try
                    {
                        job.FinancialPeriodID = fList.Where(f => DateTime.ParseExact(f.StartDate, fmt, null) < DateTime.ParseExact(job.StartTime, fmt, null) && DateTime.ParseExact(f.EndDate, fmt, null) > DateTime.ParseExact(job.StartTime, fmt, null)).Select(f => f.FinancialPeriodID).First();
                    }
                    catch (Exception)
                    {
                        job.FinancialPeriodID = null;
                    }
                }
                else return BadRequest();

                try
                {
                    job.JobID = await _context.Job.Where(j => !j.IsCompleted && j.AccountID == job.AccountID).Select(j => j.JobID).FirstAsync();
                    await PutJob(job.JobID, job);
                }
                catch (Exception)
                {
                    _context.Job.Add(job);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                var jList = await _context.Job.Where(j => j.AccountID == job.AccountID && !j.IsCompleted).ToListAsync();
                foreach (var j in jList) await DeleteJob(j.JobID);

                job.FinishTime = null;
                job.Hours = null;
                job.HoursOT = null;
                job.Notes = null;
                job.FinancialPeriodID = null;

                _context.Job.Add(job);
                await _context.SaveChangesAsync();
            }

            return Ok(job);
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteJob(int id)
        {
            if (_context.Job == null)
            {
                return NotFound();
            }
            var job = await _context.Job.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Job.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobExists(int id)
        {
            return (_context.Job?.Any(e => e.JobID == id)).GetValueOrDefault();
        }
    }
}
