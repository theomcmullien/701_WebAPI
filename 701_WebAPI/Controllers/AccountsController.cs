using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _701_WebAPI.Data;
using _701_WebAPI.Models;
using RestSharp;
using Newtonsoft.Json.Linq;
using _701_WebAPI.Models.Auth0;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using _701_WebAPI.Controllers.Classes;

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
        [Authorize]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccount()
        {
            List<Account> accounts = new List<Account>();

            using (var client = new RestClient("https://dev-bss0r74x.au.auth0.com/api/v2/users"))
            {
                var request = new RestRequest();
                request.AddHeader("authorization", await AccessToken.GetToken());

                var result = await client.GetAsync(request);
                string json = result.Content.ToString();
                AccountAuth0[] accountsArray = JsonConvert.DeserializeObject<AccountAuth0[]>(json);

                foreach (AccountAuth0 a in accountsArray)
                {
                    string[] username = a.Username.Split(".");
                    if (username.Length < 2) continue;

                    Account account = new Account()
                    {
                        AccountID = a.AccountID,
                        Email = a.Email,
                        Firstname = username[0],
                        Lastname = username[1]
                    };

                    if (a.MetaData == null || a.MetaData.Role == null || a.MetaData.Role == "" || a.MetaData.Role == "ADD_ROLE_HERE")
                    {
                        accounts.Add(account);
                        continue;
                    }

                    account.Role = a.MetaData.Role;

                    if (a.MetaData.Role.ToLower() == "employee")
                    {
                        account.Rate = a.MetaData.Rate;
                        account.RateOT = a.MetaData.RateOT;
                        account.TradeID = a.MetaData.TradeID;
                    }
                    else if (a.MetaData.Role.ToLower() == "establishment manager")
                    {
                        account.EstablishmentID = a.MetaData.EstablishmentID;
                    }

                    accounts.Add(account);

                    //using (var client2 = new RestClient($"https://dev-bss0r74x.au.auth0.com/api/v2/users/{a.AccountID}/roles"))
                    //{

                    //    var request2 = new RestRequest();
                    //    request2.AddHeader("authorization", "");
                    //    var result2 = await client2.GetAsync(request2);
                    //    string json2 = result2.Content.ToString();

                    //    AccountRole[] accountRole = JsonConvert.DeserializeObject<AccountRole[]>(json2);
                    //    if (accountRole.Length < 1)
                    //    {
                    //        accounts.Add(account);
                    //        continue;
                    //    }

                    //    string role = accountRole.First().ToString();
                    //    account.Role = role;

                    //    if (role.ToLower() == "employee")
                    //    {
                    //        account.Rate = a.MetaData.Rate;
                    //        account.RateOT = a.MetaData.RateOT;
                    //        account.TradeID = a.MetaData.TradeID;
                    //    }
                    //    else if (role.ToLower() == "establishment manager" && a.MetaData != null)
                    //    {
                    //        account.EstablishmentID = a.MetaData.EstablishmentID;
                    //    }
                    //}
                    //accounts.Add(account);
                }
            }

            if (accounts == null) return NotFound();

            return accounts;
        }

        // GET: api/Accounts/5
        [HttpGet("{accountID}")]
        [Authorize]
        public async Task<ActionResult<Account>> GetAccount(string accountID)
        {
            Account account;
            using (var client = new RestClient($"https://dev-bss0r74x.au.auth0.com/api/v2/users/{accountID}"))
            {
                var request = new RestRequest();
                request.AddHeader("authorization", await AccessToken.GetToken());
                var result = await client.GetAsync(request);
                string json = result.Content.ToString();
                AccountAuth0 a = JsonConvert.DeserializeObject<AccountAuth0>(json);

                string[] username = a.Username.Split(".");
                if (username.Length < 2) return NotFound();

                account = new Account()
                {
                    AccountID = a.AccountID,
                    Email = a.Email,
                    Firstname = username[0],
                    Lastname = username[1]
                };

                if (a.MetaData == null || a.MetaData.Role == null || a.MetaData.Role == "" || a.MetaData.Role == "ADD_ROLE_HERE") return account;

                if (a.MetaData.Role.ToLower() == "employee")
                {
                    account.Rate = a.MetaData.Rate;
                    account.RateOT = a.MetaData.RateOT;
                    account.TradeID = a.MetaData.TradeID;
                }
                else if (a.MetaData.Role.ToLower() == "establishment manager")
                {
                    account.EstablishmentID = a.MetaData.EstablishmentID;
                }

                //using (var client2 = new RestClient($"https://dev-bss0r74x.au.auth0.com/api/v2/users/{accountID}/roles"))
                //{

                //    var request2 = new RestRequest();
                //    request2.AddHeader("authorization", "");
                //    var result2 = await client2.GetAsync(request2);
                //    string json2 = result2.Content.ToString();

                //    AccountRole[] accountRole = JsonConvert.DeserializeObject<AccountRole[]>(json2);
                //    if (accountRole.Length < 1) return account;

                //    string role = accountRole.First().ToString();
                //    account.Role = role;

                //    if (role.ToLower() == "employee")
                //    {
                //        account.Rate = a.MetaData.Rate;
                //        account.RateOT = a.MetaData.RateOT;
                //        account.TradeID = a.MetaData.TradeID;
                //    }
                //    else if (role.ToLower() == "establishment manager" && a.MetaData != null)
                //    {
                //        account.EstablishmentID = a.MetaData.EstablishmentID;
                //    }
                //}
            }

            if (account == null) return NotFound();

            return account;
        }

        //// PUT: api/Accounts/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAccount(string id, Account account)
        //{
        //    if (id != account.AccountID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(account).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AccountExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Accounts
        //[HttpPost]
        //public async Task<ActionResult<Account>> PostAccount(Account account)
        //{
        //    if (_context.Account == null || account == null) return BadRequest();

        //    account.JobSheets = null;

        //    if (account.Role != "Employee")
        //    {
        //        account.Rate = null;
        //        account.RateOT = null;
        //        account.TradeID = null;
        //    }
        //    if (account.Role != "Establishment Manager")
        //    {
        //        account.EstablishmentID = null;
        //    }

        //    _context.Account.Add(account);
        //    await _context.SaveChangesAsync();

        //    return Ok(account);
        //}

        //// DELETE: api/Accounts/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAccount(int id)
        //{
        //    if (_context.Account == null)
        //    {
        //        return NotFound();
        //    }
        //    var account = await _context.Account.FindAsync(id);
        //    if (account == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Account.Remove(account);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool AccountExists(string id)
        //{
        //    return (_context.Account?.Any(e => e.AccountID == id)).GetValueOrDefault();
        //}
    }
}
