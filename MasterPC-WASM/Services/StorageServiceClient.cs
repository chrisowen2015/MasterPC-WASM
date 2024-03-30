using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.View_Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace MasterPC_WASM.Services
{
    public interface IStorageService
    {
        public Task<List<StorageVM>> GetStoragesAsync();
        public Task<StorageVM> GetStorageByIdAsync(string id);
        public Task<string> AddStorageAsync(StorageVM storageVM);
        public Task<List<string>> AddStoragesAsync(List<StorageVM> Storages);
    }
    public class StorageServiceClient(HttpClient httpClient, AuthenticationStateProvider authState) : IStorageService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider = authState;

        public async Task<List<StorageVM>> GetStoragesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<StorageVM>>("api/storages");
        }

        public async Task<StorageVM> GetStorageByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<StorageVM>($"api/storage/{id}");
        }

        [Authorize(Roles = "superuser")]
        public async Task<string> AddStorageAsync(StorageVM storageVM)
        {
            var user = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User;
            var userId = user.Identities.First().Claims.First(c => c.Type == "oid").Value;

            var response = await _httpClient.PostAsJsonAsync($"api/storage/{userId}", storageVM);

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

        public async Task<List<string>> AddStoragesAsync(List<StorageVM> Storages)
        {
            var user = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User;
            var userId = user.Identities.First().Claims.First(c => c.Type == "oid").Value;

            var response = await _httpClient.PostAsJsonAsync($"api/storages/{userId}", Storages);

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
