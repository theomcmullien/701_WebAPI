namespace _701_WebAPI.Models
{
    public class WorkType
    {
        [Key]
        public int WorkTypeID { get; set; }
        public string? Type { get; set; }

        //----- relationships -----
        public int ChargeCodeID { get; set; }

    }
}
