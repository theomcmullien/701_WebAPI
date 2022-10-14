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

        static ICellStyle currencyStyle;
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

            List<TransactionsHO> reports = new List<TransactionsHO>();
            HashSet<string> dates = new HashSet<string>();

            foreach (Job j in await _context.Job.ToListAsync())
            {
                if (j.StartTime == null) continue;
                DateTime dt = DateTime.ParseExact(j.StartTime, fmt, null);
                dates.Add($"{dt.Month}/{dt.Year}");
            }
            
            foreach (string date in dates)
            {
                string[] vars = date.Split("/");
                string month = GetMonth(int.Parse(vars[0]));
                int year = int.Parse(vars[1]);
                reports.Add(new TransactionsHO()
                { 
                    Date = date,
                    Filename = $"Transactions HO Export - {month} {year}"
                });
            }
            return reports;
        }

        public static string GetMonth(int n)
        {
            if (n == 1) return "January";
            else if (n == 2) return "February";
            else if (n == 3) return "March";
            else if (n == 4) return "April";
            else if (n == 5) return "May";
            else if (n == 6) return "June";
            else if (n == 7) return "July";
            else if (n == 8) return "August";
            else if (n == 9) return "September";
            else if (n == 10) return "October";
            else if (n == 11) return "November";
            return "December";
        }

        // GET: api/TransactionsHOs/date
        [HttpGet("{date}")]
        [Authorize]
        public async Task<ActionResult> GetTransactionsHO(string date)
        {
            if (_context.Job == null) return NotFound();
            var jobsList = await _context.Job.Where(j => j.IsCompleted).ToListAsync();

            date = date.Replace("%2F", "-");
            string[] vars = date.Split("-");
            int month = int.Parse(vars[0]);
            int year = int.Parse(vars[1]);
            string filepath = $"Data/ExcelReports/TransactionsHOExport/TransactionsHOExport_{date}.xlsx";

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
                List<Job> jobs = jobsList.Where(j => j.AccountID == a.AccountID && DateTime.ParseExact(j.StartTime, fmt, null).Month == month && DateTime.ParseExact(j.StartTime, fmt, null).Year == year).ToList();
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
            IRow headingRow = sheet.CreateRow(0);
            int columnCount = 5;
            for (int i = 0; i < columnCount; i++)
            {
                headingRow.CreateCell(i).SetCellValue(headings[i]);
                headingRow.GetCell(i).CellStyle = styleBold;
            }

            int newRow = 1;
            decimal totalAmount = 0;

            foreach (TransactionsHORow item in rowList)
            {
                IRow row = sheet.CreateRow(newRow);

                row.CreateCell(0).SetCellValue(item.Date);
                if (item.Hours != 0) row.CreateCell(1).SetCellValue(item.Hours.ToString());
                else row.CreateCell(1).SetCellValue("");
                if (item.HoursOT != 0) row.CreateCell(2).SetCellValue(item.HoursOT.ToString());
                else row.CreateCell(2).SetCellValue("");
                row.CreateCell(3).SetCellValue(item.Lastname.ToUpper());
                row.CreateCell(4).SetCellValue(string.Format("${0:0.00}", item.Amount));
                totalAmount += item.Amount;
                newRow++;

                for (int i = 0; i < 5; i++) row.GetCell(i).CellStyle = style;
            }

            IRow rowTotal = sheet.CreateRow(newRow);
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
