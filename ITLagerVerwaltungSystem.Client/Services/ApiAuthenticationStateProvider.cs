using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;

namespace ITLagerVerwaltungSystem.Client.Services
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private string? _token;

        public ApiAuthenticationStateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void SetToken(string? token)
        {
            _token = token;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void ClearToken()
        {
            _token = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (string.IsNullOrWhiteSpace(_token))
            {
                var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
                _httpClient.DefaultRequestHeaders.Authorization = null;
                return Task.FromResult(new AuthenticationState(anonymous));
            }

            try
            {
                var handler = new JwtSecurityTokenHandler();
                if (!handler.CanReadToken(_token))
                {
                    var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
                    _httpClient.DefaultRequestHeaders.Authorization = null;
                    return Task.FromResult(new AuthenticationState(anonymous));
                }
                var jwtToken = handler.ReadJwtToken(_token);
                var claims = jwtToken?.Claims ?? Array.Empty<Claim>();
                var identity = new ClaimsIdentity(claims, "jwt");
                var user = new ClaimsPrincipal(identity);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                return Task.FromResult(new AuthenticationState(user));
            }
            catch
            {
                var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
                _httpClient.DefaultRequestHeaders.Authorization = null;
                return Task.FromResult(new AuthenticationState(anonymous));
            }
        }
    }
}
