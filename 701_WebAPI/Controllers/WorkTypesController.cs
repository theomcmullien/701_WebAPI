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
    public class WorkTypesController : ControllerBase
    {
        private readonly _701_WebAPIContext _context;

        public WorkTypesController(_701_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/WorkTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkType>>> GetWorkType()
        {
            if (_context.WorkType == null) return NotFound();
            return await _context.WorkType.ToListAsync();
        }

        // GET: api/WorkTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkType>> GetWorkType(int id)
        {
            if (_context.WorkType == null) return NotFound();
            var workType = await _context.WorkType.FindAsync(id);

            if (workType == null)
            {
                return NotFound();
            }

            return workType;
        }

        // PUT: api/WorkTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkType(int id, WorkType workType)
        {
            if (id != workType.WorkTypeID)
            {
                return BadRequest();
            }

            _context.Entry(workType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkTypeExists(id))
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

        // POST: api/WorkTypes
        [HttpPost]
        public async Task<ActionResult<WorkType>> PostWorkType(WorkType workType)
        {
            if (_context.WorkType == null) return Problem("Entity set '_701_WebAPIContext.WorkType'  is null.");
            _context.WorkType.Add(workType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkType", new { id = workType.WorkTypeID }, workType);
        }

        // DELETE: api/WorkTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkType(int id)
        {
            if (_context.WorkType == null)
            {
                return NotFound();
            }
            var workType = await _context.WorkType.FindAsync(id);
            if (workType == null)
            {
                return NotFound();
            }

            _context.WorkType.Remove(workType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkTypeExists(int id)
        {
            return (_context.WorkType?.Any(e => e.WorkTypeID == id)).GetValueOrDefault();
        }
    }
}
