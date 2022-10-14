using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _701_WebAPI.Data;
using _701_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace _701_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstablishmentCodesController : ControllerBase
    {
        private readonly _701_WebAPIContext _context;

        public EstablishmentCodesController(_701_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/EstablishmentCodes
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EstablishmentCode>>> GetEstablishmentCode()
        {
            if (_context.EstablishmentCode == null) return NotFound();
            return await _context.EstablishmentCode.ToListAsync();
        }

        // GET: api/EstablishmentCodes/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<EstablishmentCode>> GetEstablishmentCode(int id)
        {
            if (_context.EstablishmentCode == null) return NotFound();
            var establishmentCode = await _context.EstablishmentCode.FindAsync(id);

            if (establishmentCode == null)
            {
                return NotFound();
            }

            return establishmentCode;
        }

        // PUT: api/EstablishmentCodes/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutEstablishmentCode(int id, EstablishmentCode establishmentCode)
        {
            if (id != establishmentCode.EstablishmentCodeID)
            {
                return BadRequest();
            }

            _context.Entry(establishmentCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstablishmentCodeExists(id))
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

        // POST: api/EstablishmentCodes
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<EstablishmentCode>> PostEstablishmentCode(EstablishmentCode establishmentCode)
        {
            if (_context.EstablishmentCode == null) return NotFound();
            _context.EstablishmentCode.Add(establishmentCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstablishmentCode", new { id = establishmentCode.EstablishmentCodeID }, establishmentCode);
        }

        // DELETE: api/EstablishmentCodes/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteEstablishmentCode(int id)
        {
            if (_context.EstablishmentCode == null)
            {
                return NotFound();
            }
            var establishmentCode = await _context.EstablishmentCode.FindAsync(id);
            if (establishmentCode == null)
            {
                return NotFound();
            }

            _context.EstablishmentCode.Remove(establishmentCode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstablishmentCodeExists(int id)
        {
            return (_context.EstablishmentCode?.Any(e => e.EstablishmentCodeID == id)).GetValueOrDefault();
        }
    }
}
