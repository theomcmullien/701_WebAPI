namespace _701_WebAPI.Models.DTO
{
    public class JobSheet
    {
        public string? Filename { get; set; }
        public string? DateCompleted { get; set; }
        public List<Job>? Jobs { get; set; }

    }
}
