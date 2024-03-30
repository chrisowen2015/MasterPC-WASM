using Api.Repositories;
using Shared.View_Models;

namespace Api.Services
{
    public interface ICPUService
    {
        public Task<List<CPUVM>> GetCPUsAsync();
        public Task<CPUVM> GetCPUByIdAsync(string id);
        public Task<string> AddCPUAsync(CPUVM cpu);
        public Task<List<string>> AddCPUsAsync(List<CPUVM> cpus);
    }
    public class CPUServiceAPI(ICPUsRepository cpus) : ICPUService
    {
        private readonly ICPUsRepository _cpus = cpus;

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

        public async Task<List<string>> AddCPUsAsync(List<CPUVM> cpus)
        {
            return await _cpus.AddCPUsAsync(cpus);
        }
    }
}
