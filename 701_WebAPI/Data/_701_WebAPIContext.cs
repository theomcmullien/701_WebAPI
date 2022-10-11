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
        
        //public DbSet<Account>? Account { get; set; }
        public DbSet<ChargeCode>? ChargeCode { get; set; }
        public DbSet<Establishment>? Establishment { get; set; }
        public DbSet<EstablishmentCode>? EstablishmentCode { get; set; }
        public DbSet<FinancialPeriod>? FinancialPeriod { get; set; }
        public DbSet<Job>? Job { get; set; }
        public DbSet<Trade>? Trade { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // -------------------------------------------------- Account --------------------------------------------------
            //builder.Entity<Account>().HasData(
            //    /// -------------------- Employee --------------------
            //    new Account()
            //    {
            //        AccountID = "auth0|6332c7bb35450ad949086866",
            //        Firstname = "Rickelle",
            //        Lastname = "Dempster",
            //        Email = "rdempster@gmail.com",
            //        Role = "Employee",
            //        Rate = 50.00m,
            //        RateOT = 0m,
            //        TradeID = 1,
            //        EstablishmentID = null,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|634575dbe16807ccd2b3818d",
            //        Firstname = "Wayne",
            //        Lastname = "Dimmock",
            //        Email = "wdimmock@gmail.com",
            //        Role = "Employee",
            //        Rate = 50.00m,
            //        RateOT = 56.00m,
            //        TradeID = 2,
            //        EstablishmentID = null,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|634576856f00df75fed2d2ed",
            //        Firstname = "Brent",
            //        Lastname = "Mallon",
            //        Email = "bmallon@gmail.com",
            //        Role = "Employee",
            //        Rate = 50.00m,
            //        RateOT = 66.00m,
            //        TradeID = 3,
            //        EstablishmentID = null,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|6345777c6f00df75fed2d2fb",
            //        Firstname = "Logan",
            //        Lastname = "Milne",
            //        Email = "lmilne@gmail.com",
            //        Role = "Employee",
            //        Rate = 50.00m,
            //        RateOT = 0m,
            //        TradeID = 1,
            //        EstablishmentID = null,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|634577c56f00df75fed2d2fe",
            //        Firstname = "Jim",
            //        Lastname = "Scully",
            //        Email = "jscully@gmail.com",
            //        Role = "Employee",
            //        Rate = 50.00m,
            //        RateOT = 0m,
            //        TradeID = 4,
            //        EstablishmentID = null,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|634578296072ed94c436db3c",
            //        Firstname = "Brian",
            //        Lastname = "Walker",
            //        Email = "bwalker@gmail.com",
            //        Role = "Employee",
            //        Rate = 52.00m,
            //        RateOT = 0m,
            //        TradeID = 5,
            //        EstablishmentID = null,
            //        JobSheets = null
            //    },
            //    /// -------------------- Establishment Manager --------------------
            //    new Account()
            //    {
            //        AccountID = "auth0|7",
            //        Firstname = "Dean",
            //        Lastname = "Nicol",
            //        Email = "dean@ascotparkhotel.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 1,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|8",
            //        Firstname = "Jo",
            //        Lastname = "Harris",
            //        Email = "manager@kelvinhotel.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 2,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|9",
            //        Firstname = "Natalie",
            //        Lastname = "Gilson",
            //        Email = "manager@villamotel.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 3,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|10",
            //        Firstname = "Sharon",
            //        Lastname = "Hinga",
            //        Email = "manager@balmoralmotel.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 4,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|11",
            //        Firstname = "Allie",
            //        Lastname = "Adams-Bell",
            //        Email = "manager@cablecourtmotel.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 5,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|12",
            //        Firstname = "Lindsay",
            //        Lastname = "Adams-Bell",
            //        Email = "manager@cablecourtmotel.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 5,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|13",
            //        Firstname = "Shelley",
            //        Lastname = "Jenkins",
            //        Email = "manager@ashfordmotorlodge.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 6,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|14",
            //        Firstname = "James",
            //        Lastname = "Wesney",
            //        Email = "manager@easternsuburbs.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 7,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|15",
            //        Firstname = "Jemma",
            //        Lastname = "Wild",
            //        Email = "manager@newfieldtavern.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 8,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|16",
            //        Firstname = "Northern",
            //        Lastname = "Manager",
            //        Email = "manager@northerntavern.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 9,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|17",
            //        Firstname = "Shanan",
            //        Lastname = "Te-Maiharoa",
            //        Email = "manager@avenalhomestead.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 10,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|18",
            //        Firstname = "Vicky",
            //        Lastname = "Cole",
            //        Email = "manager@waikiwitavern.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 11,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|19",
            //        Firstname = "Lonestar",
            //        Lastname = "Manager",
            //        Email = "manager@lonestarinv.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 12,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|20",
            //        Firstname = "Jo",
            //        Lastname = "Te-Maiharoa",
            //        Email = "manager@southlandtavern.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 13,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|21",
            //        Firstname = "Sam",
            //        Lastname = "Harpur",
            //        Email = "manager@speightsalehouseinv.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 14,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|22",
            //        Firstname = "Kelly",
            //        Lastname = "Burgess",
            //        Email = "manager@waxys.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 15,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|23",
            //        Firstname = "Mary-Ellen",
            //        Lastname = "Vercoe",
            //        Email = "manager@liquorlandsouthcity.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 16,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|24",
            //        Firstname = "Warren",
            //        Lastname = "Tayles",
            //        Email = "manager@superliquorcollingwood.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 17,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|25",
            //        Firstname = "Tracy",
            //        Lastname = "Poe",
            //        Email = "manager@eastendliquor.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 18,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|26",
            //        Firstname = "Stephane",
            //        Lastname = "Fabre",
            //        Email = "manager@windsorwines.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 19,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|27",
            //        Firstname = "Simon",
            //        Lastname = "Paterson",
            //        Email = "manager@superliquorsouthland.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 20,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|28",
            //        Firstname = "Cate",
            //        Lastname = "Wesney",
            //        Email = "cate@ascotparkhotel.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 21,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|29",
            //        Firstname = "Nicola",
            //        Lastname = "Moss",
            //        Email = "manager@liquorlandcentrepoint.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 22,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|30",
            //        Firstname = "Bryan",
            //        Lastname = "Townley",
            //        Email = "bryan@thelanglands.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 23,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|31",
            //        Firstname = "Bevan",
            //        Lastname = "Thompson",
            //        Email = "bevan@ilt.co.nz",
            //        Role = "Establishment Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = 24,
            //        JobSheets = null
            //    },
            //    /// -------------------- Maintenance Manager --------------------
            //    new Account()
            //    {
            //        AccountID = "auth0|63338d1dae15c2aa3364d822",
            //        Firstname = "Jeoggery",
            //        Lastname = "Adonis",
            //        Email = "maintenancemanager@gmail.co.nz",
            //        Role = "Maintenance Manager",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = null,
            //        JobSheets = null
            //    },
            //    /// -------------------- Office Staff --------------------
            //    new Account()
            //    {
            //        AccountID = "auth0|63338d57ae15c2aa3364d82f",
            //        Firstname = "Jeffery",
            //        Lastname = "Sparticus",
            //        Email = "officestaff@gmail.co.nz",
            //        Role = "Office Staff",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = null,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|34",
            //        Firstname = "Office2First",
            //        Lastname = "Office2Last",
            //        Email = "office2@gmail.co.nz",
            //        Role = "Office Staff",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = null,
            //        JobSheets = null
            //    },
            //    /// -------------------- Admin --------------------
            //    new Account()
            //    {
            //        AccountID = "auth0|35",
            //        Firstname = "Adam",
            //        Lastname = "Leask",
            //        Email = "adam@ilt.co.nz",
            //        Role = "Admin",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = null,
            //        JobSheets = null
            //    },
            //    new Account()
            //    {
            //        AccountID = "auth0|36",
            //        Firstname = "Kris",
            //        Lastname = "Leatherby",
            //        Email = "kris@ilt.co.nz",
            //        Role = "Admin",
            //        Rate = null,
            //        RateOT = null,
            //        TradeID = null,
            //        EstablishmentID = null,
            //        JobSheets = null
            //    }
            //);
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
                    Name = "Ascot Hotel Motel",
                    Address = "Corner of Tay Street & Racecourse Road"
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
                    Name = "Eastern Suburbs Tavern",
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
                    Name = "Avenal Homestead",
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
                    Name = "Lone Star Cafe",
                    Address = "28 Cnr Dee & Leet Street"
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
                    Address = "Cnr Elles Rd & Tweed Street"
                },
                new Establishment()
                {
                    EstablishmentID = 17,
                    Name = "Collingwood Super Liquor",
                    Address = "44 North Road"
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
            // -------------------------------------------------- EstablishmentCode --------------------------------------------------
            builder.Entity<EstablishmentCode>().HasData(
                /// -------------------- Ascot Hotel Motel --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 1,
                    EstablishmentID = 1,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 2,
                    EstablishmentID = 1,
                    ChargeCodeID = 2
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 3,
                    EstablishmentID = 1,
                    ChargeCodeID = 43
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 4,
                    EstablishmentID = 1,
                    ChargeCodeID = 48
                },
                /// -------------------- Langlands Hotel --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 5,
                    EstablishmentID = 23,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 6,
                    EstablishmentID = 23,
                    ChargeCodeID = 2
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 7,
                    EstablishmentID = 23,
                    ChargeCodeID = 43
                },
                /// -------------------- Kelvin Hotel --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 8,
                    EstablishmentID = 2,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 9,
                    EstablishmentID = 2,
                    ChargeCodeID = 2
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 10,
                    EstablishmentID = 2,
                    ChargeCodeID = 43
                },
                /// -------------------- Homestead Villa --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 11,
                    EstablishmentID = 3,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 12,
                    EstablishmentID = 3,
                    ChargeCodeID = 2
                },
                /// -------------------- Balmoral Lodge --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 13,
                    EstablishmentID = 4,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 14,
                    EstablishmentID = 4,
                    ChargeCodeID = 2
                },
                /// -------------------- Cable Court --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 15,
                    EstablishmentID = 5,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 16,
                    EstablishmentID = 5,
                    ChargeCodeID = 2
                },
                /// -------------------- Ashford Motor --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 17,
                    EstablishmentID = 6,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 18,
                    EstablishmentID = 6,
                    ChargeCodeID = 2
                },
                /// -------------------- Eastern Suburbs Tavern --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 19,
                    EstablishmentID = 7,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 20,
                    EstablishmentID = 7,
                    ChargeCodeID = 2
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 21,
                    EstablishmentID = 7,
                    ChargeCodeID = 43
                },
                /// -------------------- Newfield Tavern --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 22,
                    EstablishmentID = 8,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 23,
                    EstablishmentID = 8,
                    ChargeCodeID = 2
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 24,
                    EstablishmentID = 8,
                    ChargeCodeID = 43
                },
                /// -------------------- Northern Tavern --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 25,
                    EstablishmentID = 9,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 26,
                    EstablishmentID = 9,
                    ChargeCodeID = 2
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 27,
                    EstablishmentID = 9,
                    ChargeCodeID = 43
                },
                /// -------------------- Avenal Homestead --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 28,
                    EstablishmentID = 10,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 29,
                    EstablishmentID = 10,
                    ChargeCodeID = 2
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 30,
                    EstablishmentID = 10,
                    ChargeCodeID = 43
                },
                /// -------------------- Waikiwi Tavern --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 31,
                    EstablishmentID = 11,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 32,
                    EstablishmentID = 11,
                    ChargeCodeID = 2
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 33,
                    EstablishmentID = 11,
                    ChargeCodeID = 43
                },
                /// -------------------- Lone Star Cafe --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 34,
                    EstablishmentID = 12,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 35,
                    EstablishmentID = 12,
                    ChargeCodeID = 2
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 36,
                    EstablishmentID = 12,
                    ChargeCodeID = 43
                },
                /// -------------------- Southland Tavern --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 37,
                    EstablishmentID = 13,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 38,
                    EstablishmentID = 13,
                    ChargeCodeID = 2
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 39,
                    EstablishmentID = 13,
                    ChargeCodeID = 43
                },
                /// -------------------- Speights Alehouse --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 40,
                    EstablishmentID = 14,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 41,
                    EstablishmentID = 14,
                    ChargeCodeID = 2
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 42,
                    EstablishmentID = 14,
                    ChargeCodeID = 43
                },
                /// -------------------- Waxys --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 43,
                    EstablishmentID = 15,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 44,
                    EstablishmentID = 15,
                    ChargeCodeID = 2
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 45,
                    EstablishmentID = 15,
                    ChargeCodeID = 43
                },
                /// -------------------- Centrepoint Liquorland --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 46,
                    EstablishmentID = 22,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 47,
                    EstablishmentID = 22,
                    ChargeCodeID = 2
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 48,
                    EstablishmentID = 22,
                    ChargeCodeID = 43
                },
                /// -------------------- South City Liquorland --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 49,
                    EstablishmentID = 16,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 50,
                    EstablishmentID = 16,
                    ChargeCodeID = 2
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 51,
                    EstablishmentID = 16,
                    ChargeCodeID = 43
                },
                /// -------------------- Collingwood Super Liquor --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 52,
                    EstablishmentID = 17,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 53,
                    EstablishmentID = 17,
                    ChargeCodeID = 2
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 54,
                    EstablishmentID = 17,
                    ChargeCodeID = 43
                },
                /// -------------------- East End Bottlestore --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 55,
                    EstablishmentID = 18,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 56,
                    EstablishmentID = 18,
                    ChargeCodeID = 2
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 57,
                    EstablishmentID = 18,
                    ChargeCodeID = 43
                },
                /// -------------------- Windsor Bottlestore --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 58,
                    EstablishmentID = 19,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 59,
                    EstablishmentID = 19,
                    ChargeCodeID = 2
                },
                /// -------------------- Southland Super Liquor --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 60,
                    EstablishmentID = 20,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 61,
                    EstablishmentID = 20,
                    ChargeCodeID = 2
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 62,
                    EstablishmentID = 20,
                    ChargeCodeID = 43
                },
                /// -------------------- Southland Super Liquor --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 63,
                    EstablishmentID = 20,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 64,
                    EstablishmentID = 20,
                    ChargeCodeID = 2
                },
                /// -------------------- Ascot Sports Bar --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 65,
                    EstablishmentID = 21,
                    ChargeCodeID = 1
                },
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 66,
                    EstablishmentID = 21,
                    ChargeCodeID = 2
                },
                /// -------------------- Workshop Hours --------------------
                new EstablishmentCode()
                {
                    EstablishmentCodeID = 67,
                    EstablishmentID = 24,
                    ChargeCodeID = 49
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
                /// -------------------- Rickelle Dempster --------------------
                new Job()
                {
                    JobID = 1,
                    StartTime = "08/29/2022 05:30",
                    FinishTime = "08/29/2022 10:45",
                    Hours = 5.25,
                    HoursOT = 0,
                    Notes = "Just started painting the entirety of south city liquorland",
                    IsCompleted = true,
                    FinancialPeriodID = 6,
                    EstablishmentID = 16,
                    ChargeCodeID = 1,
                    AccountID = "auth0|6332c7bb35450ad949086866",
                },
                new Job()
                {
                    JobID = 2,
                    StartTime = "08/29/2022 13:00",
                    FinishTime = "08/29/2022 17:00",
                    Hours = 8,
                    HoursOT = 1,
                    Notes = "Continued painting the entirety of south city liquorland",
                    IsCompleted = true,
                    FinancialPeriodID = 6,
                    EstablishmentID = 16,
                    ChargeCodeID = 1,
                    AccountID = "auth0|6332c7bb35450ad949086866",
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
                    FinancialPeriodID = 6,
                    EstablishmentID = 16,
                    ChargeCodeID = 1,
                    AccountID = "auth0|6332c7bb35450ad949086866",
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
                    FinancialPeriodID = 6,
                    EstablishmentID = 16,
                    ChargeCodeID = 1,
                    AccountID = "auth0|6332c7bb35450ad949086866",
                },
                /// -------------------- Wayne Dimmock --------------------
                new Job()
                {
                    JobID = 5,
                    StartTime = "06/13/2022 05:00",
                    FinishTime = "06/13/2022 13:00",
                    Hours = 8,
                    HoursOT = 0.5,
                    Notes = "Just finished building some chairs for waxys",
                    IsCompleted = true,
                    FinancialPeriodID = 4,
                    EstablishmentID = 15,
                    ChargeCodeID = 2,
                    AccountID = "auth0|634575dbe16807ccd2b3818d",
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
                    FinancialPeriodID = 4,
                    EstablishmentID = 15,
                    ChargeCodeID = 2,
                    AccountID = "auth0|634575dbe16807ccd2b3818d",
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
                    FinancialPeriodID = 4,
                    EstablishmentID = 15,
                    ChargeCodeID = 2,
                    AccountID = "auth0|634575dbe16807ccd2b3818d",
                },
                /// -------------------- Brent Mallon --------------------
                new Job()
                {
                    JobID = 8,
                    StartTime = "01/04/2023 05:30",
                    FinishTime = "01/04/2023 13:00",
                    Hours = 7.5,
                    HoursOT = 0,
                    Notes = "Did some plumbing out at the ascot",
                    IsCompleted = true,
                    FinancialPeriodID = 10,
                    EstablishmentID = 1,
                    ChargeCodeID = 1,
                    AccountID = "auth0|634576856f00df75fed2d2ed",
                },
                new Job()
                {
                    JobID = 9,
                    StartTime = "01/06/2023 05:00",
                    FinishTime = "01/06/2023 13:00",
                    Hours = 8,
                    HoursOT = 1.5,
                    Notes = "Did some plumbing out at the ascot, a pipe under the sink was leaking",
                    IsCompleted = true,
                    FinancialPeriodID = 10,
                    EstablishmentID = 1,
                    ChargeCodeID = 1,
                    AccountID = "auth0|634576856f00df75fed2d2ed",
                },
                /// -------------------- Brian Walker --------------------
                new Job()
                {
                    JobID = 10,
                    StartTime = "06/16/2022 13:00",
                    FinishTime = "06/16/2022 18:00",
                    Hours = 5,
                    HoursOT = 0,
                    Notes = "Started some wiring out at the kiwi",
                    IsCompleted = true,
                    FinancialPeriodID = 3,
                    EstablishmentID = 11,
                    ChargeCodeID = 1,
                    AccountID = "auth0|634578296072ed94c436db3c",
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
                    FinancialPeriodID = 3,
                    EstablishmentID = 11,
                    ChargeCodeID = 1,
                    AccountID = "auth0|634578296072ed94c436db3c",
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
        }

    }
}
