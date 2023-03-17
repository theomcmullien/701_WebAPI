using _701_WebAPI.Controllers.Classes;
using _701_WebAPI.Data;
using _701_WebAPI.Models;
using _701_WebAPI.Models.Auth0;
using _701_WebAPI.Models.DTO;
using _701_WebAPI.Models.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using RestSharp;

namespace _701_WebAPI.Controllers.GetControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly _701_WebAPIContext _context;

        static string fmt = "dd/MM/yyyy HH:mm";

        public EmployeesController(_701_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Account>>> GetEmployees()
        {
            if (_context.Job == null) return NotFound();

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

                    //using (var client2 = new RestClient($"https://dev-bss0r74x.au.auth0.com/api/v2/users/{a.AccountID}/roles"))
                    //{

                    //    var request2 = new RestRequest();
                    //    request2.AddHeader("authorization", "");
                    //    var result2 = await client2.GetAsync(request2);
                    //    string json2 = result2.Content.ToString();

                    //    AccountRole[] accountRole = JsonConvert.DeserializeObject<AccountRole[]>(json2);

                    //    if (accountRole.Length < 1) continue;
                    //    string role = accountRole.First().ToString();
                    //    if (role.ToLower() != "employee" || a.MetaData == null) continue;

                    //    account.Role = role;
                    //    account.Rate = a.MetaData.Rate;
                    //    account.RateOT = a.MetaData.RateOT;
                    //    account.TradeID = a.MetaData.TradeID;
                    //}
                    //accounts.Add(account);
                }
            }

            foreach (Account a in accounts)
            {
                var jobs = await _context.Job.Where(j => j.AccountID == a.AccountID && j.IsCompleted).ToListAsync();
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

                if (llJobs.Count() > 0) a.JobSheets = new List<JobSheet>();

                foreach (var jList in llJobs)
                {
                    string date = DateTime.ParseExact(jList[0].StartTime, fmt, null).ToShortDateString().Replace("/", "-");

                    JobSheet jobSheet = new JobSheet()
                    {
                        Filename = string.Format("Job Sheet - {0} {1} - {2}", a.Firstname, a.Lastname, date),
                        DateCompleted = date,
                        Jobs = jList
                    };
                    a.JobSheets.Add(jobSheet);
                }

            }

            return accounts;
        }

        // GET: api/TimeSheets/5/date
        [HttpGet("{accountID}/{date}")]
        [Authorize]
        public async Task<ActionResult> GetTimeSheet(string accountID, string date)
        {
            if (_context.Job == null) return NotFound();
            if (_context.Establishment == null) return NotFound();
            if (_context.ChargeCode == null) return NotFound();
            var jobsList = await _context.Job.Where(j => j.IsCompleted).ToListAsync();

            string[] vars = date.Split("-");
            int day = int.Parse(vars[0]);
            int month = int.Parse(vars[1]);
            int year = int.Parse(vars[2]);
            string filepath = $"Data/ExcelReports/ILTMaintenanceTimesheet/ILTMaintenanceTimesheet_{date}.xlsx";

            Account account = new Account();

            using (var client = new RestClient($"https://dev-bss0r74x.au.auth0.com/api/v2/users/{accountID}"))
            {
                var request = new RestRequest();
                request.AddHeader("authorization", await AccessToken.GetToken());
                var result = await client.GetAsync(request);
                string json = result.Content.ToString();
                AccountAuth0 a = JsonConvert.DeserializeObject<AccountAuth0>(json);
                string[] username = a.Username.Split(".");
                account.AccountID = a.AccountID;
                account.Email = a.Email;
                account.Firstname = username[0];
                account.Lastname = username[1];
                if (a.MetaData == null) return NotFound();
                account.Role = a.MetaData.Role;
                account.Rate = a.MetaData.Rate;
                account.RateOT = a.MetaData.RateOT;
                account.TradeID = a.MetaData.TradeID;
            }

            var rowList = new List<TimeSheetRow>();

            List<Job> jobs = jobsList.Where(j => j.AccountID == account.AccountID && DateTime.ParseExact(j.StartTime, fmt, null).Day == day && DateTime.ParseExact(j.StartTime, fmt, null).Month == month && DateTime.ParseExact(j.StartTime, fmt, null).Year == year).ToList();

            foreach (Job job in jobs)
            {
                rowList.Add(new TimeSheetRow()
                {
                    Fullname = $"{account.Firstname} {account.Lastname}",
                    Date = job.StartTime,
                    Establishment = await _context.Establishment.Where(e => e.EstablishmentID == job.EstablishmentID).Select(e => e.Name).FirstAsync(),
                    ChargeCodeName = await _context.ChargeCode.Where(c => c.ChargeCodeID == job.ChargeCodeID).Select(c => c.Name).FirstAsync(),
                    Notes = job.Notes,
                    Hours = job.Hours,
                    HoursOT = job.HoursOT
                });
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

            string[] headings = { "Establishment", "Work Type", "Job Notes", "Standard Hours", "OT Hours" };
            int columnCount = 5;
            int rowCount = 0;

            if (rowList.Count() > 0)
            {
                TimeSheetRow timesheet = rowList[0];
                IRow r1 = sheet.CreateRow(rowCount++);
                r1.CreateCell(0).SetCellValue(timesheet.Fullname.ToUpper());
                r1.CreateCell(1).SetCellValue(timesheet.Date);
                r1.GetCell(0).CellStyle = style;
                r1.GetCell(1).CellStyle = style;
            }

            IRow headingRow = sheet.CreateRow(rowCount++);
            for (int i = 0; i < columnCount; i++)
            {
                headingRow.CreateCell(i).SetCellValue(headings[i]);
                headingRow.GetCell(i).CellStyle = styleBold;
            }

            double? totalHours = 0;
            double? totalHoursOT = 0;

            foreach (TimeSheetRow item in rowList)
            {
                IRow row = sheet.CreateRow(rowCount++);

                row.CreateCell(0).SetCellValue(item.Establishment);
                row.CreateCell(1).SetCellValue(item.ChargeCodeName);
                row.CreateCell(2).SetCellValue(item.Notes);
                if (item.Hours != 0) row.CreateCell(3).SetCellValue(string.Format("{0}", item.Hours));
                else row.CreateCell(3).SetCellValue("");
                if (item.HoursOT != 0) row.CreateCell(4).SetCellValue(string.Format("{0}", item.HoursOT));
                else row.CreateCell(4).SetCellValue("");
                totalHours += item.Hours;
                totalHoursOT += item.HoursOT;
                
                for (int i = 0; i < 5; i++) row.GetCell(i).CellStyle = style;
            }

            IRow rowTotal = sheet.CreateRow(rowCount++);
            rowTotal.CreateCell(3).SetCellValue($"{totalHours}");
            rowTotal.CreateCell(4).SetCellValue($"{totalHoursOT}");
            rowTotal.GetCell(3).CellStyle = styleBold;
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
