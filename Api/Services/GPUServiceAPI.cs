using Api.Repositories;
using Shared.View_Models;

namespace Api.Services
{
    public interface IGPUService
    {
        public Task<List<GPUVM>> GetGPUsAsync();
        public Task<GPUVM> GetGPUByIdAsync(string id);
        public Task<string> AddGPUAsync(GPUVM GPU);
        public Task<List<string>> AddGPUsAsync(List<GPUVM> GPUs);
    }
    public class GPUServiceAPI(IGPUsRepository gpus) : IGPUService
    {
        private readonly IGPUsRepository _gpus = gpus;

        public async Task<List<GPUVM>> GetGPUsAsync()
        {
            return await _gpus.GetGPUsAsync();
        }

        public async Task<GPUVM> GetGPUByIdAsync(string id)
        {
            return await _gpus.GetGPUByIdAsync(id);
        }

        public async Task<string> AddGPUAsync(GPUVM gpuVM)
        {
            return await _gpus.AddGPUAsync(gpuVM);
        }

        public async Task<List<string>> AddGPUsAsync(List<GPUVM> gpuVMs)
        {
            return await _gpus.AddGPUsAsync(gpuVMs);
        }
    }
}
