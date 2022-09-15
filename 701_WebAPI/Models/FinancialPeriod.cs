namespace _701_WebAPI.Models
{
    public class FinancialPeriod
    {
        [Key]
        public int FinancialPeriodID { get; set; }
        public string? Month { get; set; }
        public int Weeks { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int Year { get; set; }

    }
}
