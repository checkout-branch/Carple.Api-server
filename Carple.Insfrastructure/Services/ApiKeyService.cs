using Carple.Application.Interfaces.Services;
using Carple.Application.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text;

namespace Carple.Insfrastructure.Services
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly HttpClient _httpClient;
        private readonly VaultSettings _settings;

        public ApiKeyService(HttpClient httpClient, IOptions<VaultSettings> options)
        {
            _httpClient = httpClient;
            _settings = options.Value;
        }

        public async Task<bool> ValidateKeyAsync(string apiKey)
        {
            try
            {
                var body = new StringContent($"\"{apiKey}\"", Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_settings.ValidateUrl, body);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[ApiKeyService] Validation failed. StatusCode: {response.StatusCode}");
                    return false;
                }

                var result = await response.Content.ReadFromJsonAsync<ValidateKeyResponse>();
                return result?.IsValid ?? false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ApiKeyService] Exception: {ex.Message}");
                return false;
            }
        }

        private class ValidateKeyResponse
        {
            public bool IsValid { get; set; }
        }
    }
}
