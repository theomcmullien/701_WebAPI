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
using _701_WebAPI.Models.Excel;
using _701_WebAPI.Models.DTO.Reports;

using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using Newtonsoft.Json;
using RestSharp;
using Microsoft.AspNetCore.Authorization;
using _701_WebAPI.Controllers.Classes;

namespace _701_WebAPI.Controllers.ReportControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsHOsController : ControllerBase
    {
        private readonly _701_WebAPIContext _context;

        //static ICellStyle currencyStyle;
        static string fmt = "dd/MM/yyyy HH:mm";

        public TransactionsHOsController(_701_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/TransactionsHOs
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TransactionsHO>>> GetTransactionsHO()
        {
            if (_context.Job == null) return NotFound();
            if (_context.FinancialPeriod == null) return NotFound();

            var fList = await _context.FinancialPeriod.ToListAsync();

            List<TransactionsHO> reports = new List<TransactionsHO>();
            HashSet<FinancialPeriod> financialPeriods = new HashSet<FinancialPeriod>();

            foreach (Job j in await _context.Job.ToListAsync())
            {
                if (j.StartTime == null) continue;
                DateTime dt = DateTime.ParseExact(j.StartTime, fmt, null);
                try
                {
                    financialPeriods.Add(fList.Where(f => DateTime.ParseExact(f.StartDate, fmt, null) < dt && DateTime.ParseExact(f.EndDate, fmt, null) > dt).First());
                }
                catch (Exception)
                {
                    continue;
                }
            }
            
            foreach (var f in financialPeriods)
            {
                reports.Add(new TransactionsHO()
                {
                    FinancialPeriodID = f.FinancialPeriodID,
                    Filename = $"Transactions HO Export - {f.Month} {f.Year}"
                });
            }
            return reports;
        }

        // GET: api/TransactionsHOs/5
        [HttpGet("{financialPeriodID}")]
        [Authorize]
        public async Task<ActionResult> GetTransactionsHO(int financialPeriodID)
        {
            if (_context.Job == null) return NotFound();
            if (_context.FinancialPeriod == null) return NotFound();
            var jobsList = await _context.Job.Where(j => j.IsCompleted).ToListAsync();

            var financialPeriod = await _context.FinancialPeriod.FindAsync(financialPeriodID);
            if (financialPeriod == null) return NotFound();

            string filepath = $"Data/ExcelReports/TransactionsHOExport/TransactionsHOExport_{financialPeriod.Month}_{financialPeriod.Year}.xlsx";

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
                    if (a.MetaData == null || a.MetaData.Role == null || a.MetaData.Role.ToLower() != "employee") continue;
                    account.Role = a.MetaData.Role;
                    account.Rate = a.MetaData.Rate;
                    account.RateOT = a.MetaData.RateOT;
                    account.TradeID = a.MetaData.TradeID;
                    accounts.Add(account);
                }
            }

            var rowList = new List<TransactionsHORow>();

            foreach (Account a in accounts)
            {
                List<Job> jobs = jobsList.Where(j => j.AccountID == a.AccountID && DateTime.ParseExact(j.StartTime, fmt, null) > DateTime.ParseExact(financialPeriod.StartDate, fmt, null) && DateTime.ParseExact(j.StartTime, fmt, null) < DateTime.ParseExact(financialPeriod.EndDate, fmt, null)).ToList();
                List<List<Job>> llJobs = new List<List<Job>>();
                foreach (Job j in jobs)
                {
                    bool inList = false;
                    foreach (var jList in llJobs)
                    {
                        if (DateTime.ParseExact(jList[0].StartTime, fmt, null).ToShortDateString() == DateTime.ParseExact(j.StartTime, fmt, null).ToShortDateString())
                        {
                            jList.Add(j);
                            inList = true;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        var newList = new List<Job>();
                        newList.Add(j);
                        llJobs.Add(newList);
                    }
                }
                foreach (var jList in llJobs)
                {
                    TransactionsHORow t = new TransactionsHORow()
                    {
                        Date = DateTime.ParseExact(jList[0].StartTime, fmt, null).ToShortDateString(),
                        Hours = 0,
                        HoursOT = 0,
                        Lastname = a.Lastname,
                        Amount = 0
                    };
                    foreach (Job j in jList)
                    {
                        t.Hours += j.Hours;
                        t.HoursOT += j.HoursOT;
                    }
                    if (a.Rate == null) a.Rate = 0;
                    if (a.RateOT == null) a.RateOT = 0;
                    t.Amount = (decimal)t.Hours * (decimal)a.Rate + (decimal)t.HoursOT * (decimal)a.RateOT;

                    rowList.Add(t);
                }
            }

            IWorkbook wb = new XSSFWorkbook();
            ISheet sheet = wb.CreateSheet();

            //style
            //currencyStyle = sheet.Workbook.CreateCellStyle();
            //currencyStyle.DataFormat = sheet.Workbook.CreateDataFormat().GetFormat("$#,##0.00");

            ICellStyle style = wb.CreateCellStyle();
            IFont f1 = wb.CreateFont();
            f1.FontName = "Century Gothic";
            f1.FontHeightInPoints = 12;
            style.SetFont(f1);

            ICellStyle styleBold = wb.CreateCellStyle();
            IFont f2 = wb.CreateFont();
            f2.FontName = "Century Gothic";
            f2.FontHeightInPoints = 12;
            f2.IsBold = true;
            styleBold.SetFont(f2);

            string[] headings = { "Date", "Standard Hours", "OT Hours", "Employee", "Amount" };
            int columnCount = 5;
            int rowCount = 0;

            IRow headingRow = sheet.CreateRow(rowCount++);
            for (int i = 0; i < columnCount; i++)
            {
                headingRow.CreateCell(i).SetCellValue(headings[i]);
                headingRow.GetCell(i).CellStyle = styleBold;
            }

            decimal totalAmount = 0;

            foreach (TransactionsHORow item in rowList)
            {
                IRow row = sheet.CreateRow(rowCount++);

                row.CreateCell(0).SetCellValue(item.Date);
                if (item.Hours != 0) row.CreateCell(1).SetCellValue(item.Hours.ToString());
                else row.CreateCell(1).SetCellValue("");
                if (item.HoursOT != 0) row.CreateCell(2).SetCellValue(item.HoursOT.ToString());
                else row.CreateCell(2).SetCellValue("");
                row.CreateCell(3).SetCellValue(item.Lastname.ToUpper());
                row.CreateCell(4).SetCellValue(string.Format("${0:0.00}", item.Amount));
                totalAmount += item.Amount;
                
                for (int i = 0; i < 5; i++) row.GetCell(i).CellStyle = style;
            }

            IRow rowTotal = sheet.CreateRow(rowCount++);
            rowTotal.CreateCell(4).SetCellValue(string.Format("${0:0.00}", totalAmount));
            rowTotal.GetCell(4).CellStyle = styleBold;

            for (int i = 0; i < columnCount; i++) sheet.AutoSizeColumn(i, false);

            using (FileStream f = System.IO.File.Create(filepath))
            {
                wb.Write(f);
                f.Close();
            }

            return PhysicalFile(Path.GetFullPath(Url.Content(filepath)), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
