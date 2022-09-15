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
    public class EmployeeCurrentController : ControllerBase
    {
        private readonly _701_WebAPIContext _context;

        public EmployeeCurrentController(_701_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeCurrent>>> GetEmployeeCurrents()
        {
            if (_context.Account == null) return NotFound();
            if (_context.Job == null) return NotFound();
            if (_context.Establishment == null) return NotFound();

            string fmt = "MM/dd/yyyy HH:mm";
            List<EmployeeCurrent> employeeCurrentList = new List<EmployeeCurrent>();
            var accounts = await _context.Account.Where(a => a.Role == "Employee").ToListAsync();
            
            foreach (var a in accounts)
            {
                
                try
                {
                    var jobs = await _context.Job.Where(j => !j.IsCompleted && j.AccountID == a.AccountID).FirstAsync();
                    string? establishment = await _context.Establishment.Where(e => e.EstablishmentID == jobs.EstablishmentID).Select(e => e.Name).FirstAsync();
                    DateTime datetime = DateTime.ParseExact(jobs.StartTime, fmt, null);
                    var ec = new EmployeeCurrent()
                    {
                        Firstname = a.Firstname,
                        Lastname = a.Lastname,
                        Location = establishment,
                        StartTime = datetime.ToShortTimeString()
                    };
                    employeeCurrentList.Add(ec);
                }
                catch (Exception)
                {
                    continue;
                }

            }
            return employeeCurrentList;
        }
    }
}
