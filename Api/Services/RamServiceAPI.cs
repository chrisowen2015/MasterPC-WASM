using Api.Repositories;
using Shared.View_Models;

namespace Api.Services
{
    public interface IRamService
    {
        public Task<List<RAMVM>> GetRamsAsync();
        public Task<RAMVM> GetRamByIdAsync(string id);
        public Task<string> AddRamAsync(RAMVM ram);
        public Task<List<string>> AddRamsAsync(List<RAMVM> rams);
    }
    public class RamServiceAPI(IRamRepository ramRepository) : IRamService
    {
        private readonly IRamRepository _ramRepository = ramRepository;

        public async Task<List<RAMVM>> GetRamsAsync()
        {
            return await _ramRepository.GetRamsAsync();
        }

        public async Task<RAMVM> GetRamByIdAsync(string id)
        {
            return await _ramRepository.GetRamByIdAsync(id);
        }

        public async Task<string> AddRamAsync(RAMVM ram)
        {
            return await _ramRepository.AddRamAsync(ram);
        }

        public async Task<List<string>> AddRamsAsync(List<RAMVM> rams)
        {
            return await _ramRepository.AddRamsAsync(rams);
        }
    }
}
