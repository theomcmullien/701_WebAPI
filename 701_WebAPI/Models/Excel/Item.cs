namespace _701_WebAPI.Models.Excel
{
    public class Item
    {
        public int Gold { get; set; }
        public int Silver { get; set; }
        public int Bronze { get; set; }
        public string Country { get; set; }

        public Item(int gold, int silver, int bronze, string country)
        {
            Gold = gold;
            Silver = silver;
            Bronze = bronze;
            Country = country;
        }
    }
}
