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
    public class TradesController : ControllerBase
    {
        private readonly _701_WebAPIContext _context;

        public TradesController(_701_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Trades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trade>>> GetTrade()
        {
            if (_context.Trade == null) return NotFound();
            return await _context.Trade.ToListAsync();
        }

        // GET: api/Trades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trade>> GetTrade(int id)
        {
            if (_context.Trade == null) return NotFound();
            var trade = await _context.Trade.FindAsync(id);

            if (trade == null)
            {
                return NotFound();
            }

            return trade;
        }

        // PUT: api/Trades/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrade(int id, Trade trade)
        {
            if (id != trade.TradeID)
            {
                return BadRequest();
            }

            _context.Entry(trade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TradeExists(id))
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

        // POST: api/Trades
        [HttpPost]
        public async Task<ActionResult<Trade>> PostTrade(Trade trade)
        {
            if (_context.Trade == null) return Problem("Entity set '_701_WebAPIContext.Trade'  is null.");
            _context.Trade.Add(trade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrade", new { id = trade.TradeID }, trade);
        }

        // DELETE: api/Trades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrade(int id)
        {
            if (_context.Trade == null)
            {
                return NotFound();
            }
            var trade = await _context.Trade.FindAsync(id);
            if (trade == null)
            {
                return NotFound();
            }

            _context.Trade.Remove(trade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TradeExists(int id)
        {
            return (_context.Trade?.Any(e => e.TradeID == id)).GetValueOrDefault();
        }
    }
}
