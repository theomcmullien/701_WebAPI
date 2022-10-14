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
                    //    request2.AddHeader("authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjJ1WGVxVmY0d1NWOGVKcFVMYVZYdCJ9.eyJpc3MiOiJodHRwczovL2Rldi1ic3Mwcjc0eC5hdS5hdXRoMC5jb20vIiwic3ViIjoiRTJOYXd5TWhEcW5SV0k3RVE1SldPSXlQaTd6OXpmT0ZAY2xpZW50cyIsImF1ZCI6Imh0dHBzOi8vZGV2LWJzczByNzR4LmF1LmF1dGgwLmNvbS9hcGkvdjIvIiwiaWF0IjoxNjY0MTgwMjkyLCJleHAiOjE2NjY3NzIyOTIsImF6cCI6IkUyTmF3eU1oRHFuUldJN0VRNUpXT0l5UGk3ejl6Zk9GIiwic2NvcGUiOiJyZWFkOmNsaWVudF9ncmFudHMgY3JlYXRlOmNsaWVudF9ncmFudHMgZGVsZXRlOmNsaWVudF9ncmFudHMgdXBkYXRlOmNsaWVudF9ncmFudHMgcmVhZDp1c2VycyB1cGRhdGU6dXNlcnMgZGVsZXRlOnVzZXJzIGNyZWF0ZTp1c2VycyByZWFkOnVzZXJzX2FwcF9tZXRhZGF0YSB1cGRhdGU6dXNlcnNfYXBwX21ldGFkYXRhIGRlbGV0ZTp1c2Vyc19hcHBfbWV0YWRhdGEgY3JlYXRlOnVzZXJzX2FwcF9tZXRhZGF0YSByZWFkOnVzZXJfY3VzdG9tX2Jsb2NrcyBjcmVhdGU6dXNlcl9jdXN0b21fYmxvY2tzIGRlbGV0ZTp1c2VyX2N1c3RvbV9ibG9ja3MgY3JlYXRlOnVzZXJfdGlja2V0cyByZWFkOmNsaWVudHMgdXBkYXRlOmNsaWVudHMgZGVsZXRlOmNsaWVudHMgY3JlYXRlOmNsaWVudHMgcmVhZDpjbGllbnRfa2V5cyB1cGRhdGU6Y2xpZW50X2tleXMgZGVsZXRlOmNsaWVudF9rZXlzIGNyZWF0ZTpjbGllbnRfa2V5cyByZWFkOmNvbm5lY3Rpb25zIHVwZGF0ZTpjb25uZWN0aW9ucyBkZWxldGU6Y29ubmVjdGlvbnMgY3JlYXRlOmNvbm5lY3Rpb25zIHJlYWQ6cmVzb3VyY2Vfc2VydmVycyB1cGRhdGU6cmVzb3VyY2Vfc2VydmVycyBkZWxldGU6cmVzb3VyY2Vfc2VydmVycyBjcmVhdGU6cmVzb3VyY2Vfc2VydmVycyByZWFkOmRldmljZV9jcmVkZW50aWFscyB1cGRhdGU6ZGV2aWNlX2NyZWRlbnRpYWxzIGRlbGV0ZTpkZXZpY2VfY3JlZGVudGlhbHMgY3JlYXRlOmRldmljZV9jcmVkZW50aWFscyByZWFkOnJ1bGVzIHVwZGF0ZTpydWxlcyBkZWxldGU6cnVsZXMgY3JlYXRlOnJ1bGVzIHJlYWQ6cnVsZXNfY29uZmlncyB1cGRhdGU6cnVsZXNfY29uZmlncyBkZWxldGU6cnVsZXNfY29uZmlncyByZWFkOmhvb2tzIHVwZGF0ZTpob29rcyBkZWxldGU6aG9va3MgY3JlYXRlOmhvb2tzIHJlYWQ6YWN0aW9ucyB1cGRhdGU6YWN0aW9ucyBkZWxldGU6YWN0aW9ucyBjcmVhdGU6YWN0aW9ucyByZWFkOmVtYWlsX3Byb3ZpZGVyIHVwZGF0ZTplbWFpbF9wcm92aWRlciBkZWxldGU6ZW1haWxfcHJvdmlkZXIgY3JlYXRlOmVtYWlsX3Byb3ZpZGVyIGJsYWNrbGlzdDp0b2tlbnMgcmVhZDpzdGF0cyByZWFkOmluc2lnaHRzIHJlYWQ6dGVuYW50X3NldHRpbmdzIHVwZGF0ZTp0ZW5hbnRfc2V0dGluZ3MgcmVhZDpsb2dzIHJlYWQ6bG9nc191c2VycyByZWFkOnNoaWVsZHMgY3JlYXRlOnNoaWVsZHMgdXBkYXRlOnNoaWVsZHMgZGVsZXRlOnNoaWVsZHMgcmVhZDphbm9tYWx5X2Jsb2NrcyBkZWxldGU6YW5vbWFseV9ibG9ja3MgdXBkYXRlOnRyaWdnZXJzIHJlYWQ6dHJpZ2dlcnMgcmVhZDpncmFudHMgZGVsZXRlOmdyYW50cyByZWFkOmd1YXJkaWFuX2ZhY3RvcnMgdXBkYXRlOmd1YXJkaWFuX2ZhY3RvcnMgcmVhZDpndWFyZGlhbl9lbnJvbGxtZW50cyBkZWxldGU6Z3VhcmRpYW5fZW5yb2xsbWVudHMgY3JlYXRlOmd1YXJkaWFuX2Vucm9sbG1lbnRfdGlja2V0cyByZWFkOnVzZXJfaWRwX3Rva2VucyBjcmVhdGU6cGFzc3dvcmRzX2NoZWNraW5nX2pvYiBkZWxldGU6cGFzc3dvcmRzX2NoZWNraW5nX2pvYiByZWFkOmN1c3RvbV9kb21haW5zIGRlbGV0ZTpjdXN0b21fZG9tYWlucyBjcmVhdGU6Y3VzdG9tX2RvbWFpbnMgdXBkYXRlOmN1c3RvbV9kb21haW5zIHJlYWQ6ZW1haWxfdGVtcGxhdGVzIGNyZWF0ZTplbWFpbF90ZW1wbGF0ZXMgdXBkYXRlOmVtYWlsX3RlbXBsYXRlcyByZWFkOm1mYV9wb2xpY2llcyB1cGRhdGU6bWZhX3BvbGljaWVzIHJlYWQ6cm9sZXMgY3JlYXRlOnJvbGVzIGRlbGV0ZTpyb2xlcyB1cGRhdGU6cm9sZXMgcmVhZDpwcm9tcHRzIHVwZGF0ZTpwcm9tcHRzIHJlYWQ6YnJhbmRpbmcgdXBkYXRlOmJyYW5kaW5nIGRlbGV0ZTpicmFuZGluZyByZWFkOmxvZ19zdHJlYW1zIGNyZWF0ZTpsb2dfc3RyZWFtcyBkZWxldGU6bG9nX3N0cmVhbXMgdXBkYXRlOmxvZ19zdHJlYW1zIGNyZWF0ZTpzaWduaW5nX2tleXMgcmVhZDpzaWduaW5nX2tleXMgdXBkYXRlOnNpZ25pbmdfa2V5cyByZWFkOmxpbWl0cyB1cGRhdGU6bGltaXRzIGNyZWF0ZTpyb2xlX21lbWJlcnMgcmVhZDpyb2xlX21lbWJlcnMgZGVsZXRlOnJvbGVfbWVtYmVycyByZWFkOmVudGl0bGVtZW50cyByZWFkOmF0dGFja19wcm90ZWN0aW9uIHVwZGF0ZTphdHRhY2tfcHJvdGVjdGlvbiByZWFkOm9yZ2FuaXphdGlvbnNfc3VtbWFyeSByZWFkOm9yZ2FuaXphdGlvbnMgdXBkYXRlOm9yZ2FuaXphdGlvbnMgY3JlYXRlOm9yZ2FuaXphdGlvbnMgZGVsZXRlOm9yZ2FuaXphdGlvbnMgY3JlYXRlOm9yZ2FuaXphdGlvbl9tZW1iZXJzIHJlYWQ6b3JnYW5pemF0aW9uX21lbWJlcnMgZGVsZXRlOm9yZ2FuaXphdGlvbl9tZW1iZXJzIGNyZWF0ZTpvcmdhbml6YXRpb25fY29ubmVjdGlvbnMgcmVhZDpvcmdhbml6YXRpb25fY29ubmVjdGlvbnMgdXBkYXRlOm9yZ2FuaXphdGlvbl9jb25uZWN0aW9ucyBkZWxldGU6b3JnYW5pemF0aW9uX2Nvbm5lY3Rpb25zIGNyZWF0ZTpvcmdhbml6YXRpb25fbWVtYmVyX3JvbGVzIHJlYWQ6b3JnYW5pemF0aW9uX21lbWJlcl9yb2xlcyBkZWxldGU6b3JnYW5pemF0aW9uX21lbWJlcl9yb2xlcyBjcmVhdGU6b3JnYW5pemF0aW9uX2ludml0YXRpb25zIHJlYWQ6b3JnYW5pemF0aW9uX2ludml0YXRpb25zIGRlbGV0ZTpvcmdhbml6YXRpb25faW52aXRhdGlvbnMiLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMifQ.TdcttVCyVgQvjNDatGRIBRC6Z8twt6ixjF-46V2KHrN0BhVf6zpqLj9ri4YldxN-lC_2Vgiidx6nJiBxIzNG2F5fMMp9To4H_KwoqCesO-TEaeOSuCB4DaA7E9yTAYq4rY-7FslKbHjbczq66t0Yw8ht5yo-HL1S9kMGqfFsg8RdxGBKWj-GAB362S3CrgKB5mKR7-DcRxRSRjd78E_EOs9o7MzNrWqlMmiJCOO9ZNB3YzadhBp8A1ofxtgH6HdY_l6tgNNoIGMxC3GFHfmOeXONofichx4rsRIhnf4jSRQT_0fUxqY7TqfA8QFpriTzKif_QJDPEW66YpGbt1nLmA");
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
                    string date = DateTime.ParseExact(jList[0].StartTime, fmt, null).ToShortDateString();

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

            date = date.Replace("%2F", "-");
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

            if (rowList.Count() > 0)
            {
                var timesheet = rowList[0];
                IRow r1 = sheet.CreateRow(0);
                r1.CreateCell(0).SetCellValue(timesheet.Fullname.ToUpper());
                r1.CreateCell(1).SetCellValue(timesheet.Date);
                r1.GetCell(0).CellStyle = style;
                r1.GetCell(1).CellStyle = style;
            }

            string[] headings = { "Establishment", "Work Type", "Job Notes", "Standard Hours", "OT Hours" };
            IRow headingRow = sheet.CreateRow(1);
            int columnCount = 5;
            for (int i = 0; i < columnCount; i++)
            {
                headingRow.CreateCell(i).SetCellValue(headings[i]);
                headingRow.GetCell(i).CellStyle = styleBold;
            }

            int newRow = 2;
            double? totalHours = 0;
            double? totalHoursOT = 0;

            foreach (TimeSheetRow item in rowList)
            {
                IRow row = sheet.CreateRow(newRow);

                row.CreateCell(0).SetCellValue(item.Establishment);
                row.CreateCell(1).SetCellValue(item.ChargeCodeName);
                row.CreateCell(2).SetCellValue(item.Notes);
                if (item.Hours != 0) row.CreateCell(3).SetCellValue(string.Format("{0}", item.Hours));
                else row.CreateCell(3).SetCellValue("");
                if (item.HoursOT != 0) row.CreateCell(4).SetCellValue(string.Format("{0}", item.HoursOT));
                else row.CreateCell(4).SetCellValue("");
                totalHours += item.Hours;
                totalHoursOT += item.HoursOT;
                newRow++;

                for (int i = 0; i < 5; i++) row.GetCell(i).CellStyle = style;
            }

            IRow rowTotal = sheet.CreateRow(newRow);
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
