﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _701_WebAPI.Data;
using _701_WebAPI.Models;
using _701_WebAPI.Models.Auth0;
using Newtonsoft.Json;
using RestSharp;
using Microsoft.AspNetCore.Authorization;
using _701_WebAPI.Controllers.Classes;

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
        [Authorize]
        public async Task<ActionResult<IEnumerable<Establishment>>> GetEstablishment()
        {
            if (_context.Establishment == null) return NotFound();
            
            var establishments = await _context.Establishment.ToListAsync();

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

                    if (a.MetaData == null || a.MetaData.Role == null || a.MetaData.Role.ToLower() != "establishment manager") continue;
                    account.Role = a.MetaData.Role;
                    account.EstablishmentID = a.MetaData.EstablishmentID;
                    accounts.Add(account);

                    //using (var client2 = new RestClient($"https://dev-bss0r74x.au.auth0.com/api/v2/users/{a.AccountID}/roles"))
                    //{
                    //    var request2 = new RestRequest();
                    //    request2.AddHeader("authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjJ1WGVxVmY0d1NWOGVKcFVMYVZYdCJ9.eyJpc3MiOiJodHRwczovL2Rldi1ic3Mwcjc0eC5hdS5hdXRoMC5jb20vIiwic3ViIjoiRTJOYXd5TWhEcW5SV0k3RVE1SldPSXlQaTd6OXpmT0ZAY2xpZW50cyIsImF1ZCI6Imh0dHBzOi8vZGV2LWJzczByNzR4LmF1LmF1dGgwLmNvbS9hcGkvdjIvIiwiaWF0IjoxNjY0MTgwMjkyLCJleHAiOjE2NjY3NzIyOTIsImF6cCI6IkUyTmF3eU1oRHFuUldJN0VRNUpXT0l5UGk3ejl6Zk9GIiwic2NvcGUiOiJyZWFkOmNsaWVudF9ncmFudHMgY3JlYXRlOmNsaWVudF9ncmFudHMgZGVsZXRlOmNsaWVudF9ncmFudHMgdXBkYXRlOmNsaWVudF9ncmFudHMgcmVhZDp1c2VycyB1cGRhdGU6dXNlcnMgZGVsZXRlOnVzZXJzIGNyZWF0ZTp1c2VycyByZWFkOnVzZXJzX2FwcF9tZXRhZGF0YSB1cGRhdGU6dXNlcnNfYXBwX21ldGFkYXRhIGRlbGV0ZTp1c2Vyc19hcHBfbWV0YWRhdGEgY3JlYXRlOnVzZXJzX2FwcF9tZXRhZGF0YSByZWFkOnVzZXJfY3VzdG9tX2Jsb2NrcyBjcmVhdGU6dXNlcl9jdXN0b21fYmxvY2tzIGRlbGV0ZTp1c2VyX2N1c3RvbV9ibG9ja3MgY3JlYXRlOnVzZXJfdGlja2V0cyByZWFkOmNsaWVudHMgdXBkYXRlOmNsaWVudHMgZGVsZXRlOmNsaWVudHMgY3JlYXRlOmNsaWVudHMgcmVhZDpjbGllbnRfa2V5cyB1cGRhdGU6Y2xpZW50X2tleXMgZGVsZXRlOmNsaWVudF9rZXlzIGNyZWF0ZTpjbGllbnRfa2V5cyByZWFkOmNvbm5lY3Rpb25zIHVwZGF0ZTpjb25uZWN0aW9ucyBkZWxldGU6Y29ubmVjdGlvbnMgY3JlYXRlOmNvbm5lY3Rpb25zIHJlYWQ6cmVzb3VyY2Vfc2VydmVycyB1cGRhdGU6cmVzb3VyY2Vfc2VydmVycyBkZWxldGU6cmVzb3VyY2Vfc2VydmVycyBjcmVhdGU6cmVzb3VyY2Vfc2VydmVycyByZWFkOmRldmljZV9jcmVkZW50aWFscyB1cGRhdGU6ZGV2aWNlX2NyZWRlbnRpYWxzIGRlbGV0ZTpkZXZpY2VfY3JlZGVudGlhbHMgY3JlYXRlOmRldmljZV9jcmVkZW50aWFscyByZWFkOnJ1bGVzIHVwZGF0ZTpydWxlcyBkZWxldGU6cnVsZXMgY3JlYXRlOnJ1bGVzIHJlYWQ6cnVsZXNfY29uZmlncyB1cGRhdGU6cnVsZXNfY29uZmlncyBkZWxldGU6cnVsZXNfY29uZmlncyByZWFkOmhvb2tzIHVwZGF0ZTpob29rcyBkZWxldGU6aG9va3MgY3JlYXRlOmhvb2tzIHJlYWQ6YWN0aW9ucyB1cGRhdGU6YWN0aW9ucyBkZWxldGU6YWN0aW9ucyBjcmVhdGU6YWN0aW9ucyByZWFkOmVtYWlsX3Byb3ZpZGVyIHVwZGF0ZTplbWFpbF9wcm92aWRlciBkZWxldGU6ZW1haWxfcHJvdmlkZXIgY3JlYXRlOmVtYWlsX3Byb3ZpZGVyIGJsYWNrbGlzdDp0b2tlbnMgcmVhZDpzdGF0cyByZWFkOmluc2lnaHRzIHJlYWQ6dGVuYW50X3NldHRpbmdzIHVwZGF0ZTp0ZW5hbnRfc2V0dGluZ3MgcmVhZDpsb2dzIHJlYWQ6bG9nc191c2VycyByZWFkOnNoaWVsZHMgY3JlYXRlOnNoaWVsZHMgdXBkYXRlOnNoaWVsZHMgZGVsZXRlOnNoaWVsZHMgcmVhZDphbm9tYWx5X2Jsb2NrcyBkZWxldGU6YW5vbWFseV9ibG9ja3MgdXBkYXRlOnRyaWdnZXJzIHJlYWQ6dHJpZ2dlcnMgcmVhZDpncmFudHMgZGVsZXRlOmdyYW50cyByZWFkOmd1YXJkaWFuX2ZhY3RvcnMgdXBkYXRlOmd1YXJkaWFuX2ZhY3RvcnMgcmVhZDpndWFyZGlhbl9lbnJvbGxtZW50cyBkZWxldGU6Z3VhcmRpYW5fZW5yb2xsbWVudHMgY3JlYXRlOmd1YXJkaWFuX2Vucm9sbG1lbnRfdGlja2V0cyByZWFkOnVzZXJfaWRwX3Rva2VucyBjcmVhdGU6cGFzc3dvcmRzX2NoZWNraW5nX2pvYiBkZWxldGU6cGFzc3dvcmRzX2NoZWNraW5nX2pvYiByZWFkOmN1c3RvbV9kb21haW5zIGRlbGV0ZTpjdXN0b21fZG9tYWlucyBjcmVhdGU6Y3VzdG9tX2RvbWFpbnMgdXBkYXRlOmN1c3RvbV9kb21haW5zIHJlYWQ6ZW1haWxfdGVtcGxhdGVzIGNyZWF0ZTplbWFpbF90ZW1wbGF0ZXMgdXBkYXRlOmVtYWlsX3RlbXBsYXRlcyByZWFkOm1mYV9wb2xpY2llcyB1cGRhdGU6bWZhX3BvbGljaWVzIHJlYWQ6cm9sZXMgY3JlYXRlOnJvbGVzIGRlbGV0ZTpyb2xlcyB1cGRhdGU6cm9sZXMgcmVhZDpwcm9tcHRzIHVwZGF0ZTpwcm9tcHRzIHJlYWQ6YnJhbmRpbmcgdXBkYXRlOmJyYW5kaW5nIGRlbGV0ZTpicmFuZGluZyByZWFkOmxvZ19zdHJlYW1zIGNyZWF0ZTpsb2dfc3RyZWFtcyBkZWxldGU6bG9nX3N0cmVhbXMgdXBkYXRlOmxvZ19zdHJlYW1zIGNyZWF0ZTpzaWduaW5nX2tleXMgcmVhZDpzaWduaW5nX2tleXMgdXBkYXRlOnNpZ25pbmdfa2V5cyByZWFkOmxpbWl0cyB1cGRhdGU6bGltaXRzIGNyZWF0ZTpyb2xlX21lbWJlcnMgcmVhZDpyb2xlX21lbWJlcnMgZGVsZXRlOnJvbGVfbWVtYmVycyByZWFkOmVudGl0bGVtZW50cyByZWFkOmF0dGFja19wcm90ZWN0aW9uIHVwZGF0ZTphdHRhY2tfcHJvdGVjdGlvbiByZWFkOm9yZ2FuaXphdGlvbnNfc3VtbWFyeSByZWFkOm9yZ2FuaXphdGlvbnMgdXBkYXRlOm9yZ2FuaXphdGlvbnMgY3JlYXRlOm9yZ2FuaXphdGlvbnMgZGVsZXRlOm9yZ2FuaXphdGlvbnMgY3JlYXRlOm9yZ2FuaXphdGlvbl9tZW1iZXJzIHJlYWQ6b3JnYW5pemF0aW9uX21lbWJlcnMgZGVsZXRlOm9yZ2FuaXphdGlvbl9tZW1iZXJzIGNyZWF0ZTpvcmdhbml6YXRpb25fY29ubmVjdGlvbnMgcmVhZDpvcmdhbml6YXRpb25fY29ubmVjdGlvbnMgdXBkYXRlOm9yZ2FuaXphdGlvbl9jb25uZWN0aW9ucyBkZWxldGU6b3JnYW5pemF0aW9uX2Nvbm5lY3Rpb25zIGNyZWF0ZTpvcmdhbml6YXRpb25fbWVtYmVyX3JvbGVzIHJlYWQ6b3JnYW5pemF0aW9uX21lbWJlcl9yb2xlcyBkZWxldGU6b3JnYW5pemF0aW9uX21lbWJlcl9yb2xlcyBjcmVhdGU6b3JnYW5pemF0aW9uX2ludml0YXRpb25zIHJlYWQ6b3JnYW5pemF0aW9uX2ludml0YXRpb25zIGRlbGV0ZTpvcmdhbml6YXRpb25faW52aXRhdGlvbnMiLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMifQ.TdcttVCyVgQvjNDatGRIBRC6Z8twt6ixjF-46V2KHrN0BhVf6zpqLj9ri4YldxN-lC_2Vgiidx6nJiBxIzNG2F5fMMp9To4H_KwoqCesO-TEaeOSuCB4DaA7E9yTAYq4rY-7FslKbHjbczq66t0Yw8ht5yo-HL1S9kMGqfFsg8RdxGBKWj-GAB362S3CrgKB5mKR7-DcRxRSRjd78E_EOs9o7MzNrWqlMmiJCOO9ZNB3YzadhBp8A1ofxtgH6HdY_l6tgNNoIGMxC3GFHfmOeXONofichx4rsRIhnf4jSRQT_0fUxqY7TqfA8QFpriTzKif_QJDPEW66YpGbt1nLmA");
                    //    var result2 = await client2.GetAsync(request2);
                    //    string json2 = result2.Content.ToString();
                    //    AccountRole[] accountRole = JsonConvert.DeserializeObject<AccountRole[]>(json2);
                    //    if (accountRole.Length < 1) continue;
                    //    string role = accountRole.First().ToString();
                    //    if (role.ToLower() != "establishment manager" || a.MetaData == null) continue;
                    //    account.Role = role;
                    //    account.EstablishmentID = a.MetaData.EstablishmentID;
                    //}
                    //accounts.Add(account);
                }
            }

            foreach (var e in establishments)
            {
                string manager = "", email = "";
                bool addComma = false;
                foreach (var a in accounts.Where(a => a.EstablishmentID == e.EstablishmentID).ToList())
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
