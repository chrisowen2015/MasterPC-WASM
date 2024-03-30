using Api.Repositories;
using Shared.View_Models;

namespace Api.Services
{
    public interface IMotherboardService
    {
        public Task<List<MotherboardVM>> GetMotherboardsAsync();
        public Task<MotherboardVM> GetMotherboardByIdAsync(string id);
        public Task<string> AddMotherboardAsync(MotherboardVM motherboard);
        public Task<List<string>> AddMotherboardsAsync(List<MotherboardVM> motherboards);
    }
    public class MotherboardServiceAPI(IMotherboardsRepository motherboards) : IMotherboardService
    {
        private readonly IMotherboardsRepository _motherboards = motherboards;

        public async Task<List<MotherboardVM>> GetMotherboardsAsync()
        {
            return await _motherboards.GetMotherboardsAsync();
        }

        public async Task<MotherboardVM> GetMotherboardByIdAsync(string id)
        {
            return await _motherboards.GetMotherboardByIdAsync(id);
        }

        public async Task<string> AddMotherboardAsync(MotherboardVM motherboardVM)
        {
            return await _motherboards.AddMotherboardAsync(motherboardVM);
        }

        public async Task<List<string>> AddMotherboardsAsync(List<MotherboardVM> motherboardVMs)
        {
            return await _motherboards.AddMotherboardsAsync(motherboardVMs);
        }
    }
}
