using Api.Repositories;
using Shared.View_Models;

namespace Api.Services
{
    public interface ICaseService
    {
        public Task<List<CaseVM>> GetCasesAsync();
        public Task<CaseVM> GetCaseByIdAsync(string id);
        public Task<string> AddCaseAsync(CaseVM Case);
        public Task<List<string>> AddCasesAsync(List<CaseVM> Cases);
    }
    public class CaseServiceAPI(ICasesRepository cases) : ICaseService
    {
        private readonly ICasesRepository _cases = cases;

        public async Task<List<CaseVM>> GetCasesAsync()
        {
            return await _cases.GetCasesAsync();
        }

        public async Task<CaseVM> GetCaseByIdAsync(string id)
        {
            return await _cases.GetCaseByIdAsync(id);
        }

        public async Task<string> AddCaseAsync(CaseVM caseVM)
        {
            return await _cases.AddCaseAsync(caseVM);
        }

        public async Task<List<string>> AddCasesAsync(List<CaseVM> caseVMs)
        {
            return await _cases.AddCasesAsync(caseVMs);
        }
    }
}
