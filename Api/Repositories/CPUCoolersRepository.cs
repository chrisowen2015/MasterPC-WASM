using Api.Data;
using Api.Data.Entities;
using Shared.View_Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public interface ICPUCoolersRepository
    {
        public Task<List<CPUCoolerVM>> GetCPUCoolersAsync();
        public Task<CPUCoolerVM> GetCPUCoolerByIdAsync(string id);
        public Task<string> AddCPUCoolerAsync(CPUCoolerVM CPUCooler);
        public Task<List<string>> AddCPUCoolersAsync(List<CPUCoolerVM> CPUCoolers);
    }
    public class CPUCoolersRepository(MasterPcdbContext applicationDbContext) : ICPUCoolersRepository
    {
        private readonly MasterPcdbContext _context = applicationDbContext;

        public async Task<List<CPUCoolerVM>> GetCPUCoolersAsync()
        {
            List<CPUCoolerVM> resultCPUCoolers = new List<CPUCoolerVM>();

            List<CpuCooler> CPUCoolers = await _context.CpuCoolers.ToListAsync();

            foreach (var CPUCooler in CPUCoolers)
            {
                CPUCoolerVM CPUCoolerVM = new CPUCoolerVM
                {
                    Id = CPUCooler.Id,
                    PCPId = CPUCooler.PCPId,
                    Name = CPUCooler.Name,
                    ImgUrl = CPUCooler.ImgUrl,
                    Manufacturer = CPUCooler.Manufacturer,
                    MinRPM = CPUCooler.MinRPM,
                    MaxRPM = CPUCooler.MaxRPM,
                    MinNoise = CPUCooler.MinNoise,
                    MaxNoise = CPUCooler.MaxNoise,
                    Color = CPUCooler.Color,
                    RadiatorSize = CPUCooler.RadiatorSize,
                    CompatibleSockets = CPUCooler.CompatibleSockets.Split(',').ToList(),
                    Price = CPUCooler.Price,
                };
                resultCPUCoolers.Add(CPUCoolerVM);
            }

            return resultCPUCoolers;
        }

        public async Task<CPUCoolerVM> GetCPUCoolerByIdAsync(string id)
        {
            CpuCooler? CPUCooler = await _context.CpuCoolers.Where(c => c.Id == id).Select(c => new CpuCooler
            {
                Id = c.Id,
                PCPId = c.PCPId,
                Name = c.Name,
                ImgUrl = c.ImgUrl,
                Manufacturer = c.Manufacturer,
                MinRPM = c.MinRPM,
                MaxRPM = c.MaxRPM,
                MinNoise = c.MinNoise,
                MaxNoise = c.MaxNoise,
                Color = c.Color,
                RadiatorSize = c.RadiatorSize,
                CompatibleSockets = c.CompatibleSockets,
                Price = c.Price,
            }).SingleOrDefaultAsync();

            if(CPUCooler is not null)
            {
                return new CPUCoolerVM
                {
                    Id = CPUCooler.Id,
                    PCPId = CPUCooler.PCPId,
                    Name = CPUCooler.Name,
                    ImgUrl = CPUCooler.ImgUrl,
                    Manufacturer = CPUCooler.Manufacturer,
                    MinRPM = CPUCooler.MinRPM,
                    MaxRPM = CPUCooler.MaxRPM,
                    MinNoise = CPUCooler.MinNoise,
                    MaxNoise = CPUCooler.MaxNoise,
                    Color = CPUCooler.Color,
                    RadiatorSize = CPUCooler.RadiatorSize,
                    CompatibleSockets = CPUCooler.CompatibleSockets.Split(',').ToList(),
                    Price = CPUCooler.Price,
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<string> AddCPUCoolerAsync(CPUCoolerVM CPUCooler)
        {
            CpuCooler newCPUCooler = new CpuCooler
            {
                Id = Guid.NewGuid().ToString(),
                PCPId = CPUCooler.PCPId,
                Name = CPUCooler.Name,
                ImgUrl = CPUCooler.ImgUrl,
                Manufacturer = CPUCooler.Manufacturer,
                MinRPM = CPUCooler.MinRPM,
                MaxRPM = CPUCooler.MaxRPM,
                MinNoise = CPUCooler.MinNoise,
                MaxNoise = CPUCooler.MaxNoise,
                Color = CPUCooler.Color,
                RadiatorSize = CPUCooler.RadiatorSize,
                CompatibleSockets = string.Join(',', CPUCooler.CompatibleSockets),
                Price = CPUCooler.Price,
            };

            await _context.CpuCoolers.AddAsync(newCPUCooler);
            await _context.SaveChangesAsync();

            return newCPUCooler.Id;
        }

        public async Task<List<string>> AddCPUCoolersAsync(List<CPUCoolerVM> CPUCoolers)
        {
            List<string> newCPUCoolersIds = new List<string>();

            List<CpuCooler> newCPUCoolers = CPUCoolers.Select(CPUCoolerVM => new CpuCooler
            {
                Id = Guid.NewGuid().ToString(),
                PCPId = CPUCoolerVM.PCPId,
                Name = CPUCoolerVM.Name,
                ImgUrl = CPUCoolerVM.ImgUrl,
                Manufacturer = CPUCoolerVM.Manufacturer,
                MinRPM = CPUCoolerVM.MinRPM,
                MaxRPM = CPUCoolerVM.MaxRPM,
                MinNoise = CPUCoolerVM.MinNoise,
                MaxNoise = CPUCoolerVM.MaxNoise,
                Color = CPUCoolerVM.Color,
                RadiatorSize = CPUCoolerVM.RadiatorSize,
                CompatibleSockets = string.Join(',', CPUCoolerVM.CompatibleSockets),
                Price = CPUCoolerVM.Price,
            }).ToList();

            await _context.CpuCoolers.AddRangeAsync(newCPUCoolers);
            await _context.SaveChangesAsync();

            return newCPUCoolers.Select(CPUCooler => CPUCooler.Id).ToList();
        }
    }
}
