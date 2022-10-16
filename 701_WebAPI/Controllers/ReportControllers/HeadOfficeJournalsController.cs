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
    public class HeadOfficeJournalsController : ControllerBase
    {
        private readonly _701_WebAPIContext _context;

        static string fmt = "dd/MM/yyyy HH:mm";

        public HeadOfficeJournalsController(_701_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/BranchManagers/5
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<HeadOfficeJournal>>> GetHeadOfficeJournals()
        {
            if (_context.Job == null) return NotFound();
            if (_context.FinancialPeriod == null) return NotFound();

            var fList = await _context.FinancialPeriod.ToListAsync();

            List<HeadOfficeJournal> reports = new List<HeadOfficeJournal>();
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
                reports.Add(new HeadOfficeJournal()
                {
                    FinancialPeriodID = f.FinancialPeriodID,
                    Filename = $"Head Office Journal Report - {f.Month} {f.Year}"
                });
            }

            return reports;
        }

        // GET: api/TransactionsHOs/5
        [HttpGet("{financialPeriodID}")]
        [Authorize]
        public async Task<ActionResult> GetBranchManagers(int financialPeriodID)
        {
            if (_context.Job == null) return NotFound();
            if (_context.Establishment == null) return NotFound();
            if (_context.FinancialPeriod == null) return NotFound();
            if (_context.ChargeCode == null) return NotFound();

            var jobsList = await _context.Job.Where(j => j.IsCompleted).ToListAsync();

            var financialPeriod = await _context.FinancialPeriod.FindAsync(financialPeriodID);
            if (financialPeriod == null) return NotFound();

            string filepath = $"Data/ExcelReports/HeadOfficeJournal/HeadOfficeJournal_{financialPeriod.Month}_{financialPeriod.Year}.xlsx";

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

            var rowList = new List<HeadOfficeJournalRow>();

            foreach (Account a in accounts)
            {
                List<Job> jobs = jobsList.Where(j => j.AccountID == a.AccountID && DateTime.ParseExact(j.StartTime, fmt, null) > DateTime.ParseExact(financialPeriod.StartDate, fmt, null) && DateTime.ParseExact(j.StartTime, fmt, null) < DateTime.ParseExact(financialPeriod.EndDate, fmt, null)).ToList();
                List<List<Job>> llJobs = new List<List<Job>>();
                foreach (Job j in jobs)
                {
                    bool inList = false;
                    foreach (var jList in llJobs)
                    {
                        bool isDateMatch = DateTime.ParseExact(jList[0].StartTime, fmt, null).ToShortDateString() == DateTime.ParseExact(j.StartTime, fmt, null).ToShortDateString();
                        if (isDateMatch && jList[0].ChargeCodeID == j.ChargeCodeID && jList[0].EstablishmentID == j.EstablishmentID)
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
                    var chargeCode = await _context.ChargeCode.FindAsync(jList[0].ChargeCodeID);
                    if (chargeCode == null) continue;
                    var establishment = await _context.Establishment.FindAsync(jList[0].EstablishmentID);
                    if (establishment == null) continue;

                    HeadOfficeJournalRow h = new HeadOfficeJournalRow()
                    {
                        EstablishmentID = establishment.EstablishmentID,
                        DateRange = $"{DateTime.ParseExact(financialPeriod.StartDate, fmt, null).ToShortDateString()} to {DateTime.ParseExact(financialPeriod.EndDate, fmt, null).ToShortDateString()}",
                        ChargeCodeID = chargeCode.ChargeCodeID,
                        Date = DateTime.ParseExact(jList[0].StartTime, fmt, null).ToShortDateString(),
                        Amount = 0
                    };

                    double? hours = 0;
                    double? hoursOT = 0;

                    foreach (Job j in jList)
                    {
                        hours += j.Hours;
                        hoursOT += j.HoursOT;
                    }

                    if (a.Rate == null) a.Rate = 0;
                    if (a.RateOT == null) a.RateOT = 0;
                    h.Amount = (decimal)hours * (decimal)a.Rate + (decimal)hoursOT * (decimal)a.RateOT;

                    rowList.Add(h);
                }
            }

            IWorkbook wb = new XSSFWorkbook();
            ISheet sheet = wb.CreateSheet();

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

            ICellStyle styleBoldBig = wb.CreateCellStyle();
            IFont f3 = wb.CreateFont();
            f3.FontName = "Century Gothic";
            f3.FontHeightInPoints = 16;
            f3.IsBold = true;
            styleBoldBig.SetFont(f3);

            string[] headings = { "Charge Code", "Amount" };
            int columnCount = 2;
            int rowCount = 0;

            if (rowList.Count() > 0)
            {
                HeadOfficeJournalRow first = rowList[0];
                IRow r1 = sheet.CreateRow(rowCount++);
                rowCount++;
                r1.CreateCell(0).SetCellValue("Date Range:");
                r1.GetCell(0).CellStyle = style;
                r1.CreateCell(1).SetCellValue(first.DateRange);
                r1.GetCell(1).CellStyle = style;
            }

            List<List<HeadOfficeJournalRow>> llEstablishmentRows = new List<List<HeadOfficeJournalRow>>();
            foreach (HeadOfficeJournalRow h in rowList)
            {
                bool inList = false;
                foreach (var row in llEstablishmentRows)
                {
                    if (row[0].EstablishmentID == h.EstablishmentID)
                    {
                        row.Add(h);
                        inList = true;
                        break;
                    }
                }
                if (!inList)
                {
                    var newList = new List<HeadOfficeJournalRow>();
                    newList.Add(h);
                    llEstablishmentRows.Add(newList);
                }
            }

            decimal totalForReport = 0;

            foreach (List<HeadOfficeJournalRow> eList in llEstablishmentRows)
            {
                var establishment = await _context.Establishment.FindAsync(eList[0].EstablishmentID);
                if (establishment == null) continue;

                IRow re = sheet.CreateRow(rowCount++);
                re.CreateCell(0).SetCellValue(establishment.Name);
                re.GetCell(0).CellStyle = styleBoldBig;

                List<List<HeadOfficeJournalRow>> llChargeCodeRows = new List<List<HeadOfficeJournalRow>>();
                foreach (HeadOfficeJournalRow h in eList)
                {
                    bool inList = false;
                    foreach (var row in llChargeCodeRows)
                    {
                        if (row[0].ChargeCodeID == h.ChargeCodeID)
                        {
                            row.Add(h);
                            inList = true;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        var newList = new List<HeadOfficeJournalRow>();
                        newList.Add(h);
                        llChargeCodeRows.Add(newList);
                    }
                }

                IRow headingRow = sheet.CreateRow(rowCount++);
                for (int i = 0; i < columnCount; i++)
                {
                    headingRow.CreateCell(i).SetCellValue(headings[i]);
                    headingRow.GetCell(i).CellStyle = styleBold;
                }

                decimal totalForBranch = 0;

                foreach (List<HeadOfficeJournalRow> cList in llChargeCodeRows)
                {
                    var chargeCode = await _context.ChargeCode.FindAsync(cList[0].ChargeCodeID);
                    if (chargeCode == null) continue;

                    decimal totalForChargeCode = 0;

                    foreach (HeadOfficeJournalRow item in cList) totalForChargeCode += item.Amount;

                    IRow row = sheet.CreateRow(rowCount++);
                    row.CreateCell(0).SetCellValue(chargeCode.Name);
                    row.GetCell(0).CellStyle = style;
                    row.CreateCell(1).SetCellValue(string.Format("${0:0.00}", totalForChargeCode));
                    row.GetCell(1).CellStyle = style;

                    totalForBranch += totalForChargeCode;
                }

                IRow rowTotalForBranch = sheet.CreateRow(rowCount++);
                rowTotalForBranch.CreateCell(0).SetCellValue("Branch Total:");
                rowTotalForBranch.GetCell(0).CellStyle = styleBold;
                rowTotalForBranch.CreateCell(1).SetCellValue(string.Format("${0:0.00}", totalForBranch));
                rowTotalForBranch.GetCell(1).CellStyle = styleBold;

                totalForReport += totalForBranch;

                rowCount++;
            }

            IRow rowTotalForReport = sheet.CreateRow(rowCount);
            rowTotalForReport.CreateCell(0).SetCellValue("Report Total:");
            rowTotalForReport.GetCell(0).CellStyle = styleBold;
            rowTotalForReport.CreateCell(1).SetCellValue(string.Format("${0:0.00}", totalForReport));
            rowTotalForReport.GetCell(1).CellStyle = styleBold;

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
