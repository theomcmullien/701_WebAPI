using Newtonsoft.Json;

namespace _701_WebAPI.Models.Auth0
{
    public class AccountRole
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
