using _701_WebAPI.Data;
using _701_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _701_WebAPI.Controllers.GetControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentJobController : ControllerBase
    {
        private readonly _701_WebAPIContext _context;

        public CurrentJobController(_701_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Jobs/5
        [HttpGet("{accountID}")]
        public async Task<ActionResult<Job>> GetJob(string accountID)
        {
            if (_context.Job == null) return NotFound();

            Job job;

            try
            {
                job = await _context.Job.Where(j => !j.IsCompleted && j.AccountID == accountID).FirstAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }

            if (job == null) return NotFound();

            return job;
        }
    }
}
