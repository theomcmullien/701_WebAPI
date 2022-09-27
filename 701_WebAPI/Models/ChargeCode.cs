global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;

namespace _701_WebAPI.Models
{
    public class ChargeCode
    {
        [Key]
        public int ChargeCodeID { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }
}
