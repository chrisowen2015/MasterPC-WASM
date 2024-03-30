using Api.Data;
using Api.Data.Entities;
using Shared.View_Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public interface IRamRepository
    {
        public Task<List<RAMVM>> GetRamsAsync();
        public Task<RAMVM> GetRamByIdAsync(string id);
        public Task<string> AddRamAsync(RAMVM Ram);
        public Task<List<string>> AddRamsAsync(List<RAMVM> Rams);
    }
    public class RamRepository(MasterPcdbContext applicationDbContext) : IRamRepository
    {
        private readonly MasterPcdbContext _context = applicationDbContext;

        public async Task<List<RAMVM>> GetRamsAsync()
        {
            List<RAMVM> resultRams = new List<RAMVM>();

            List<Ram> Rams = await _context.Rams.ToListAsync();

            foreach (var Ram in Rams)
            {
                RAMVM RamVM = new RAMVM
                {
                    Id = Ram.Id,
                    PCPId = Ram.PCPId,
                    Name = Ram.Name,
                    ImgUrl = Ram.ImgUrl,
                    Manufacturer = Ram.Manufacturer,
                    Price = Ram.Price,
                    Type = Ram.Type,
                    Speed = Ram.Speed,
                    NumModules = Ram.NumModules,
                    PricePerGB = Ram.PricePerGB,
                    FirstWordLatency = Ram.FirstWordLatency,
                    Capacity = Ram.Capacity,
                    Color = Ram.Color,
                    RGB = Ram.RGB,
                    CASLatency = Ram.CASLatency,
                    HeatSpreader = Ram.HeatSpreader,
                };

                resultRams.Add(RamVM);
            }

            return resultRams;
        }

        public async Task<RAMVM> GetRamByIdAsync(string id)
        {
            RAMVM? Ram = await _context.Rams.Where(p => p.Id == id).Select(p => new RAMVM
            {
                Id = p.Id,
                PCPId = p.PCPId,
                Name = p.Name,
                ImgUrl = p.ImgUrl,
                Manufacturer = p.Manufacturer,
                Price = p.Price,
                Type = p.Type,
                Speed = p.Speed,
                NumModules = p.NumModules,
                PricePerGB = p.PricePerGB,
                FirstWordLatency = p.FirstWordLatency,
                Capacity = p.Capacity,
                Color = p.Color,
                RGB = p.RGB,
                CASLatency = p.CASLatency,
                HeatSpreader = p.HeatSpreader,
            }).SingleOrDefaultAsync();

            if (Ram is not null)
            {
                return Ram;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> AddRamAsync(RAMVM Ram)
        {
            Ram newRam = new Ram
            {
                Id = Guid.NewGuid().ToString(),
                PCPId = Ram.PCPId,
                Name = Ram.Name,
                ImgUrl = Ram.ImgUrl,
                Manufacturer = Ram.Manufacturer,
                Price = Ram.Price,
                Type = Ram.Type,
                Speed = Ram.Speed,
                NumModules = Ram.NumModules,
                PricePerGB = Ram.PricePerGB,
                FirstWordLatency = Ram.FirstWordLatency,
                Capacity = Ram.Capacity,
                Color = Ram.Color,
                RGB = Ram.RGB,
                CASLatency = Ram.CASLatency,
                HeatSpreader = Ram.HeatSpreader,
            };

            await _context.Rams.AddAsync(newRam);
            await _context.SaveChangesAsync();

            return newRam.Id;
        }

        public async Task<List<string>> AddRamsAsync(List<RAMVM> Rams)
        {
            List<Ram> newRams = Rams.Select(Ram => new Ram
            {
                Id = Guid.NewGuid().ToString(),
                PCPId = Ram.PCPId,
                Name = Ram.Name,
                ImgUrl = Ram.ImgUrl,
                Manufacturer = Ram.Manufacturer,
                Price = Ram.Price,
                Type = Ram.Type,
                Speed = Ram.Speed,
                NumModules = Ram.NumModules,
                PricePerGB = Ram.PricePerGB,
                FirstWordLatency = Ram.FirstWordLatency,
                Capacity = Ram.Capacity,
                Color = Ram.Color,
                RGB = Ram.RGB,
                CASLatency = Ram.CASLatency,
                HeatSpreader = Ram.HeatSpreader,
            }).ToList();

            await _context.Rams.AddRangeAsync(newRams);
            await _context.SaveChangesAsync();

            List<string> newRAMIds = newRams.Select(ram => ram.Id).ToList();

            return newRAMIds;
        }
    }
}
