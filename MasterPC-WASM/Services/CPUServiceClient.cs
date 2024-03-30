using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.View_Models;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace MasterPC_WASM.Services
{
    public interface ICPUService
    {
        public Task<List<CPUVM>> GetCPUsAsync();
        public Task<CPUVM> GetCPUByIdAsync(string id);
        public Task<string> AddCPUAsync(CPUVM cpu);
        public Task<List<string>> AddCPUsAsync(List<CPUVM> CPUs);
    }
    public class CPUServiceClient(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider) : ICPUService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;

        public async Task<List<CPUVM>> GetCPUsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CPUVM>>("api/cpus");
        }

        public async Task<CPUVM> GetCPUByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<CPUVM>($"api/cpu/{id}");
        }

        [Authorize(Roles = "superuser")]
        public async Task<string> AddCPUAsync(CPUVM cpu)
        {
            var user = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User;
            var userId = user.Identities.First().Claims.First(c => c.Type == "oid").Value;

            var response = await _httpClient.PostAsJsonAsync("api/cpu/{userId}", cpu);

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

        public async Task<List<string>> AddCPUsAsync(List<CPUVM> CPUs)
        {
            var user = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User;
            var userId = user.Identities.First().Claims.First(c => c.Type == "oid").Value;

            var response = await _httpClient.PostAsJsonAsync($"api/cpus/{userId}", CPUs);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                string idJSON = response.Content.ReadAsStringAsync().Result;
                List<string> ids = JsonSerializer.Deserialize<List<string>>(idJSON);

                return ids;
            }
            else
            {
                return null;
            }
        }
    }
}
