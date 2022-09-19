using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _701_WebAPI.Models;

namespace _701_WebAPI.Data
{
    public class _701_WebAPIContext : DbContext
    {
        public _701_WebAPIContext (DbContextOptions<_701_WebAPIContext> options) : base(options)
        {
        }

        public DbSet<Account>? Account { get; set; }
        public DbSet<ChargeCode>? ChargeCode { get; set; }
        public DbSet<Establishment>? Establishment { get; set; }
        public DbSet<FinancialPeriod>? FinancialPeriod { get; set; }
        public DbSet<Job>? Job { get; set; }
        public DbSet<Trade>? Trade { get; set; }
        public DbSet<WorkType>? WorkType { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // -------------------------------------------------- Account --------------------------------------------------
            builder.Entity<Account>().HasData(
                /// -------------------- Employee --------------------
                new Account()
                {
                    AccountID = 1,
                    Firstname = "Rickelle",
                    Lastname = "Dempster",
                    Email = "rdempster@gmail.com",
                    Password = "123",
                    Role = "Employee",
                    Rate = 50.00m,
                    RateOT = 0m,
                    TradeID = 1,
                    EstablishmentID = null,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 2,
                    Firstname = "Wayne",
                    Lastname = "Dimmock",
                    Email = "wdimmock@gmail.com",
                    Password = "123",
                    Role = "Employee",
                    Rate = 50.00m,
                    RateOT = 56.00m,
                    TradeID = 2,
                    EstablishmentID = null,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 3,
                    Firstname = "Brent",
                    Lastname = "Mallon",
                    Email = "bmallon@gmail.com",
                    Password = "123",
                    Role = "Employee",
                    Rate = 50.00m,
                    RateOT = 66.00m,
                    TradeID = 3,
                    EstablishmentID = null,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 4,
                    Firstname = "Logan",
                    Lastname = "Milne",
                    Email = "lmilne@gmail.com",
                    Password = "123",
                    Role = "Employee",
                    Rate = 50.00m,
                    RateOT = 0m,
                    TradeID = 1,
                    EstablishmentID = null,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 5,
                    Firstname = "Jim",
                    Lastname = "Scully",
                    Email = "jscully@gmail.com",
                    Password = "123",
                    Role = "Employee",
                    Rate = 50.00m,
                    RateOT = 0m,
                    TradeID = 4,
                    EstablishmentID = null,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 6,
                    Firstname = "Brian",
                    Lastname = "Walker",
                    Email = "bwalker@gmail.com",
                    Password = "123",
                    Role = "Employee",
                    Rate = 52.00m,
                    RateOT = 0m,
                    TradeID = 5,
                    EstablishmentID = null,
                    JobSheets = null
                },
                /// -------------------- Establishment Manager --------------------
                new Account()
                {
                    AccountID = 7,
                    Firstname = "Dean",
                    Lastname = "Nicol",
                    Email = "dean@ascotparkhotel.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 1,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 8,
                    Firstname = "Jo",
                    Lastname = "Harris",
                    Email = "manager@kelvinhotel.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 2,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 9,
                    Firstname = "Natalie",
                    Lastname = "Gilson",
                    Email = "manager@villamotel.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 3,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 10,
                    Firstname = "Sharon",
                    Lastname = "Hinga",
                    Email = "manager@balmoralmotel.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 4,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 11,
                    Firstname = "Allie",
                    Lastname = "Adams-Bell",
                    Email = "manager@cablecourtmotel.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 5,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 12,
                    Firstname = "Lindsay",
                    Lastname = "Adams-Bell",
                    Email = "manager@cablecourtmotel.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 5,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 13,
                    Firstname = "Shelley",
                    Lastname = "Jenkins",
                    Email = "manager@ashfordmotorlodge.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 6,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 14,
                    Firstname = "James",
                    Lastname = "Wesney",
                    Email = "manager@easternsuburbs.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 7,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 15,
                    Firstname = "Jemma",
                    Lastname = "Wild",
                    Email = "manager@newfieldtavern.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 8,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 16,
                    Firstname = "Northern",
                    Lastname = "Manager",
                    Email = "manager@northerntavern.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 9,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 17,
                    Firstname = "Shanan",
                    Lastname = "Te-Maiharoa",
                    Email = "manager@avenalhomestead.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 10,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 18,
                    Firstname = "Vicky",
                    Lastname = "Cole",
                    Email = "manager@waikiwitavern.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 11,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 19,
                    Firstname = "Lonestar",
                    Lastname = "Manager",
                    Email = "manager@lonestarinv.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 12,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 20,
                    Firstname = "Jo",
                    Lastname = "Te-Maiharoa",
                    Email = "manager@southlandtavern.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 13,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 21,
                    Firstname = "Sam",
                    Lastname = "Harpur",
                    Email = "manager@speightsalehouseinv.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 14,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 22,
                    Firstname = "Kelly",
                    Lastname = "Burgess",
                    Email = "manager@waxys.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 15,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 23,
                    Firstname = "Mary-Ellen",
                    Lastname = "Vercoe",
                    Email = "manager@liquorlandsouthcity.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 16,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 24,
                    Firstname = "Warren",
                    Lastname = "Tayles",
                    Email = "manager@superliquorcollingwood.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 17,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 25,
                    Firstname = "Tracy",
                    Lastname = "Poe",
                    Email = "manager@eastendliquor.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 18,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 26,
                    Firstname = "Stephane",
                    Lastname = "Fabre",
                    Email = "manager@windsorwines.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 19,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 27,
                    Firstname = "Simon",
                    Lastname = "Paterson",
                    Email = "manager@superliquorsouthland.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 20,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 28,
                    Firstname = "Cate",
                    Lastname = "Wesney",
                    Email = "cate@ascotparkhotel.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 21,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 29,
                    Firstname = "Nicola",
                    Lastname = "Moss",
                    Email = "manager@liquorlandcentrepoint.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 22,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 30,
                    Firstname = "Bryan",
                    Lastname = "Townley",
                    Email = "bryan@thelanglands.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 23,
                    JobSheets = null
                },
                new Account()
                {
                    AccountID = 31,
                    Firstname = "Bevan",
                    Lastname = "Thompson",
                    Email = "bevan@ilt.co.nz",
                    Password = "123",
                    Role = "Establishment Manager",
                    Rate = null,
                    RateOT = null,
                    TradeID = null,
                    EstablishmentID = 24,
                    JobSheets = null
                }
            );
            // -------------------------------------------------- ChargeCode --------------------------------------------------
            builder.Entity<ChargeCode>().HasData(
                new ChargeCode()
                {
                    ChargeCodeID = 1,
                    Name = "Repairs & Maintenance",
                    Code = "7471",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 2,
                    Name = "Redevelopment",
                    Code = "9891800",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 3,
                    Name = "COAL BOILER",
                    Code = "746211",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 4,
                    Name = "COAL FEED EQUIPMENT",
                    Code = "746212",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 5,
                    Name = "DIESEL BOILER",
                    Code = "746213",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 6,
                    Name = "STEAM BOILER",
                    Code = "746214",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 7,
                    Name = "ELECTRIC BOILER",
                    Code = "746215",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 8,
                    Name = "HEAT CIRCULATION PUMPS",
                    Code = "746221",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 9,
                    Name = "HEATING MAINTENANCE",
                    Code = "746222",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 10,
                    Name = "VENTILATION - EXTRACTION",
                    Code = "746231",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 11,
                    Name = "VENTILATION - AIR CIRCULATION",
                    Code = "746232",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 12,
                    Name = "SWITCHBOARD MAINTENANCE",
                    Code = "746241",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 13,
                    Name = "LOAD SHEDDING EQUIPMENT",
                    Code = "746242",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 14,
                    Name = "ALL APPLIANCE MAINTENANCE",
                    Code = "746243",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 15,
                    Name = "COMPUTER EQUIPMENT",
                    Code = "746244",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 16,
                    Name = "RADIO AND TV ELECTRONIC",
                    Code = "746245",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 17,
                    Name = "P.A.B.X and TELEPHONE",
                    Code = "746246",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 18,
                    Name = "TILL MAINTENANCE",
                    Code = "746247",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 19,
                    Name = "LIGHT BULB COST",
                    Code = "746248",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 20,
                    Name = "EMERGENCY GENERATOR COSTS",
                    Code = "746249",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 21,
                    Name = "ROOM MAINTENANCE",
                    Code = "746251",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 22,
                    Name = "KITCHEN MAINTENANCE",
                    Code = "746252",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 23,
                    Name = "BAR MAINTENANCE",
                    Code = "746253",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 24,
                    Name = "BOTTLE STORE MAINTENANCE",
                    Code = "746254",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 25,
                    Name = "RESTAURANT MAINTENANCE",
                    Code = "746255",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 26,
                    Name = "EXTERIOR MAINTENANCE",
                    Code = "746256",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 27,
                    Name = "RECEPTION & FOYER MAINTENANCE ",
                    Code = "746257",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 28,
                    Name = "CONFERENCE ROOM MAINTENANCE",
                    Code = "746258",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 29,
                    Name = "MANAGER RESIDENCE MAINTENANCE",
                    Code = "746259",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 30,
                    Name = "SERVICE AREA MAINTENANCE",
                    Code = "746260",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 31,
                    Name = "KELVIN 7th FLOOR",
                    Code = "746261",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 32,
                    Name = "SHOP MAINTENANCE",
                    Code = "746262",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 33,
                    Name = "REFRIGERATION LIQUOR",
                    Code = "746270",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 34,
                    Name = "REFRIGERATION FOOD",
                    Code = "746271",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 35,
                    Name = "CARPET & FLOOR CLEANING",
                    Code = "746272",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 36,
                    Name = "SKIP BIN COSTS",
                    Code = "746273",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 37,
                    Name = "PEST CONTROL",
                    Code = "746274",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 38,
                    Name = "WINDOW CLEANING",
                    Code = "746275",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 39,
                    Name = "BUILDING W.O.F.",
                    Code = "746276",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 40,
                    Name = "N.Z. FIRE SERVICE COSTS",
                    Code = "746277",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 41,
                    Name = "LIFT MAINTENANCE",
                    Code = "746278",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 42,
                    Name = "FIRE ALARM CALL OUTS",
                    Code = "746279",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 43,
                    Name = "BEER LINE CLEANING",
                    Code = "746280",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 44,
                    Name = "TOILET FLUSH VALVES",
                    Code = "746281",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 45,
                    Name = "PROMOTIONAL",
                    Code = "746291",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 46,
                    Name = "NON BUDGET COSTS",
                    Code = "7685",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 47,
                    Name = "NURSERY COSTS - LAWNS",
                    Code = "746296",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 48,
                    Name = "NURSERY COSTS - GARDENS",
                    Code = "746297",
                },
                new ChargeCode()
                {
                    ChargeCodeID = 49,
                    Name = "OTHER",
                    Code = "747500",
                }
            );
            // -------------------------------------------------- Establishment --------------------------------------------------
            builder.Entity<Establishment>().HasData(
                new Establishment()
                {
                    EstablishmentID = 1,
                    Name = "Ascot",
                    Address = "35 East Road"
                },
                new Establishment()
                {
                    EstablishmentID = 2,
                    Name = "Kelvin Hotel",
                    Address = "20 Kelvin Street"
                },
                new Establishment()
                {
                    EstablishmentID = 3,
                    Name = "Homestead Villa",
                    Address = "329 Dee Street"
                },
                new Establishment()
                {
                    EstablishmentID = 4,
                    Name = "Balmoral Lodge",
                    Address = "256 Tay Street"
                },
                new Establishment()
                {
                    EstablishmentID = 5,
                    Name = "Cable Court",
                    Address = "327 Tay Street"
                },
                new Establishment()
                {
                    EstablishmentID = 6,
                    Name = "Ashford Motor",
                    Address = "292 Dee Street"
                },
                new Establishment()
                {
                    EstablishmentID = 7,
                    Name = "Eastern Suburbs",
                    Address = "60 Glengarry Crescent"
                },
                new Establishment()
                {
                    EstablishmentID = 8,
                    Name = "Newfield Tavern",
                    Address = "315 Centre Street"
                },
                new Establishment()
                {
                    EstablishmentID = 9,
                    Name = "Northern Tavern",
                    Address = "Sydney Street"
                },
                new Establishment()
                {
                    EstablishmentID = 10,
                    Name = "Homestead",
                    Address = "Dee Street"
                },
                new Establishment()
                {
                    EstablishmentID = 11,
                    Name = "Waikiwi Tavern",
                    Address = "181 North Road"
                },
                new Establishment()
                {
                    EstablishmentID = 12,
                    Name = "Lone Star",
                    Address = "28 Cnr Dee &, Leet Street"
                },
                new Establishment()
                {
                    EstablishmentID = 13,
                    Name = "Southland Tavern",
                    Address = "410 Elles Road"
                },
                new Establishment()
                {
                    EstablishmentID = 14,
                    Name = "Speights Alehouse",
                    Address = "38 Dee Street"
                },
                new Establishment()
                {
                    EstablishmentID = 15,
                    Name = "Waxys",
                    Address = "90 Dee Street"
                },
                new Establishment()
                {
                    EstablishmentID = 16,
                    Name = "South City Liquorland",
                    Address = "123456789"
                },
                new Establishment()
                {
                    EstablishmentID = 17,
                    Name = "Collingwood Super Liquor",
                    Address = "Cnr Elles Rd &, Tweed Street"
                },
                new Establishment()
                {
                    EstablishmentID = 18,
                    Name = "East End Bottlestore",
                    Address = "12 Bamborough Street"
                },
                new Establishment()
                {
                    EstablishmentID = 19,
                    Name = "Windsor Bottlestore",
                    Address = "8 Windsor Street"
                },
                new Establishment()
                {
                    EstablishmentID = 20,
                    Name = "Southland Super Liquor",
                    Address = "406 Elles Road"
                },
                new Establishment()
                {
                    EstablishmentID = 21,
                    Name = "Ascot Sports Bar",
                    Address = "35 East Road"
                },
                new Establishment()
                {
                    EstablishmentID = 22,
                    Name = "Centrepoint Liquorland",
                    Address = "252 Dee Street"
                },
                new Establishment()
                {
                    EstablishmentID = 23,
                    Name = "The Langlands",
                    Address = "59 Dee Street"
                },
                new Establishment()
                {
                    EstablishmentID = 24,
                    Name = "Workshop Hours",
                    Address = "228 Elles Road"
                }
            );
            // -------------------------------------------------- FinancialPeriod --------------------------------------------------
            builder.Entity<FinancialPeriod>().HasData(
                new FinancialPeriod()
                {
                    FinancialPeriodID = 1,
                    Month = "April",
                    Weeks = 5,
                    StartDate = "03/27/2022 00:00",
                    EndDate = "05/01/2022 00:00",
                    Year = 2022,
                },
                new FinancialPeriod()
                {
                    FinancialPeriodID = 2,
                    Month = "May",
                    Weeks = 4,
                    StartDate = "05/01/2022 00:00",
                    EndDate = "05/29/2022 00:00",
                    Year = 2022,
                },
                new FinancialPeriod()
                {
                    FinancialPeriodID = 3,
                    Month = "June",
                    Weeks = 4,
                    StartDate = "05/29/2022 00:00",
                    EndDate = "06/26/2022 00:00",
                    Year = 2022,
                },
                new FinancialPeriod()
                {
                    FinancialPeriodID = 4,
                    Month = "July",
                    Weeks = 5,
                    StartDate = "06/26/2022 00:00",
                    EndDate = "07/31/2022 00:00",
                    Year = 2022,
                },
                new FinancialPeriod()
                {
                    FinancialPeriodID = 5,
                    Month = "August",
                    Weeks = 4,
                    StartDate = "07/31/2022 00:00",
                    EndDate = "08/28/2022 00:00",
                    Year = 2022,
                },
                new FinancialPeriod()
                {
                    FinancialPeriodID = 6,
                    Month = "September",
                    Weeks = 5,
                    StartDate = "08/28/2022 00:00",
                    EndDate = "10/02/2022 00:00",
                    Year = 2022,
                },
                new FinancialPeriod()
                {
                    FinancialPeriodID = 7,
                    Month = "October",
                    Weeks = 4,
                    StartDate = "10/02/2022 00:00",
                    EndDate = "10/30/2022 00:00",
                    Year = 2022,
                },
                new FinancialPeriod()
                {
                    FinancialPeriodID = 8,
                    Month = "November",
                    Weeks = 4,
                    StartDate = "10/30/2022 00:00",
                    EndDate = "11/27/2022 00:00",
                    Year = 2022,
                },
                new FinancialPeriod()
                {
                    FinancialPeriodID = 9,
                    Month = "December",
                    Weeks = 5,
                    StartDate = "11/27/2022 00:00",
                    EndDate = "01/01/2023 00:00",
                    Year = 2022,
                },
                new FinancialPeriod()
                {
                    FinancialPeriodID = 10,
                    Month = "January",
                    Weeks = 4,
                    StartDate = "01/01/2023 00:00",
                    EndDate = "01/29/2023 00:00",
                    Year = 2022,
                },
                new FinancialPeriod()
                {
                    FinancialPeriodID = 11,
                    Month = "February",
                    Weeks = 4,
                    StartDate = "01/29/2023 00:00",
                    EndDate = "02/26/2023 00:00",
                    Year = 2022,
                },
                new FinancialPeriod()
                {
                    FinancialPeriodID = 12,
                    Month = "March",
                    Weeks = 4,
                    StartDate = "02/26/2023 00:00",
                    EndDate = "04/02/2023 00:00",
                    Year = 2022,
                }
            );
            // -------------------------------------------------- Job --------------------------------------------------
            builder.Entity<Job>().HasData(
                /// -------------------- Employee 1 --------------------
                new Job()
                {
                    JobID = 1,
                    StartTime = "08/29/2022 05:30",
                    FinishTime = "08/29/2022 10:45",
                    Hours = 5.25,
                    HoursOT = 0,
                    Notes = "Just started painting the entirety of south city liquorland",
                    IsCompleted = true,
                    WorkTypeID = 1,
                    FinancialPeriodID = 6,
                    EstablishmentID = 16,
                    AccountID = 1,
                },
                new Job()
                {
                    JobID = 2,
                    StartTime = "08/29/2022 13:00",
                    FinishTime = "08/29/2022 17:00",
                    Hours = 4,
                    HoursOT = 0,
                    Notes = "Continued painting the entirety of south city liquorland",
                    IsCompleted = true,
                    WorkTypeID = 1,
                    FinancialPeriodID = 6,
                    EstablishmentID = 16,
                    AccountID = 1,
                },
                new Job()
                {
                    JobID = 3,
                    StartTime = "09/15/2022 06:00",
                    FinishTime = "09/15/2023 12:45",
                    Hours = 6.75,
                    HoursOT = 0,
                    Notes = "Continued painting the entirety of south city liquorland",
                    IsCompleted = true,
                    WorkTypeID = 1,
                    FinancialPeriodID = 6,
                    EstablishmentID = 16,
                    AccountID = 1,
                },
                new Job()
                {
                    JobID = 4,
                    StartTime = "09/17/2022 06:00",
                    FinishTime = "09/17/2022 09:00",
                    Hours = 3,
                    HoursOT = 0,
                    Notes = "Just finished painting the entirety of south city liquorland by myself",
                    IsCompleted = true,
                    WorkTypeID = 1,
                    FinancialPeriodID = 6,
                    EstablishmentID = 16,
                    AccountID = 1,
                },
                /// -------------------- Employee 2 --------------------
                new Job()
                {
                    JobID = 5,
                    StartTime = "06/13/2022 05:00",
                    FinishTime = "06/13/2022 13:00",
                    Hours = 8,
                    HoursOT = 0,
                    Notes = "Just finished building some chairs for waxys",
                    IsCompleted = true,
                    WorkTypeID = 2,
                    FinancialPeriodID = 4,
                    EstablishmentID = 15,
                    AccountID = 2,
                },
                new Job()
                {
                    JobID = 6,
                    StartTime = "06/14/2022 05:00",
                    FinishTime = "06/14/2022 10:00",
                    Hours = 5,
                    HoursOT = 0,
                    Notes = "Just started building some tables for waxys",
                    IsCompleted = true,
                    WorkTypeID = 2,
                    FinancialPeriodID = 4,
                    EstablishmentID = 15,
                    AccountID = 2,
                },
                new Job()
                {
                    JobID = 7,
                    StartTime = "06/15/2022 07:30",
                    FinishTime = "06/15/2022 11:00",
                    Hours = 3.5,
                    HoursOT = 0,
                    Notes = "Just finished building some tables for waxys",
                    IsCompleted = true,
                    WorkTypeID = 2,
                    FinancialPeriodID = 4,
                    EstablishmentID = 15,
                    AccountID = 2,
                },
                /// -------------------- Employee 3 --------------------
                new Job()
                {
                    JobID = 8,
                    StartTime = "01/04/2023 05:30",
                    FinishTime = "01/04/2023 13:00",
                    Hours = 7.5,
                    HoursOT = 0,
                    Notes = "Did some plumbing out at the ascot",
                    IsCompleted = true,
                    WorkTypeID = 3,
                    FinancialPeriodID = 10,
                    EstablishmentID = 1,
                    AccountID = 3,
                },
                new Job()
                {
                    JobID = 9,
                    StartTime = "01/06/2023 05:00",
                    FinishTime = "01/06/2023 13:00",
                    Hours = 8,
                    HoursOT = 0,
                    Notes = "Did some plumbing out at the ascot, a pipe under the sink was leaking",
                    IsCompleted = true,
                    WorkTypeID = 3,
                    FinancialPeriodID = 10,
                    EstablishmentID = 1,
                    AccountID = 3,
                },
                /// -------------------- Employee 4 --------------------
                new Job()
                {
                    JobID = 10,
                    StartTime = "06/16/2022 13:00",
                    FinishTime = "06/16/2022 18:00",
                    Hours = 5,
                    HoursOT = 0,
                    Notes = "Started some wiring out at the kiwi",
                    IsCompleted = true,
                    WorkTypeID = 4,
                    FinancialPeriodID = 3,
                    EstablishmentID = 11,
                    AccountID = 6,
                },
                new Job()
                {
                    JobID = 11,
                    StartTime = "06/18/2022 12:00",
                    FinishTime = "06/18/2022 17:00",
                    Hours = 5,
                    HoursOT = 0,
                    Notes = "Finished the wiring out at the kiwi",
                    IsCompleted = true,
                    WorkTypeID = 4,
                    FinancialPeriodID = 3,
                    EstablishmentID = 11,
                    AccountID = 6,
                }
            );
            // -------------------------------------------------- Trade --------------------------------------------------
            builder.Entity<Trade>().HasData(
                new Trade()
                {
                    TradeID = 1,
                    Type = "Painter"
                },
                new Trade()
                {
                    TradeID = 2,
                    Type = "Carpenter"
                },
                new Trade()
                {
                    TradeID = 3,
                    Type = "Plumber"
                },
                new Trade()
                {
                    TradeID = 4,
                    Type = "Beer Serviceman"
                },
                new Trade()
                {
                    TradeID = 5,
                    Type = "Electrican"
                },
                new Trade()
                {
                    TradeID = 6,
                    Type = "Gardener"
                }
            );
            // -------------------------------------------------- WorkType --------------------------------------------------
            builder.Entity<WorkType>().HasData(
                new WorkType()
                {
                    WorkTypeID = 1,
                    Type = "Painting",
                    ChargeCodeID = 1,
                },
                new WorkType()
                {
                    WorkTypeID = 2,
                    Type = "Building",
                    ChargeCodeID = 1,
                },
                new WorkType()
                {
                    WorkTypeID = 3,
                    Type = "Plumbing",
                    ChargeCodeID = 1,
                },
                new WorkType()
                {
                    WorkTypeID = 4,
                    Type = "Wiring",
                    ChargeCodeID = 1,
                },
                new WorkType()
                {
                    WorkTypeID = 5,
                    Type = "Gardening",
                    ChargeCodeID = 1,
                }
            );
        }
    }
}
