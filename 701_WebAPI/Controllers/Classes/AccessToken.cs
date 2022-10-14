using _701_WebAPI.Models.Auth0;
using Newtonsoft.Json;
using RestSharp;

namespace _701_WebAPI.Controllers.Classes
{
    public class AccessToken
    {
        public static async Task<string> GetToken()
        {
            Token token = new Token();
            using (var client = new RestClient("https://dev-bss0r74x.au.auth0.com/oauth/token"))
            {
                var request = new RestRequest();
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\"client_id\":\"E2NawyMhDqnRWI7EQ5JWOIyPi7z9zfOF\",\"client_secret\":\"jsTK_LvhjKLM4gd1XoMWG0qcySNJzecTNetLFmdHae5eIyxTdMx634AtuQfy9CRu\",\"audience\":\"https://dev-bss0r74x.au.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
                var result = await client.PostAsync(request);
                string json = result.Content.ToString();
                token = JsonConvert.DeserializeObject<Token>(json);
            }
            return token.ToString();
        }
    }
}
