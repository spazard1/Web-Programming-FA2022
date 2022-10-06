using Mario.Entities;
using Newtonsoft.Json;
using Polly;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;

namespace Mario.Services
{
    public class ExternalMarioService
    {

        private readonly HttpClient httpClient = new();

        public async Task<MarioEntity?> GetMarioLevelStatusAsync()
        {
            var policy = Policy.HandleInner<HttpRequestException>(ex =>
            {
                return ex?.StatusCode == HttpStatusCode.ServiceUnavailable;
            }).WaitAndRetryAsync(5, retryAttempt =>
                TimeSpan.FromMilliseconds(100 * Math.Pow(2, retryAttempt))
            ,(ex, timeSpan, context) =>
            {
                Debug.WriteLine("We are trying again.");
            });

            string? responseString = null;

            await policy.ExecuteAsync(async () =>
            {
                var response = await httpClient.GetAsync("https://webprogrammingmario.azurewebsites.net/api/mario/jump");
                responseString = await response.Content.ReadAsStringAsync();
            });

            if (responseString == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<MarioEntity?>(responseString);
        }
    }
}
