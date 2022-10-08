using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _701_WebAPI.Data;
using _701_WebAPI.Models;
using _701_WebAPI.Models.Auth0;
using _701_WebAPI.Models.DTO;

namespace _701_WebAPI.Controllers.ReportControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Temp : ControllerBase
    {
        private readonly _701_WebAPIContext _context;

        public Temp(_701_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/TransactionsHOs
        [HttpGet]
        public async Task<ActionResult> GetTransactionsHO()
        {
            return File(Url.Content("~/Data/ExcelReports/text.xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
