using Newtonsoft.Json;

namespace _701_WebAPI.Models.Auth0
{
    public class AccountAuth0
    {
        [JsonProperty("user_id")]
        public string AccountID { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("user_metadata")]
        public MetaData? MetaData { get; set; }

    }
}
