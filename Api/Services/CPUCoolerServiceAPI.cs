using Api.Repositories;
using Shared.View_Models;

namespace Api.Services
{
    public interface ICPUCoolerService
    {
        public Task<List<CPUCoolerVM>> GetCPUCoolersAsync();
        public Task<CPUCoolerVM> GetCPUCoolerByIdAsync(string id);
        public Task<string> AddCPUCoolerAsync(CPUCoolerVM CPUCooler);
        public Task<List<string>> AddCPUCoolersAsync(List<CPUCoolerVM> CPUCoolers);
    }
    public class CPUCoolerServiceAPI(ICPUCoolersRepository CPUCoolers) : ICPUCoolerService
    {
        private readonly ICPUCoolersRepository _CPUCoolers = CPUCoolers;

        public async Task<List<CPUCoolerVM>> GetCPUCoolersAsync()
        {
            return await _CPUCoolers.GetCPUCoolersAsync();
        }

        public async Task<CPUCoolerVM> GetCPUCoolerByIdAsync(string id)
        {
            return await _CPUCoolers.GetCPUCoolerByIdAsync(id);
        }

        public async Task<string> AddCPUCoolerAsync(CPUCoolerVM CPUCooler)
        {
            return await _CPUCoolers.AddCPUCoolerAsync(CPUCooler);
        }

        public async Task<List<string>> AddCPUCoolersAsync(List<CPUCoolerVM> CPUCoolers)
        {
            return await _CPUCoolers.AddCPUCoolersAsync(CPUCoolers);
        }
    }
}
