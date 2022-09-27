global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
using _701_WebAPI.Models.DTO;

namespace _701_WebAPI.Models
{
    public class Account
    {
        public string AccountID { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public decimal? Rate { get; set; } //employee
        public decimal? RateOT { get; set; } //employee

        //----- relationships -----
        public int? TradeID { get; set; } //employee
        public int? EstablishmentID { get; set; } //establishment manager

        //----- DTO -----
        public List<JobSheet>? JobSheets { get; set; }
    }
}
