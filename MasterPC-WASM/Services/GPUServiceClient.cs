using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.View_Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace MasterPC_WASM.Services
{
    public interface IGPUService
    {
        public Task<List<GPUVM>> GetGPUsAsync();
        public Task<GPUVM> GetGPUByIdAsync(string id);
        public Task<string> AddGPUAsync(GPUVM gpu);
        public Task<List<string>> AddGPUsAsync(List<GPUVM> GPUs);
    }   
    public class GPUServiceClient(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider) : IGPUService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;

        public async Task<List<GPUVM>> GetGPUsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<GPUVM>>("api/gpus");
        }

        public async Task<GPUVM> GetGPUByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<GPUVM>($"api/gpu/{id}");
        }

        [Authorize(Roles = "superuser")]
        public async Task<string> AddGPUAsync(GPUVM gpu)
        {
            var user = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User;
            var userId = user.Identities.First().Claims.First(c => c.Type == "oid").Value;

            var response = await _httpClient.PostAsJsonAsync("api/gpu/{userId}", gpu);

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

        public async Task<List<string>> AddGPUsAsync(List<GPUVM> GPUs)
        {
            var user = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User;
            var userId = user.Identities.First().Claims.First(c => c.Type == "oid").Value;

            var response = await _httpClient.PostAsJsonAsync($"api/gpus/{userId}", GPUs);

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
