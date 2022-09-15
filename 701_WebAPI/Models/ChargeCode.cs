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
