using Api.Repositories;
using Shared.View_Models;

namespace Api.Services
{
    public interface ICaseService
    {
        public Task<List<CaseVM>> GetCasesAsync();
        public Task<CaseVM> GetCaseByIdAsync(string id);
        public Task<string> AddCaseAsync(CaseVM Case);
    }
    public class CaseServiceAPI : ICaseService
    {
        private readonly ICasesRepository _cases;

        public CaseServiceAPI(ICasesRepository cases)
        {
            _cases = cases;
        }

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
    }
}
