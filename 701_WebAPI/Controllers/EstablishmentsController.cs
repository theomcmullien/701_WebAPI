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
    public class EstablishmentsController : ControllerBase
    {
        private readonly _701_WebAPIContext _context;

        public EstablishmentsController(_701_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Establishments

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Establishment>>> GetEstablishment()
        {
            if (_context.Establishment == null) return NotFound();
            if (_context.Account == null) return NotFound();

            var establishments = await _context.Establishment.ToListAsync();

            foreach (var e in establishments)
            {
                string manager = "", email = "";
                bool addComma = false;
                foreach (var a in await _context.Account.Where(a => a.EstablishmentID == e.EstablishmentID).ToListAsync())
                {
                    if (addComma)
                    {
                        manager += ", ";
                        email += ", ";
                    }
                    manager += string.Format("{0} {1}", a.Firstname, a.Lastname);
                    email += a.Email;
                    addComma = true;
                }
                e.Manager = manager;
                e.Email = email;
            }
            return establishments;
        }

        // GET: api/Establishments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Establishment>> GetEstablishment(int id)
        {
            if (_context.Establishment == null) return NotFound();
            var establishment = await _context.Establishment.FindAsync(id);

            if (establishment == null)
            {
                return NotFound();
            }

            return establishment;
        }

        // PUT: api/Establishments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstablishment(int id, Establishment establishment)
        {
            if (id != establishment.EstablishmentID)
            {
                return BadRequest();
            }

            _context.Entry(establishment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstablishmentExists(id))
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

        // POST: api/Establishments
        [HttpPost]
        public async Task<ActionResult<Establishment>> PostEstablishment(Establishment establishment)
        {
            if (_context.Establishment == null) return BadRequest();

            establishment.Manager = null;
            establishment.Email = null;

            _context.Establishment.Add(establishment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstablishment", new { id = establishment.EstablishmentID }, establishment);
        }

        // DELETE: api/Establishments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstablishment(int id)
        {
            if (_context.Establishment == null)
            {
                return NotFound();
            }
            var establishment = await _context.Establishment.FindAsync(id);
            if (establishment == null)
            {
                return NotFound();
            }

            _context.Establishment.Remove(establishment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstablishmentExists(int id)
        {
            return (_context.Establishment?.Any(e => e.EstablishmentID == id)).GetValueOrDefault();
        }
    }
}
