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
    public class ChargeCodesController : ControllerBase
    {
        private readonly _701_WebAPIContext _context;

        public ChargeCodesController(_701_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/ChargeCodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChargeCode>>> GetChargeCode()
        {
            if (_context.ChargeCode == null) return NotFound();
            return await _context.ChargeCode.ToListAsync();
        }

        // GET: api/ChargeCodes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChargeCode>> GetChargeCode(int id)
        {
            if (_context.ChargeCode == null) return NotFound();
            var chargeCode = await _context.ChargeCode.FindAsync(id);

            if (chargeCode == null)
            {
                return NotFound();
            }

            return chargeCode;
        }

        // PUT: api/ChargeCodes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChargeCode(int id, ChargeCode chargeCode)
        {
            if (id != chargeCode.ChargeCodeID)
            {
                return BadRequest();
            }

            _context.Entry(chargeCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChargeCodeExists(id))
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

        // POST: api/ChargeCodes
        [HttpPost]
        public async Task<ActionResult<ChargeCode>> PostChargeCode(ChargeCode chargeCode)
        {
            if (_context.ChargeCode == null) return Problem("Entity set '_701_WebAPIContext.ChargeCode'  is null.");
            _context.ChargeCode.Add(chargeCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChargeCode", new { id = chargeCode.ChargeCodeID }, chargeCode);
        }

        // DELETE: api/ChargeCodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChargeCode(int id)
        {
            if (_context.ChargeCode == null)
            {
                return NotFound();
            }
            var chargeCode = await _context.ChargeCode.FindAsync(id);
            if (chargeCode == null)
            {
                return NotFound();
            }

            _context.ChargeCode.Remove(chargeCode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChargeCodeExists(int id)
        {
            return (_context.ChargeCode?.Any(e => e.ChargeCodeID == id)).GetValueOrDefault();
        }
    }
}
