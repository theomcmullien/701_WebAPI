using Newtonsoft.Json;

namespace _701_WebAPI.Models.Auth0
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        public override string ToString()
        {
            return $"{TokenType} {AccessToken}";
        }
    }
}
