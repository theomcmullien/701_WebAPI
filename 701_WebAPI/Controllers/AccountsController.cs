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
    public class AccountsController : ControllerBase
    {
        private readonly _701_WebAPIContext _context;

        public AccountsController(_701_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccount()
        {
            if (_context.Account == null) return NotFound();
            return await _context.Account.ToListAsync();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            if (_context.Account == null) return NotFound();
            var account = await _context.Account.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account account)
        {
            if (id != account.AccountID)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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

        // POST: api/Accounts
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            if (_context.Account == null || account == null) return BadRequest();

            account.JobSheets = null;

            if (account.Role != "Employee")
            {
                account.Rate = null;
                account.RateOT = null;
                account.TradeID = null;
            }
            if (account.Role != "Establishment Manager")
            {
                account.EstablishmentID = null;
            }

            _context.Account.Add(account);
            await _context.SaveChangesAsync();

            return Ok(account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            if (_context.Account == null)
            {
                return NotFound();
            }
            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Account.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return (_context.Account?.Any(e => e.AccountID == id)).GetValueOrDefault();
        }
    }
}
