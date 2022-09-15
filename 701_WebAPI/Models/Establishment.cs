namespace _701_WebAPI.Models
{
    public class Establishment
    {
        [Key]
        public int EstablishmentID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }

        //----- DTO -----
        [NotMapped]
        public string? Manager { get; set; }
        [NotMapped]
        public string? Email { get; set; }

    }
}
