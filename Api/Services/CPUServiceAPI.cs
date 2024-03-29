using Api.Repository;
using Shared.View_Models;

namespace Api.Services
{
    public interface ICPUService
    {
        public Task<List<CPUVM>> GetCPUsAsync();
        public Task<CPUVM> GetCPUByIdAsync(string id);
        public Task<string> AddCPUAsync(CPUVM cpu);
    }
    public class CPUServiceAPI : ICPUService
    {
        private readonly ICPUsRepository _cpus;

        public CPUServiceAPI(ICPUsRepository cpus)
        {
            _cpus = cpus;
        }

        public bool OnClient()
        {
            return false;
        }

        public async Task<List<CPUVM>> GetCPUsAsync()
        {
            return await _cpus.GetCPUsAsync();
        }

        public async Task<CPUVM> GetCPUByIdAsync(string id)
        {
            return await _cpus.GetCPUByIdAsync(id);
        }

        public async Task<string> AddCPUAsync(CPUVM cpu)
        {
            return await _cpus.AddCPUAsync(cpu);
        }
    }
}
