using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _701_WebAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChargeCode",
                columns: table => new
                {
                    ChargeCodeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargeCode", x => x.ChargeCodeID);
                });

            migrationBuilder.CreateTable(
                name: "Establishment",
                columns: table => new
                {
                    EstablishmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Establishment", x => x.EstablishmentID);
                });

            migrationBuilder.CreateTable(
                name: "EstablishmentCode",
                columns: table => new
                {
                    EstablishmentCodeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstablishmentID = table.Column<int>(type: "int", nullable: false),
                    ChargeCodeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstablishmentCode", x => x.EstablishmentCodeID);
                });

            migrationBuilder.CreateTable(
                name: "FinancialPeriod",
                columns: table => new
                {
                    FinancialPeriodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weeks = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialPeriod", x => x.FinancialPeriodID);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    JobID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hours = table.Column<double>(type: "float", nullable: true),
                    HoursOT = table.Column<double>(type: "float", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    FinancialPeriodID = table.Column<int>(type: "int", nullable: true),
                    EstablishmentID = table.Column<int>(type: "int", nullable: false),
                    ChargeCodeID = table.Column<int>(type: "int", nullable: false),
                    AccountID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.JobID);
                });

            migrationBuilder.CreateTable(
                name: "Trade",
                columns: table => new
                {
                    TradeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trade", x => x.TradeID);
                });

            migrationBuilder.InsertData(
                table: "ChargeCode",
                columns: new[] { "ChargeCodeID", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "7471", "Repairs & Maintenance" },
                    { 2, "9891800", "Redevelopment" },
                    { 3, "746211", "COAL BOILER" },
                    { 4, "746212", "COAL FEED EQUIPMENT" },
                    { 5, "746213", "DIESEL BOILER" },
                    { 6, "746214", "STEAM BOILER" },
                    { 7, "746215", "ELECTRIC BOILER" },
                    { 8, "746221", "HEAT CIRCULATION PUMPS" },
                    { 9, "746222", "HEATING MAINTENANCE" },
                    { 10, "746231", "VENTILATION - EXTRACTION" },
                    { 11, "746232", "VENTILATION - AIR CIRCULATION" },
                    { 12, "746241", "SWITCHBOARD MAINTENANCE" },
                    { 13, "746242", "LOAD SHEDDING EQUIPMENT" },
                    { 14, "746243", "ALL APPLIANCE MAINTENANCE" },
                    { 15, "746244", "COMPUTER EQUIPMENT" },
                    { 16, "746245", "RADIO AND TV ELECTRONIC" },
                    { 17, "746246", "P.A.B.X and TELEPHONE" },
                    { 18, "746247", "TILL MAINTENANCE" },
                    { 19, "746248", "LIGHT BULB COST" },
                    { 20, "746249", "EMERGENCY GENERATOR COSTS" },
                    { 21, "746251", "ROOM MAINTENANCE" },
                    { 22, "746252", "KITCHEN MAINTENANCE" },
                    { 23, "746253", "BAR MAINTENANCE" },
                    { 24, "746254", "BOTTLE STORE MAINTENANCE" },
                    { 25, "746255", "RESTAURANT MAINTENANCE" },
                    { 26, "746256", "EXTERIOR MAINTENANCE" },
                    { 27, "746257", "RECEPTION & FOYER MAINTENANCE " },
                    { 28, "746258", "CONFERENCE ROOM MAINTENANCE" },
                    { 29, "746259", "MANAGER RESIDENCE MAINTENANCE" },
                    { 30, "746260", "SERVICE AREA MAINTENANCE" },
                    { 31, "746261", "KELVIN 7th FLOOR" },
                    { 32, "746262", "SHOP MAINTENANCE" },
                    { 33, "746270", "REFRIGERATION LIQUOR" },
                    { 34, "746271", "REFRIGERATION FOOD" },
                    { 35, "746272", "CARPET & FLOOR CLEANING" },
                    { 36, "746273", "SKIP BIN COSTS" },
                    { 37, "746274", "PEST CONTROL" },
                    { 38, "746275", "WINDOW CLEANING" },
                    { 39, "746276", "BUILDING W.O.F." },
                    { 40, "746277", "N.Z. FIRE SERVICE COSTS" },
                    { 41, "746278", "LIFT MAINTENANCE" },
                    { 42, "746279", "FIRE ALARM CALL OUTS" }
                });

            migrationBuilder.InsertData(
                table: "ChargeCode",
                columns: new[] { "ChargeCodeID", "Code", "Name" },
                values: new object[,]
                {
                    { 43, "746280", "BEER LINE CLEANING" },
                    { 44, "746281", "TOILET FLUSH VALVES" },
                    { 45, "746291", "PROMOTIONAL" },
                    { 46, "7685", "NON BUDGET COSTS" },
                    { 47, "746296", "NURSERY COSTS - LAWNS" },
                    { 48, "746297", "NURSERY COSTS - GARDENS" },
                    { 49, "747500", "OTHER" }
                });

            migrationBuilder.InsertData(
                table: "Establishment",
                columns: new[] { "EstablishmentID", "Address", "Name" },
                values: new object[,]
                {
                    { 1, "Corner of Tay Street & Racecourse Road", "Ascot Hotel Motel" },
                    { 2, "20 Kelvin Street", "Kelvin Hotel" },
                    { 3, "329 Dee Street", "Homestead Villa" },
                    { 4, "256 Tay Street", "Balmoral Lodge" },
                    { 5, "327 Tay Street", "Cable Court" },
                    { 6, "292 Dee Street", "Ashford Motor" },
                    { 7, "60 Glengarry Crescent", "Eastern Suburbs Tavern" },
                    { 8, "315 Centre Street", "Newfield Tavern" },
                    { 9, "Sydney Street", "Northern Tavern" },
                    { 10, "Dee Street", "Avenal Homestead" },
                    { 11, "181 North Road", "Waikiwi Tavern" },
                    { 12, "28 Cnr Dee & Leet Street", "Lone Star Cafe" },
                    { 13, "410 Elles Road", "Southland Tavern" },
                    { 14, "38 Dee Street", "Speights Alehouse" },
                    { 15, "90 Dee Street", "Waxys" },
                    { 16, "Cnr Elles Rd & Tweed Street", "South City Liquorland" },
                    { 17, "44 North Road", "Collingwood Super Liquor" },
                    { 18, "12 Bamborough Street", "East End Bottlestore" },
                    { 19, "8 Windsor Street", "Windsor Bottlestore" },
                    { 20, "406 Elles Road", "Southland Super Liquor" },
                    { 21, "35 East Road", "Ascot Sports Bar" },
                    { 22, "252 Dee Street", "Centrepoint Liquorland" },
                    { 23, "59 Dee Street", "The Langlands" },
                    { 24, "228 Elles Road", "Workshop Hours" }
                });

            migrationBuilder.InsertData(
                table: "EstablishmentCode",
                columns: new[] { "EstablishmentCodeID", "ChargeCodeID", "EstablishmentID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 43, 1 },
                    { 4, 48, 1 },
                    { 5, 1, 23 },
                    { 6, 2, 23 },
                    { 7, 43, 23 },
                    { 8, 1, 2 },
                    { 9, 2, 2 },
                    { 10, 43, 2 },
                    { 11, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "EstablishmentCode",
                columns: new[] { "EstablishmentCodeID", "ChargeCodeID", "EstablishmentID" },
                values: new object[,]
                {
                    { 12, 2, 3 },
                    { 13, 1, 4 },
                    { 14, 2, 4 },
                    { 15, 1, 5 },
                    { 16, 2, 5 },
                    { 17, 1, 6 },
                    { 18, 2, 6 },
                    { 19, 1, 7 },
                    { 20, 2, 7 },
                    { 21, 43, 7 },
                    { 22, 1, 8 },
                    { 23, 2, 8 },
                    { 24, 43, 8 },
                    { 25, 1, 9 },
                    { 26, 2, 9 },
                    { 27, 43, 9 },
                    { 28, 1, 10 },
                    { 29, 2, 10 },
                    { 30, 43, 10 },
                    { 31, 1, 11 },
                    { 32, 2, 11 },
                    { 33, 43, 11 },
                    { 34, 1, 12 },
                    { 35, 2, 12 },
                    { 36, 43, 12 },
                    { 37, 1, 13 },
                    { 38, 2, 13 },
                    { 39, 43, 13 },
                    { 40, 1, 14 },
                    { 41, 2, 14 },
                    { 42, 43, 14 },
                    { 43, 1, 15 },
                    { 44, 2, 15 },
                    { 45, 43, 15 },
                    { 46, 1, 22 },
                    { 47, 2, 22 },
                    { 48, 43, 22 },
                    { 49, 1, 16 },
                    { 50, 2, 16 },
                    { 51, 43, 16 },
                    { 52, 1, 17 },
                    { 53, 2, 17 }
                });

            migrationBuilder.InsertData(
                table: "EstablishmentCode",
                columns: new[] { "EstablishmentCodeID", "ChargeCodeID", "EstablishmentID" },
                values: new object[,]
                {
                    { 54, 43, 17 },
                    { 55, 1, 18 },
                    { 56, 2, 18 },
                    { 57, 43, 18 },
                    { 58, 1, 19 },
                    { 59, 2, 19 },
                    { 60, 1, 20 },
                    { 61, 2, 20 },
                    { 62, 43, 20 },
                    { 63, 1, 20 },
                    { 64, 2, 20 },
                    { 65, 1, 21 },
                    { 66, 2, 21 },
                    { 67, 49, 24 }
                });

            migrationBuilder.InsertData(
                table: "FinancialPeriod",
                columns: new[] { "FinancialPeriodID", "EndDate", "Month", "StartDate", "Weeks", "Year" },
                values: new object[,]
                {
                    { 1, "01/05/2022 00:00", "April", "27/03/2022 00:00", 5, 2022 },
                    { 2, "29/05/2022 00:00", "May", "01/05/2022 00:00", 4, 2022 },
                    { 3, "26/06/2022 00:00", "June", "29/05/2022 00:00", 4, 2022 },
                    { 4, "31/07/2022 00:00", "July", "26/06/2022 00:00", 5, 2022 },
                    { 5, "28/08/2022 00:00", "August", "31/07/2022 00:00", 4, 2022 },
                    { 6, "02/10/2022 00:00", "September", "28/08/2022 00:00", 5, 2022 },
                    { 7, "30/10/2022 00:00", "October", "02/10/2022 00:00", 4, 2022 },
                    { 8, "27/11/2022 00:00", "November", "30/10/2022 00:00", 4, 2022 },
                    { 9, "01/01/2023 00:00", "December", "27/11/2022 00:00", 5, 2022 },
                    { 10, "29/01/2023 00:00", "January", "01/01/2023 00:00", 4, 2022 },
                    { 11, "26/02/2023 00:00", "February", "29/01/2023 00:00", 4, 2022 },
                    { 12, "02/04/2023 00:00", "March", "26/02/2023 00:00", 4, 2022 }
                });

            migrationBuilder.InsertData(
                table: "Job",
                columns: new[] { "JobID", "AccountID", "ChargeCodeID", "EstablishmentID", "FinancialPeriodID", "FinishTime", "Hours", "HoursOT", "IsCompleted", "Notes", "StartTime" },
                values: new object[,]
                {
                    { 1, "auth0|6332c7bb35450ad949086866", 1, 16, 6, "29/08/2022 10:45", 5.25, 0.0, true, "Just started painting the entirety of south city liquorland", "29/08/2022 05:30" },
                    { 2, "auth0|6332c7bb35450ad949086866", 1, 16, 6, "29/08/2022 17:00", 8.0, 1.0, true, "Continued painting the entirety of south city liquorland", "29/08/2022 13:00" },
                    { 3, "auth0|6332c7bb35450ad949086866", 1, 16, 6, "15/09/2022 12:45", 6.75, 0.0, true, "Continued painting the entirety of south city liquorland", "15/09/2022 06:00" },
                    { 4, "auth0|6332c7bb35450ad949086866", 1, 16, 6, "17/09/2022 09:00", 3.0, 0.0, true, "Just finished painting the entirety of south city liquorland by myself", "17/09/2022 06:00" },
                    { 5, "auth0|634575dbe16807ccd2b3818d", 2, 15, 4, "13/06/2022 13:00", 8.0, 0.5, true, "Just finished building some chairs for waxys", "13/06/2022 05:00" },
                    { 6, "auth0|634575dbe16807ccd2b3818d", 2, 15, 4, "14/06/2022 10:00", 5.0, 0.0, true, "Just started building some tables for waxys", "14/06/2022 05:00" },
                    { 7, "auth0|634575dbe16807ccd2b3818d", 2, 15, 4, "15/06/2022 11:00", 3.5, 0.0, true, "Just finished building some tables for waxys", "15/06/2022 07:30" },
                    { 8, "auth0|634576856f00df75fed2d2ed", 1, 1, 10, "04/01/2023 13:00", 7.5, 0.0, true, "Did some plumbing out at the ascot", "04/01/2023 05:30" },
                    { 9, "auth0|634576856f00df75fed2d2ed", 1, 1, 10, "06/01/2023 13:00", 8.0, 1.5, true, "Did some plumbing out at the ascot, a pipe under the sink was leaking", "06/01/2023 05:00" },
                    { 10, "auth0|634576856f00df75fed2d2ed", 2, 11, 3, "11/06/2022 13:00", 7.5, 0.0, true, "Did some plumbing out at the kiwi", "11/06/2022 05:30" },
                    { 11, "auth0|634576856f00df75fed2d2ed", 2, 11, 3, "13/06/2022 13:00", 8.0, 1.5, true, "Did some plumbing out at the kiw, a leaky pipe", "13/06/2022 05:00" },
                    { 12, "auth0|634578296072ed94c436db3c", 1, 11, 3, "16/06/2022 18:00", 5.0, 0.0, true, "Started some wiring out at the kiwi", "16/06/2022 13:00" },
                    { 13, "auth0|634578296072ed94c436db3c", 1, 11, 3, "18/06/2022 17:00", 5.0, 0.0, true, "Finished the wiring out at the kiwi", "18/06/2022 12:00" }
                });

            migrationBuilder.InsertData(
                table: "Trade",
                columns: new[] { "TradeID", "Type" },
                values: new object[,]
                {
                    { 1, "Painter" },
                    { 2, "Carpenter" },
                    { 3, "Plumber" }
                });

            migrationBuilder.InsertData(
                table: "Trade",
                columns: new[] { "TradeID", "Type" },
                values: new object[] { 4, "Beer Serviceman" });

            migrationBuilder.InsertData(
                table: "Trade",
                columns: new[] { "TradeID", "Type" },
                values: new object[] { 5, "Electrican" });

            migrationBuilder.InsertData(
                table: "Trade",
                columns: new[] { "TradeID", "Type" },
                values: new object[] { 6, "Gardener" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChargeCode");

            migrationBuilder.DropTable(
                name: "Establishment");

            migrationBuilder.DropTable(
                name: "EstablishmentCode");

            migrationBuilder.DropTable(
                name: "FinancialPeriod");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Trade");
        }
    }
}
