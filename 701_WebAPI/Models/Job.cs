namespace _701_WebAPI.Models
{
    public class Job
    {
        [Key]
        public int JobID { get; set; }
        public string? StartTime { get; set; }
        public string? FinishTime { get; set; }
        public double? Hours { get; set; }
        public double? HoursOT { get; set; }
        public string? Notes { get; set; }
        public bool IsCompleted { get; set; }
        public string? Area { get; set; }

        //----- relationships -----
        public int WorkTypeID { get; set; }
        public int ChargeCodeID { get; set; }
        public int? FinancialPeriodID { get; set; }
        public int EstablishmentID { get; set; }
        public int AccountID { get; set; }
        
    }
}
