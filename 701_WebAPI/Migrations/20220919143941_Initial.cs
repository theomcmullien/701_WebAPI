using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _701_WebAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RateOT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TradeID = table.Column<int>(type: "int", nullable: true),
                    EstablishmentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountID);
                });

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
                    WorkTypeID = table.Column<int>(type: "int", nullable: false),
                    FinancialPeriodID = table.Column<int>(type: "int", nullable: true),
                    EstablishmentID = table.Column<int>(type: "int", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "WorkType",
                columns: table => new
                {
                    WorkTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChargeCodeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkType", x => x.WorkTypeID);
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountID", "Email", "EstablishmentID", "Firstname", "Lastname", "Password", "Rate", "RateOT", "Role", "TradeID" },
                values: new object[,]
                {
                    { 1, "rdempster@gmail.com", null, "Rickelle", "Dempster", "123", 50.00m, 0m, "Employee", 1 },
                    { 2, "wdimmock@gmail.com", null, "Wayne", "Dimmock", "123", 50.00m, 56.00m, "Employee", 2 },
                    { 3, "bmallon@gmail.com", null, "Brent", "Mallon", "123", 50.00m, 66.00m, "Employee", 3 },
                    { 4, "lmilne@gmail.com", null, "Logan", "Milne", "123", 50.00m, 0m, "Employee", 1 },
                    { 5, "jscully@gmail.com", null, "Jim", "Scully", "123", 50.00m, 0m, "Employee", 4 },
                    { 6, "bwalker@gmail.com", null, "Brian", "Walker", "123", 52.00m, 0m, "Employee", 5 },
                    { 7, "dean@ascotparkhotel.co.nz", 1, "Dean", "Nicol", "123", null, null, "Establishment Manager", null },
                    { 8, "manager@kelvinhotel.co.nz", 2, "Jo", "Harris", "123", null, null, "Establishment Manager", null },
                    { 9, "manager@villamotel.co.nz", 3, "Natalie", "Gilson", "123", null, null, "Establishment Manager", null },
                    { 10, "manager@balmoralmotel.co.nz", 4, "Sharon", "Hinga", "123", null, null, "Establishment Manager", null },
                    { 11, "manager@cablecourtmotel.co.nz", 5, "Allie", "Adams-Bell", "123", null, null, "Establishment Manager", null },
                    { 12, "manager@cablecourtmotel.co.nz", 5, "Lindsay", "Adams-Bell", "123", null, null, "Establishment Manager", null },
                    { 13, "manager@ashfordmotorlodge.co.nz", 6, "Shelley", "Jenkins", "123", null, null, "Establishment Manager", null },
                    { 14, "manager@easternsuburbs.co.nz", 7, "James", "Wesney", "123", null, null, "Establishment Manager", null },
                    { 15, "manager@newfieldtavern.co.nz", 8, "Jemma", "Wild", "123", null, null, "Establishment Manager", null },
                    { 16, "manager@northerntavern.co.nz", 9, "Northern", "Manager", "123", null, null, "Establishment Manager", null },
                    { 17, "manager@avenalhomestead.co.nz", 10, "Shanan", "Te-Maiharoa", "123", null, null, "Establishment Manager", null },
                    { 18, "manager@waikiwitavern.co.nz", 11, "Vicky", "Cole", "123", null, null, "Establishment Manager", null },
                    { 19, "manager@lonestarinv.co.nz", 12, "Lonestar", "Manager", "123", null, null, "Establishment Manager", null },
                    { 20, "manager@southlandtavern.co.nz", 13, "Jo", "Te-Maiharoa", "123", null, null, "Establishment Manager", null },
                    { 21, "manager@speightsalehouseinv.co.nz", 14, "Sam", "Harpur", "123", null, null, "Establishment Manager", null },
                    { 22, "manager@waxys.co.nz", 15, "Kelly", "Burgess", "123", null, null, "Establishment Manager", null },
                    { 23, "manager@liquorlandsouthcity.co.nz", 16, "Mary-Ellen", "Vercoe", "123", null, null, "Establishment Manager", null },
                    { 24, "manager@superliquorcollingwood.co.nz", 17, "Warren", "Tayles", "123", null, null, "Establishment Manager", null },
                    { 25, "manager@eastendliquor.co.nz", 18, "Tracy", "Poe", "123", null, null, "Establishment Manager", null },
                    { 26, "manager@windsorwines.co.nz", 19, "Stephane", "Fabre", "123", null, null, "Establishment Manager", null },
                    { 27, "manager@superliquorsouthland.co.nz", 20, "Simon", "Paterson", "123", null, null, "Establishment Manager", null },
                    { 28, "cate@ascotparkhotel.co.nz", 21, "Cate", "Wesney", "123", null, null, "Establishment Manager", null },
                    { 29, "manager@liquorlandcentrepoint.co.nz", 22, "Nicola", "Moss", "123", null, null, "Establishment Manager", null },
                    { 30, "bryan@thelanglands.co.nz", 23, "Bryan", "Townley", "123", null, null, "Establishment Manager", null },
                    { 31, "bevan@ilt.co.nz", 24, "Bevan", "Thompson", "123", null, null, "Establishment Manager", null }
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
                    { 11, "746232", "VENTILATION - AIR CIRCULATION" }
                });

            migrationBuilder.InsertData(
                table: "ChargeCode",
                columns: new[] { "ChargeCodeID", "Code", "Name" },
                values: new object[,]
                {
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
                    { 42, "746279", "FIRE ALARM CALL OUTS" },
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
                    { 1, "35 East Road", "Ascot" },
                    { 2, "20 Kelvin Street", "Kelvin Hotel" },
                    { 3, "329 Dee Street", "Homestead Villa" },
                    { 4, "256 Tay Street", "Balmoral Lodge" }
                });

            migrationBuilder.InsertData(
                table: "Establishment",
                columns: new[] { "EstablishmentID", "Address", "Name" },
                values: new object[,]
                {
                    { 5, "327 Tay Street", "Cable Court" },
                    { 6, "292 Dee Street", "Ashford Motor" },
                    { 7, "60 Glengarry Crescent", "Eastern Suburbs" },
                    { 8, "315 Centre Street", "Newfield Tavern" },
                    { 9, "Sydney Street", "Northern Tavern" },
                    { 10, "Dee Street", "Homestead" },
                    { 11, "181 North Road", "Waikiwi Tavern" },
                    { 12, "28 Cnr Dee &, Leet Street", "Lone Star" },
                    { 13, "410 Elles Road", "Southland Tavern" },
                    { 14, "38 Dee Street", "Speights Alehouse" },
                    { 15, "90 Dee Street", "Waxys" },
                    { 16, "123456789", "South City Liquorland" },
                    { 17, "Cnr Elles Rd &, Tweed Street", "Collingwood Super Liquor" },
                    { 18, "12 Bamborough Street", "East End Bottlestore" },
                    { 19, "8 Windsor Street", "Windsor Bottlestore" },
                    { 20, "406 Elles Road", "Southland Super Liquor" },
                    { 21, "35 East Road", "Ascot Sports Bar" },
                    { 22, "252 Dee Street", "Centrepoint Liquorland" },
                    { 23, "59 Dee Street", "The Langlands" },
                    { 24, "228 Elles Road", "Workshop Hours" }
                });

            migrationBuilder.InsertData(
                table: "FinancialPeriod",
                columns: new[] { "FinancialPeriodID", "EndDate", "Month", "StartDate", "Weeks", "Year" },
                values: new object[,]
                {
                    { 1, "05/01/2022 00:00", "April", "03/27/2022 00:00", 5, 2022 },
                    { 2, "05/29/2022 00:00", "May", "05/01/2022 00:00", 4, 2022 },
                    { 3, "06/26/2022 00:00", "June", "05/29/2022 00:00", 4, 2022 },
                    { 4, "07/31/2022 00:00", "July", "06/26/2022 00:00", 5, 2022 },
                    { 5, "08/28/2022 00:00", "August", "07/31/2022 00:00", 4, 2022 },
                    { 6, "10/02/2022 00:00", "September", "08/28/2022 00:00", 5, 2022 },
                    { 7, "10/30/2022 00:00", "October", "10/02/2022 00:00", 4, 2022 },
                    { 8, "11/27/2022 00:00", "November", "10/30/2022 00:00", 4, 2022 },
                    { 9, "01/01/2023 00:00", "December", "11/27/2022 00:00", 5, 2022 },
                    { 10, "01/29/2023 00:00", "January", "01/01/2023 00:00", 4, 2022 },
                    { 11, "02/26/2023 00:00", "February", "01/29/2023 00:00", 4, 2022 },
                    { 12, "04/02/2023 00:00", "March", "02/26/2023 00:00", 4, 2022 }
                });

            migrationBuilder.InsertData(
                table: "Job",
                columns: new[] { "JobID", "AccountID", "EstablishmentID", "FinancialPeriodID", "FinishTime", "Hours", "HoursOT", "IsCompleted", "Notes", "StartTime", "WorkTypeID" },
                values: new object[,]
                {
                    { 1, 1, 16, 6, "08/29/2022 10:45", 5.25, 0.0, true, "Just started painting the entirety of south city liquorland", "08/29/2022 05:30", 1 },
                    { 2, 1, 16, 6, "08/29/2022 17:00", 4.0, 0.0, true, "Continued painting the entirety of south city liquorland", "08/29/2022 13:00", 1 },
                    { 3, 1, 16, 6, "09/15/2023 12:45", 6.75, 0.0, true, "Continued painting the entirety of south city liquorland", "09/15/2022 06:00", 1 },
                    { 4, 1, 16, 6, "09/17/2022 09:00", 3.0, 0.0, true, "Just finished painting the entirety of south city liquorland by myself", "09/17/2022 06:00", 1 },
                    { 5, 2, 15, 4, "06/13/2022 13:00", 8.0, 0.0, true, "Just finished building some chairs for waxys", "06/13/2022 05:00", 2 },
                    { 6, 2, 15, 4, "06/14/2022 10:00", 5.0, 0.0, true, "Just started building some tables for waxys", "06/14/2022 05:00", 2 },
                    { 7, 2, 15, 4, "06/15/2022 11:00", 3.5, 0.0, true, "Just finished building some tables for waxys", "06/15/2022 07:30", 2 },
                    { 8, 3, 1, 10, "01/04/2023 13:00", 7.5, 0.0, true, "Did some plumbing out at the ascot", "01/04/2023 05:30", 3 },
                    { 9, 3, 1, 10, "01/06/2023 13:00", 8.0, 0.0, true, "Did some plumbing out at the ascot, a pipe under the sink was leaking", "01/06/2023 05:00", 3 },
                    { 10, 6, 11, 3, "06/16/2022 18:00", 5.0, 0.0, true, "Started some wiring out at the kiwi", "06/16/2022 13:00", 4 }
                });

            migrationBuilder.InsertData(
                table: "Job",
                columns: new[] { "JobID", "AccountID", "EstablishmentID", "FinancialPeriodID", "FinishTime", "Hours", "HoursOT", "IsCompleted", "Notes", "StartTime", "WorkTypeID" },
                values: new object[] { 11, 6, 11, 3, "06/18/2022 17:00", 5.0, 0.0, true, "Finished the wiring out at the kiwi", "06/18/2022 12:00", 4 });

            migrationBuilder.InsertData(
                table: "Trade",
                columns: new[] { "TradeID", "Type" },
                values: new object[,]
                {
                    { 1, "Painter" },
                    { 2, "Carpenter" },
                    { 3, "Plumber" },
                    { 4, "Beer Serviceman" },
                    { 5, "Electrican" },
                    { 6, "Gardener" }
                });

            migrationBuilder.InsertData(
                table: "WorkType",
                columns: new[] { "WorkTypeID", "ChargeCodeID", "Type" },
                values: new object[,]
                {
                    { 1, 1, "Painting" },
                    { 2, 1, "Building" },
                    { 3, 1, "Plumbing" },
                    { 4, 1, "Wiring" },
                    { 5, 1, "Gardening" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "ChargeCode");

            migrationBuilder.DropTable(
                name: "Establishment");

            migrationBuilder.DropTable(
                name: "FinancialPeriod");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Trade");

            migrationBuilder.DropTable(
                name: "WorkType");
        }
    }
}
