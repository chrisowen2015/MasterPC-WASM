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
    public class MotherboardsRepository : IMotherboardsRepository
    {
        private readonly MasterPcdbContext _context;

        public MotherboardsRepository(MasterPcdbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

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
            List<string> newMotherboardIds = new List<string>();

            foreach (var Motherboard in Motherboards)
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
                newMotherboardIds.Add(newMotherboard.Id);
            }

            await _context.SaveChangesAsync();

            return newMotherboardIds;
        }

    }
}
