using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.View_Models;
using System.Net.Http.Json;
using System.Security.Claims;

namespace MasterPC_WASM.Services
{
    public interface ICPUService
    {
        public Task<List<CPUVM>> GetCPUsAsync();
        public Task<CPUVM> GetCPUByIdAsync(string id);
        public Task<string> AddCPUAsync(CPUVM cpu);
        public Task<bool> UpdateCPUAsync(CPUVM cpu);
        public Task<bool> DeleteCPUAsync(string id);
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

        public async Task<bool> UpdateCPUAsync(CPUVM cpu)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/CPUs/{cpu.Id}", cpu);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteCPUAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/CPUs/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
