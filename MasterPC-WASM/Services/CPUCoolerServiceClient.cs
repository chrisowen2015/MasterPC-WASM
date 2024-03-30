using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.View_Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace MasterPC_WASM.Services
{
    public interface ICPUCoolerService
    {
        public Task<List<CPUCoolerVM>> GetCPUCoolersAsync();
        public Task<CPUCoolerVM> GetCPUCoolerByIdAsync(string id);
        public Task<string> AddCPUCoolerAsync(CPUCoolerVM cpuCoolerVM);
        public Task<List<string>> AddCPUCoolersAsync(List<CPUCoolerVM> CPUCoolers);
    }
    public class CPUCoolerServiceClient(HttpClient httpClient, AuthenticationStateProvider authState) : ICPUCoolerService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider = authState;

        public async Task<List<CPUCoolerVM>> GetCPUCoolersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CPUCoolerVM>>("api/cpu-coolers");
        }

        public async Task<CPUCoolerVM> GetCPUCoolerByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<CPUCoolerVM>($"api/cpu-cooler/{id}");
        }

        public async Task<string> AddCPUCoolerAsync(CPUCoolerVM cpuCoolerVM)
        {
            var user = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User;
            var userId = user.Identities.First().Claims.First(c => c.Type == "oid").Value;

            var response = await _httpClient.PostAsJsonAsync($"api/cpu-cooler/{userId}", cpuCoolerVM);

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

        public async Task<List<string>> AddCPUCoolersAsync(List<CPUCoolerVM> CPUCoolers)
        {
            var user = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User;
            var userId = user.Identities.First().Claims.First(c => c.Type == "oid").Value;

            var response = await _httpClient.PostAsJsonAsync($"api/cpu-coolers/{userId}", CPUCoolers);

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
