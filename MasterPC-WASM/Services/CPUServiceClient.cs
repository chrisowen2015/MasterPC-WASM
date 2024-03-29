using Microsoft.AspNetCore.Authorization;
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
    public class CPUServiceClient : ICPUService
    {
        private readonly HttpClient _httpClient;

        public CPUServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public bool OnClient()
        {
            return true;
        }

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
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Role, "superuser") }));
            var response = await _httpClient.PostAsJsonAsync("api/cpu", cpu);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                Uri u = response.Headers.Location;
                string id = u.Segments.Last();
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
