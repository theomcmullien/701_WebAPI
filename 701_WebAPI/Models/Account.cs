global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
using _701_WebAPI.Models.DTO;

namespace _701_WebAPI.Models
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Rate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? RateOT { get; set; }

        //----- relationships -----
        public int? TradeID { get; set; }
        public int? EstablishmentID { get; set; }

        //----- DTO -----
        [NotMapped]
        public List<JobSheet>? JobSheets { get; set; }
    }
}
