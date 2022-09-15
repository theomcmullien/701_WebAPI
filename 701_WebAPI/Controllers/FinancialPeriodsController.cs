using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _701_WebAPI.Data;
using _701_WebAPI.Models;

namespace _701_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialPeriodsController : ControllerBase
    {
        private readonly _701_WebAPIContext _context;

        public FinancialPeriodsController(_701_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/FinancialPeriods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinancialPeriod>>> GetFinancialPeriod()
        {
            if (_context.FinancialPeriod == null) return NotFound();
            return await _context.FinancialPeriod.ToListAsync();
        }

        // GET: api/FinancialPeriods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FinancialPeriod>> GetFinancialPeriod(int id)
        {
            if (_context.FinancialPeriod == null) return NotFound();
            var financialPeriod = await _context.FinancialPeriod.FindAsync(id);

            if (financialPeriod == null)
            {
                return NotFound();
            }

            return financialPeriod;
        }

        // PUT: api/FinancialPeriods/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFinancialPeriod(int id, FinancialPeriod financialPeriod)
        {
            if (id != financialPeriod.FinancialPeriodID)
            {
                return BadRequest();
            }

            _context.Entry(financialPeriod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinancialPeriodExists(id))
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

        // POST: api/FinancialPeriods
        [HttpPost]
        public async Task<ActionResult<FinancialPeriod>> PostFinancialPeriod(FinancialPeriod financialPeriod)
        {
            if (_context.FinancialPeriod == null) return Problem("Entity set '_701_WebAPIContext.FinancialPeriod'  is null.");
            _context.FinancialPeriod.Add(financialPeriod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFinancialPeriod", new { id = financialPeriod.FinancialPeriodID }, financialPeriod);
        }

        // DELETE: api/FinancialPeriods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinancialPeriod(int id)
        {
            if (_context.FinancialPeriod == null)
            {
                return NotFound();
            }
            var financialPeriod = await _context.FinancialPeriod.FindAsync(id);
            if (financialPeriod == null)
            {
                return NotFound();
            }

            _context.FinancialPeriod.Remove(financialPeriod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FinancialPeriodExists(int id)
        {
            return (_context.FinancialPeriod?.Any(e => e.FinancialPeriodID == id)).GetValueOrDefault();
        }
    }
}
