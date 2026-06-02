using DTOLibrary;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Security.Principal;

namespace BotInterface
{
    internal class ApiClient
    {
        private readonly HttpClient _http;

        public ApiClient(IConfiguration config)
        {
            var baseUrl = config["ApiBaseUrl"]
                ?? throw new InvalidOperationException("ApiBaseUrl is not configured.");

            _http = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        public async Task<List<AccountDTO>?> GetAccountsAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<List<AccountDTO>>("/account/list");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[ApiClient] GetAccounts failed: {ex.Message}");
                return null;
            }
        }
    }
}
