using Smart_Parking_System.Application_Layer.Dtos.Auth;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Smart_Parking_System.Infrastructure_Layer.Services
{
    public class KeycloakService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public KeycloakService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        private string Realm => _config["Keycloak:Realm"]!;
        private string BaseUrl => _config["Keycloak:AuthServerUrl"]!.TrimEnd('/');

        public async Task<bool> RegisterAsync(RegisterDTO dto)
        {

            var tokenResponse = await _httpClient.PostAsync(
                $"{BaseUrl}/realms/master/protocol/openid-connect/token",
                new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type", "password"},
                    {"client_id", "admin-cli"},
                    {"username", _config["Keycloak:AdminUser"]},
                    {"password", _config["Keycloak:AdminPassword"]}
                })
            );

            var tokenJson = JsonDocument.Parse(await tokenResponse.Content.ReadAsStringAsync());
            if (!tokenResponse.IsSuccessStatusCode) return false;

            var accessToken = tokenJson.RootElement.GetProperty("access_token").GetString();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var userObj = new
            {
                username = dto.Username,
                email = dto.Email,
                enabled = true,
                credentials = new[]
                {
                    new { type = "password", value = dto.Password, temporary = false }
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(userObj), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                $"{BaseUrl}/admin/realms/{Realm}/users",
                content
            );

            return response.IsSuccessStatusCode;
        }

        public async Task<string?> LoginAsync(LoginDTO dto)
        {
            var data = new Dictionary<string, string>
            {
                {"client_id", _config["Keycloak:ClientId"]!},
                {"client_secret", _config["Keycloak:ClientSecret"]!},
                {"grant_type", "password"},
                {"username", dto.Username},
                {"password", dto.Password}
            };

            var response = await _httpClient.PostAsync(
                $"{BaseUrl}/realms/{Realm}/protocol/openid-connect/token",
                new FormUrlEncodedContent(data)
            );

            if (!response.IsSuccessStatusCode) return null;

            var result = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            return result.RootElement.GetProperty("access_token").GetString();
        }
    }
}
