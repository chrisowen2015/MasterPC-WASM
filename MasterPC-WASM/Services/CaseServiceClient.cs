using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.View_Models;
using System.Net.Http.Json;

namespace MasterPC_WASM.Services
{
    public interface ICaseService
    {
        public Task<List<CaseVM>> GetCasesAsync();
        public Task<CaseVM> GetCaseByIdAsync(string id);
        public Task<string> AddCaseAsync(CaseVM caseVM);
    }
    public class CaseServiceClient : ICaseService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public CaseServiceClient(HttpClient httpClient, AuthenticationStateProvider authState)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authState;
        }

        public bool OnClient()
        {
            return true;
        }

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
    }
}
