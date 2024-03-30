using Api.Data;
using Api.Data.Entities;
using Shared.View_Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public interface IPSUsRepository
    {
        public Task<List<PSUVM>> GetPSUsAsync();
        public Task<PSUVM> GetPSUByIdAsync(string id);
        public Task<string> AddPSUAsync(PSUVM PSU);
        public Task<List<string>> AddPSUsAsync(List<PSUVM> PSUs);
    }
    public class PSUsRepository(MasterPcdbContext applicationDbContext) : IPSUsRepository
    {
        private readonly MasterPcdbContext _context = applicationDbContext;

        public async Task<List<PSUVM>> GetPSUsAsync()
        {
            List<PSUVM> resultPSUs = new List<PSUVM>();

            List<PSU> PSUs = await _context.PSUs.ToListAsync();

            foreach (var PSU in PSUs)
            {
                PSUVM PSUVM = new PSUVM
                {
                    Id = PSU.Id,
                    PCPId = PSU.PCPId,
                    Name = PSU.Name,
                    ImgUrl = PSU.ImgUrl,
                    Manufacturer = PSU.Manufacturer,
                    Price = PSU.Price,
                    Type = PSU.Type,
                    Efficiency = PSU.Efficiency,
                    Wattage = PSU.Wattage,
                    Modular = PSU.Modular,
                    Color = PSU.Color,
                };
                resultPSUs.Add(PSUVM);
            }
            return resultPSUs;
        }

        public async Task<PSUVM> GetPSUByIdAsync(string id)
        {
            PSUVM? PSU = await _context.PSUs.Where(p => p.Id == id).Select(p => new PSUVM
            {
                Id = p.Id,
                PCPId = p.PCPId,
                Name = p.Name,
                ImgUrl = p.ImgUrl,
                Manufacturer = p.Manufacturer,
                Price = p.Price,
                Type = p.Type,
                Efficiency = p.Efficiency,
                Wattage = p.Wattage,
                Modular = p.Modular,
                Color = p.Color,
            }).SingleOrDefaultAsync();

            if (PSU is not null)
            {
                return PSU;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> AddPSUAsync(PSUVM PSU)
        {
            PSU newPSU = new PSU
            {
                Id = Guid.NewGuid().ToString(),
                PCPId = PSU.PCPId,
                Name = PSU.Name,
                ImgUrl = PSU.ImgUrl,
                Manufacturer = PSU.Manufacturer,
                Price = PSU.Price,
                Type = PSU.Type,
                Efficiency = PSU.Efficiency,
                Wattage = PSU.Wattage,
                Modular = PSU.Modular,
                Color = PSU.Color,
            };

            await _context.PSUs.AddAsync(newPSU);
            await _context.SaveChangesAsync();

            return newPSU.Id;
        }

        public async Task<List<string>> AddPSUsAsync(List<PSUVM> PSUs)
        {
            List<PSU> newPSUs = PSUs.Select(psu => new PSU
            {
                Id = Guid.NewGuid().ToString(),
                PCPId = psu.PCPId,
                Name = psu.Name,
                ImgUrl = psu.ImgUrl,
                Manufacturer = psu.Manufacturer,
                Price = psu.Price,
                Type = psu.Type,
                Efficiency = psu.Efficiency,
                Wattage = psu.Wattage,
                Modular = psu.Modular,
                Color = psu.Color,
            }).ToList();

            await _context.PSUs.AddRangeAsync(newPSUs);
            await _context.SaveChangesAsync();

            List<string> newPSUIds = newPSUs.Select(psu => psu.Id).ToList();

            return newPSUIds;
        }
    }
}
