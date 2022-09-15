namespace _701_WebAPI.Models
{
    public class Trade
    {
        [Key]
        public int TradeID { get; set; }
        public string? Type { get; set; }

    }
}
