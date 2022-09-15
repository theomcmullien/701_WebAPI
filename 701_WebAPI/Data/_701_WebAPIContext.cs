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
        public _701_WebAPIContext (DbContextOptions<_701_WebAPIContext> options)
            : base(options)
        {
        }
        public DbSet<_701_WebAPI.Models.Account>? Account { get; set; }
        public DbSet<_701_WebAPI.Models.ChargeCode>? ChargeCode { get; set; }
        public DbSet<_701_WebAPI.Models.Establishment>? Establishment { get; set; }
        public DbSet<_701_WebAPI.Models.FinancialPeriod>? FinancialPeriod { get; set; }
        public DbSet<_701_WebAPI.Models.Job>? Job { get; set; }
        public DbSet<_701_WebAPI.Models.Trade>? Trade { get; set; }
        public DbSet<_701_WebAPI.Models.WorkType>? WorkType { get; set; }

    }
}
