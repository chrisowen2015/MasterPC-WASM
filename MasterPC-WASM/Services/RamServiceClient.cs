using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.View_Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace MasterPC_WASM.Services
{
    public interface IRAMService
    {
        public Task<List<RAMVM>> GetRamsAsync();
        public Task<RAMVM> GetRamByIdAsync(string id);
        public Task<string> AddRamAsync(RAMVM ramVM);
        public Task<List<string>> AddRamsAsync(List<RAMVM> Rams);
    }
    public class RAMServiceClient(HttpClient httpClient, AuthenticationStateProvider authState) : IRAMService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider = authState;

        public async Task<List<RAMVM>> GetRamsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<RAMVM>>("api/rams");
        }

        public async Task<RAMVM> GetRamByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<RAMVM>($"api/ram/{id}");
        }

        [Authorize(Roles = "superuser")]
        public async Task<string> AddRamAsync(RAMVM ramVM)
        {
            var user = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User;
            var userId = user.Identities.First().Claims.First(c => c.Type == "oid").Value;

            var response = await _httpClient.PostAsJsonAsync($"api/ram/{userId}", ramVM);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                string id = response.Content.ReadAsStringAsync().Result.ToString();

                return id;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<string>> AddRamsAsync(List<RAMVM> Rams)
        {
            var user = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User;
            var userId = user.Identities.First().Claims.First(c => c.Type == "oid").Value;

            var response = await _httpClient.PostAsJsonAsync($"api/rams/{userId}", Rams);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                List<string> ids = JsonSerializer.Deserialize<List<string>>(response.Content.ReadAsStringAsync().Result.ToString());

                return ids;
            }
            else
            {
                return null;
            }
        }

    }
}
