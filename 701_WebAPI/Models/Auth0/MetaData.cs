using Newtonsoft.Json;

namespace _701_WebAPI.Models.Auth0
{
    public class MetaData
    {
        //employee
        [JsonProperty("rate")]
        public decimal Rate { get; set; }
        [JsonProperty("rateOT")]
        public decimal RateOT { get; set; }
        [JsonProperty("tradeID")]
        public int TradeID { get; set; }

        //establishment manager
        [JsonProperty("establishmentID")]
        public int EstablishmentID { get; set; }
    }
}
