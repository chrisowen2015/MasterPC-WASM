using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.View_Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace MasterPC_WASM.Services
{
    public interface IMotherboardService
    {
        public Task<List<MotherboardVM>> GetMotherboardsAsync();
        public Task<MotherboardVM> GetMotherboardByIdAsync(string id);
        public Task<string> AddMotherboardAsync(MotherboardVM motherboardVM);
        public Task<List<string>> AddMotherboardsAsync(List<MotherboardVM> Motherboards);
    }
    public class MotherboardServiceClient(HttpClient httpClient, AuthenticationStateProvider authState) : IMotherboardService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider = authState;

        public async Task<List<MotherboardVM>> GetMotherboardsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<MotherboardVM>>("api/motherboards");
        }

        public async Task<MotherboardVM> GetMotherboardByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<MotherboardVM>($"api/motherboard/{id}");
        }

        [Authorize(Roles = "superuser")]
        public async Task<string> AddMotherboardAsync(MotherboardVM motherboardVM)
        {
            var user = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User;
            var userId = user.Identities.First().Claims.First(c => c.Type == "oid").Value;

            var response = await _httpClient.PostAsJsonAsync($"api/motherboard/{userId}", motherboardVM);

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

        public async Task<List<string>> AddMotherboardsAsync(List<MotherboardVM> Motherboards)
        {
            var user = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User;
            var userId = user.Identities.First().Claims.First(c => c.Type == "oid").Value;

            var response = await _httpClient.PostAsJsonAsync($"api/motherboards/{userId}", Motherboards);

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
