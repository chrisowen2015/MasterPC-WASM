using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.View_Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace MasterPC_WASM.Services
{
    public interface ICaseService
    {
        public Task<List<CaseVM>> GetCasesAsync();
        public Task<CaseVM> GetCaseByIdAsync(string id);
        public Task<string> AddCaseAsync(CaseVM caseVM);
        public Task<List<string>> AddCasesAsync(List<CaseVM> Cases);
    }
    public class CaseServiceClient(HttpClient httpClient, AuthenticationStateProvider authState) : ICaseService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider = authState;

        public async Task<List<CaseVM>> GetCasesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CaseVM>>("api/cases");
        }

        public async Task<CaseVM> GetCaseByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<CaseVM>($"api/case/{id}");
        }

        // Honestly don't know if this Authorize attribute does anything
        [Authorize(Roles = "superuser")]
        public async Task<string> AddCaseAsync(CaseVM caseVM)
        {
            var user = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User;
            var userId = user.Identities.First().Claims.First(c => c.Type == "oid").Value;

            var response = await _httpClient.PostAsJsonAsync($"api/case/{userId}", caseVM);

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

        public async Task<List<string>> AddCasesAsync(List<CaseVM> Cases)
        {
            var user = _authenticationStateProvider.GetAuthenticationStateAsync().Result.User;
            var userId = user.Identities.First().Claims.First(c => c.Type == "oid").Value;

            var response = await _httpClient.PostAsJsonAsync($"api/cases/{userId}", Cases);

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
