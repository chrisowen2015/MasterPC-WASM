using Api.Data;
using Api.Data.Entities;
using Shared.View_Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public interface IGPUsRepository
    {
        public Task<List<GPUVM>> GetGPUsAsync();
        public Task<GPUVM> GetGPUByIdAsync(string id);
        public Task<string> AddGPUAsync(GPUVM gpu);
        public Task<List<string>> AddGPUsAsync(List<GPUVM> gpus);
    }
    public class GPUsRepository(MasterPcdbContext applicationDbContext) : IGPUsRepository
    {
        private readonly MasterPcdbContext _context = applicationDbContext;

        public async Task<List<GPUVM>> GetGPUsAsync()
        {
            List<GPUVM> resultGpus = new List<GPUVM>();

            List<Gpu> gpus = await _context.Gpus.ToListAsync();

            foreach (var gpu in gpus)
            {
                GPUVM gpuVM = new GPUVM
                {
                    Id = gpu.Id,
                    PCPId = gpu.PCPId,
                    Name = gpu.Name,
                    ImgUrl = gpu.ImgUrl,
                    Manufacturer = gpu.Manufacturer,
                    Price = gpu.Price,
                    Chipset = gpu.Chipset,
                    Memory = gpu.Memory,
                    CoreClock = gpu.CoreClock,
                    BoostClock = gpu.BoostClock,
                    Color = gpu.Color,
                    Length = gpu.Length,
                    Tdp = gpu.Tdp,
                };
                resultGpus.Add(gpuVM);
            }

            return resultGpus;
        }

        public async Task<GPUVM> GetGPUByIdAsync(string id)
        {
            GPUVM? gpu = await _context.Gpus.Where(c => c.Id == id).Select(c => new GPUVM
            {
                Id = c.Id,
                PCPId = c.PCPId,
                Name = c.Name,
                ImgUrl = c.ImgUrl,
                Manufacturer = c.Manufacturer,
                Price = c.Price,
                Chipset = c.Chipset,
                Memory = c.Memory,
                CoreClock = c.CoreClock,
                BoostClock = c.BoostClock,
                Color = c.Color,
                Length = c.Length,
                Tdp = c.Tdp,
            }).SingleOrDefaultAsync();

            if (gpu is not null)
            {
                return gpu;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> AddGPUAsync(GPUVM gpu)
        {
            Gpu newGpu = new Gpu
            {
                Id = Guid.NewGuid().ToString(),
                PCPId = gpu.PCPId,
                Name = gpu.Name,
                ImgUrl = gpu.ImgUrl,
                Manufacturer = gpu.Manufacturer,
                Price = gpu.Price,
                Chipset = gpu.Chipset,
                Memory = gpu.Memory,
                CoreClock = gpu.CoreClock,
                BoostClock = gpu.BoostClock,
                Color = gpu.Color,
                Length = gpu.Length,
                Tdp = gpu.Tdp,
            };

            await _context.Gpus.AddAsync(newGpu);
            await _context.SaveChangesAsync();

            return newGpu.Id;
        }

        public async Task<List<string>> AddGPUsAsync(List<GPUVM> gpus)
        {
            List<string> newGpuIds = new List<string>();

            List<Gpu> newCPUCoolers = gpus.Select(gpu => new Gpu
            {
                Id = Guid.NewGuid().ToString(),
                PCPId = gpu.PCPId,
                Name = gpu.Name,
                ImgUrl = gpu.ImgUrl,
                Manufacturer = gpu.Manufacturer,
                Price = gpu.Price,
                Chipset = gpu.Chipset,
                Memory = gpu.Memory,
                CoreClock = gpu.CoreClock,
                BoostClock = gpu.BoostClock,
                Color = gpu.Color,
                Length = gpu.Length,
                Tdp = gpu.Tdp,
            }).ToList();

            await _context.Gpus.AddRangeAsync(newCPUCoolers);
            await _context.SaveChangesAsync();

            return newCPUCoolers.Select(gpu => gpu.Id).ToList();
        }
    }
}

