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

using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace _701_WebAPI.Controllers.ReportControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsHOsController : ControllerBase
    {
        private readonly _701_WebAPIContext _context;

        static ICellStyle currencyStyle;

        public TransactionsHOsController(_701_WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/TransactionsHOs
        [HttpGet]
        public async Task<ActionResult> GetTransactionsHO()
        {
            IWorkbook wb = new XSSFWorkbook();
            ISheet sheet = wb.CreateSheet();

            string filepath = "Data/ExcelReports/test.xlsx";

            //style
            currencyStyle = sheet.Workbook.CreateCellStyle();
            currencyStyle.DataFormat = sheet.Workbook.CreateDataFormat().GetFormat("$#,##0.00");


            string[] headings = { "Country Name", "Total", "Gold", "Silver", "Bronze" };

            //List<Medals> medalList = DB.GetMedalsList();
            List<Item> medalList = new List<Item>();
            medalList.Add(new Item(554354353, 6, 6, "New Zealand"));
            medalList.Add(new Item(5, 6, 3, "Australia"));
            medalList.Add(new Item(6, 2, 7, "Canada"));
            medalList.Add(new Item(2, 3, 4, "America"));
            medalList.Add(new Item(1, 4, 9, "Switzerland"));
            medalList.Add(new Item(6, 9, 3, "Germany"));


            IRow headingRow = sheet.CreateRow(0);

            int columnCount = 5;

            for (int i = 0; i < columnCount; i++)
            {
                headingRow.CreateCell(i).SetCellValue(headings[i]);
                ICell cell = headingRow.GetCell(i);
                cell.CellStyle = currencyStyle;
            }

            int newRow = 0;

            foreach (Item medal in medalList)
            {
                newRow++;
                IRow row = sheet.CreateRow(newRow);

                int total = medal.Bronze + medal.Silver + medal.Gold;

                row.CreateCell(0).SetCellValue(medal.Country);
                row.CreateCell(1).SetCellValue(total);
                row.CreateCell(2).SetCellValue(medal.Gold);
                row.CreateCell(3).SetCellValue(medal.Silver);
                row.CreateCell(4).SetCellValue(medal.Bronze);
                row.Cells[2].CellStyle = currencyStyle;
            }

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
