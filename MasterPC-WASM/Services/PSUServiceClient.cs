using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.View_Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace MasterPC_WASM.Services
{
    public interface IPSUService
    {
        public Task<List<PSUVM>> GetPSUsAsync();
        public Task<PSUVM> GetPSUByIdAsync(string id);
        public Task<string> AddPSUAsync(PSUVM psuVM);
        public Task<List<string>> AddPSUsAsync(List<PSUVM> PSUs);
    }
    public class PSUServiceClient(HttpClient httpClient, AuthenticationStateProvider authState) : IPSUService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider = authState;

        public async Task<List<PSUVM>> GetPSUsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PSUVM>>("api/psus");
        }

        public async Task<PSUVM> GetPSUByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<PSUVM>($"api/psu/{id}");
        }

        [Authorize(Roles = "superuser")]
        public async Task<string> AddPSUAsync(PSUVM psuVM)
        {
            var user = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User;
            var userId = user.Identities.First().Claims.First(c => c.Type == "oid").Value;

            var response = await _httpClient.PostAsJsonAsync($"api/psu/{userId}", psuVM);

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

        public async Task<List<string>> AddPSUsAsync(List<PSUVM> PSUs)
        {
            var user = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User;
            var userId = user.Identities.First().Claims.First(c => c.Type == "oid").Value;

            var response = await _httpClient.PostAsJsonAsync($"api/psus/{userId}", PSUs);

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
