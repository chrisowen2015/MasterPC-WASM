using Api.Data;
using Api.Data.Entities;
using Shared.View_Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public interface IMotherboardsRepository
    {
        public Task<List<MotherboardVM>> GetMotherboardsAsync();
        public Task<MotherboardVM> GetMotherboardByIdAsync(string id);
        public Task<string> AddMotherboardAsync(MotherboardVM Motherboard);
        public Task<List<string>> AddMotherboardsAsync(List<MotherboardVM> Motherboards);
    }
    public class MotherboardsRepository(MasterPcdbContext applicationDbContext) : IMotherboardsRepository
    {
        private readonly MasterPcdbContext _context = applicationDbContext;

        public async Task<List<MotherboardVM>> GetMotherboardsAsync()
        {
            List<MotherboardVM> resultMotherboards = new List<MotherboardVM>();

            List<Motherboard> motherboards = await _context.Motherboards.ToListAsync();

            foreach (var Motherboard in motherboards)
            {
                MotherboardVM MotherboardVM = new MotherboardVM
                {
                    Id = Motherboard.Id,
                    PCPId = Motherboard.PCPId,
                    Name = Motherboard.Name,
                    ImgUrl = Motherboard.ImgUrl,
                    Manufacturer = Motherboard.Manufacturer,
                    Price = Motherboard.Price,
                    Socket = Motherboard.Socket,
                    Chipset = Motherboard.Chipset,
                    FormFactor = Motherboard.FormFactor,
                    MemorySlots = Motherboard.MemorySlots,
                    MaxMemory = Motherboard.MaxMemory,
                    IntegratedWifi = Motherboard.IntegratedWifi,
                    Color = Motherboard.Color,
                    M2Slot = Motherboard.M2Slot,
                };
                resultMotherboards.Add(MotherboardVM);
            }

            return resultMotherboards;
        }

        public async Task<MotherboardVM> GetMotherboardByIdAsync(string id)
        {
            MotherboardVM? Motherboard = await _context.Motherboards.Where(m => m.Id == id).Select(m => new MotherboardVM
            {
                Id = m.Id,
                PCPId = m.PCPId,
                Name = m.Name,
                ImgUrl = m.ImgUrl,
                Manufacturer = m.Manufacturer,
                Price = m.Price,
                Socket = m.Socket,
                Chipset = m.Chipset,
                FormFactor = m.FormFactor,
                MemorySlots = m.MemorySlots,
                MaxMemory = m.MaxMemory,
                IntegratedWifi = m.IntegratedWifi,
                Color = m.Color,
                M2Slot = m.M2Slot,
            }).SingleOrDefaultAsync();

            if (Motherboard is not null)
            {
                return Motherboard;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> AddMotherboardAsync(MotherboardVM Motherboard)
        {
            Motherboard newMotherboard = new Motherboard
            {
                Id = Guid.NewGuid().ToString(),
                PCPId = Motherboard.PCPId,
                Name = Motherboard.Name,
                ImgUrl = Motherboard.ImgUrl,
                Manufacturer = Motherboard.Manufacturer,
                Price = Motherboard.Price,
                Socket = Motherboard.Socket,
                Chipset = Motherboard.Chipset,
                FormFactor = Motherboard.FormFactor,
                MemorySlots = Motherboard.MemorySlots,
                MaxMemory = Motherboard.MaxMemory,
                IntegratedWifi = Motherboard.IntegratedWifi,
                Color = Motherboard.Color,
                M2Slot = Motherboard.M2Slot,
            };

            await _context.Motherboards.AddAsync(newMotherboard);
            await _context.SaveChangesAsync();

            return newMotherboard.Id;
        }

        public async Task<List<string>> AddMotherboardsAsync(List<MotherboardVM> Motherboards)
        {
            List<Motherboard> newMotherboards = Motherboards.Select(motherboard => new Motherboard
            {
                Id = Guid.NewGuid().ToString(),
                PCPId = motherboard.PCPId,
                Name = motherboard.Name,
                ImgUrl = motherboard.ImgUrl,
                Manufacturer = motherboard.Manufacturer,
                Price = motherboard.Price,
                Socket = motherboard.Socket,
                Chipset = motherboard.Chipset,
                FormFactor = motherboard.FormFactor,
                MemorySlots = motherboard.MemorySlots,
                MaxMemory = motherboard.MaxMemory,
                IntegratedWifi = motherboard.IntegratedWifi,
                Color = motherboard.Color,
                M2Slot = motherboard.M2Slot,
            }).ToList();

            await _context.Motherboards.AddRangeAsync(newMotherboards);
            await _context.SaveChangesAsync();

            List<string> newMotherboardIds = newMotherboards.Select(m => m.Id).ToList();

            return newMotherboardIds;
        }

    }
}
