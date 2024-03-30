using Api.Repositories;
using Shared.View_Models;

namespace Api.Services
{
    public interface IPSUService
    {
        public Task<List<PSUVM>> GetPSUsAsync();
        public Task<PSUVM> GetPSUByIdAsync(string id);
        public Task<string> AddPSUAsync(PSUVM PSU);
        public Task<List<string>> AddPSUsAsync(List<PSUVM> PSUs);
    }
    public class PSUServiceAPI(IPSUsRepository psusRepository) : IPSUService
    {
        private readonly IPSUsRepository _psusRepository = psusRepository;

        public async Task<List<PSUVM>> GetPSUsAsync()
        {
            return await _psusRepository.GetPSUsAsync();
        }

        public async Task<PSUVM> GetPSUByIdAsync(string id)
        {
            return await _psusRepository.GetPSUByIdAsync(id);
        }

        public async Task<string> AddPSUAsync(PSUVM PSU)
        {
            return await _psusRepository.AddPSUAsync(PSU);
        }

        public async Task<List<string>> AddPSUsAsync(List<PSUVM> PSUs)
        {
            return await _psusRepository.AddPSUsAsync(PSUs);
        }
    }
}
