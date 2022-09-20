namespace _701_WebAPI.Models
{
    public class EstablishmentCode
    {
        [Key]
        public int EstablishmentCodeID { get; set; }

        //----- relationships -----
        public int EstablishmentID { get; set; }
        public int ChargeCodeID { get; set; }

    }
}
