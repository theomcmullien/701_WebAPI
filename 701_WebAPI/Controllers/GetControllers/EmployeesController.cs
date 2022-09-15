using _701_WebAPI.Data;
using _701_WebAPI.Models;
using _701_WebAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _701_WebAPI.Controllers.GetControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly _701_WebAPIContext _context;

        public EmployeesController(_701_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetEmployees()
        {
            if (_context.Account == null) return NotFound();
            if (_context.Job == null) return NotFound();

            string fmt = "MM/dd/yyyy HH:mm";

            var accounts = await _context.Account.Where(a => a.Role == "Employee").ToListAsync();
            foreach (Account a in accounts)
            {
                var jobs = await _context.Job.Where(j => j.AccountID == a.AccountID && j.IsCompleted).ToListAsync();

                List<List<Job>> llJobs = new List<List<Job>>();

                foreach (Job j in jobs)
                {
                    bool inList = false;
                    foreach (var jList in llJobs)
                    {
                        if (DateTime.ParseExact(jList[0].StartTime, fmt, null).ToShortDateString() == DateTime.ParseExact(j.StartTime, fmt, null).ToShortDateString())
                        {
                            jList.Add(j);
                            inList = true;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        var newList = new List<Job>();
                        newList.Add(j);
                        llJobs.Add(newList);
                    }
                }

                if (llJobs.Count() > 0) a.JobSheets = new List<JobSheet>();

                foreach (var jList in llJobs)
                {
                    string date = DateTime.ParseExact(jList[0].StartTime, fmt, null).ToShortDateString();
                    JobSheet jobSheet = new JobSheet()
                    {
                        Filename = string.Format("Job Sheet - {0} {1} - {2}", a.Firstname, a.Lastname, date),
                        DateCompleted = date,
                        Jobs = jList
                    };
                    a.JobSheets.Add(jobSheet);
                }

            }

            return accounts;
        }

    }
}
