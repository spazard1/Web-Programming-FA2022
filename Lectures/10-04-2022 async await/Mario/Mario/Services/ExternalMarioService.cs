using Mario.Entities;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace Mario.Services
{
    public class ExternalMarioService
    {

        private readonly HttpClient httpClient = new();

        public async Task<MarioEntity?> GetMarioLevelStatusAsync()
        {
            var response = await httpClient.GetAsync("https://webprogrammingmario.azurewebsites.net/api/mario/jump");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<MarioEntity?>(responseString);
        }
    }
}
