using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ITLagerVerwaltungSystem.Client.Models;

namespace ITLagerVerwaltungSystem.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", new { Email = email, Password = password });
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
                return result?.Token;
            }
            return null;
        }
    }
}
