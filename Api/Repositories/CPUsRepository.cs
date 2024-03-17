using Microsoft.EntityFrameworkCore;
using Api.Data.Entities;
using Api.Data;
using Shared.View_Models;

namespace Api.Repository
{
    public interface ICPUsRepository
    {
        public Task<List<CPUVM>> GetCPUsAsync();
        public Task<CPUVM> GetCPUByIdAsync(string id);
        public Task<string> AddCPUAsync(CPUVM cpu);
        public Task<bool> UpdateCPUAsync(CPUVM cpu);
        public Task<bool> DeleteCPUAsync(string id);
    }

    public class CPUsRepository : ICPUsRepository
    {
        private readonly MasterPcdbContext _context;
        public CPUsRepository(MasterPcdbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<List<CPUVM>> GetCPUsAsync()
        {
            List<CPUVM> resultCPUs = new List<CPUVM>();

            List<Cpu> cpus = await _context.Cpus.ToListAsync();

            foreach (var cpu in cpus)
            {
                CPUVM cpuVM = new CPUVM
                {
                    Id = cpu.Id,
                    Name = cpu.Name,
                    Manufacturer = cpu.Manufacturer,
                    Price = cpu.Price,
                    CoreCount = cpu.CoreCount,
                    CoreClock = cpu.CoreClock,
                    BoostClock = cpu.BoostClock,
                    TDP = cpu.Tdp,
                    Graphics = cpu.Graphics,
                    SMT = cpu.Smt
                };
                resultCPUs.Add(cpuVM);
            }

            return resultCPUs;
        }

        public async Task<CPUVM> GetCPUByIdAsync(string id)
        {
            CPUVM? cpu = await _context.Cpus.Where(c => c.Id == id).Select(c => new CPUVM
            {
                Id = c.Id,
                Name = c.Name,
                Manufacturer = c.Manufacturer,
                Price = c.Price,
                CoreCount = c.CoreCount,
                CoreClock = c.CoreClock,
                BoostClock = c.BoostClock,
                TDP = c.Tdp,
                Graphics = c.Graphics,
                SMT = c.Smt
            }).SingleOrDefaultAsync();

            if (cpu is not null)
            {
                return cpu;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> AddCPUAsync(CPUVM cpu)
        {
            Cpu newCPU = new Cpu
            {
                Id = Guid.NewGuid().ToString(),
                Name = cpu.Name,
                Manufacturer = cpu.Manufacturer,
                Price = cpu.Price,
                CoreCount = cpu.CoreCount,
                CoreClock = cpu.CoreClock,
                BoostClock = cpu.BoostClock,
                Tdp = cpu.TDP,
                Graphics = cpu.Graphics,
                Smt = cpu.SMT
            };

            _context.Add(newCPU);
            await _context.SaveChangesAsync();

            return newCPU.Id;
        }

        public async Task<bool> UpdateCPUAsync(CPUVM cpu)
        {
            Cpu? existingCPU = await _context.Cpus.Where(c => c.Id == cpu.Id).SingleOrDefaultAsync();

            if (existingCPU is not null)
            {
                existingCPU.Name = cpu.Name;
                existingCPU.Manufacturer = cpu.Manufacturer;
                existingCPU.Price = cpu.Price;
                existingCPU.CoreCount = cpu.CoreCount;
                existingCPU.CoreClock = cpu.CoreClock;
                existingCPU.BoostClock = cpu.BoostClock;
                existingCPU.Tdp = cpu.TDP;
                existingCPU.Graphics = cpu.Graphics;
                existingCPU.Smt = cpu.SMT;

                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteCPUAsync(string id)
        {
            Cpu? existingCPU = await _context.Cpus.Where(c => c.Id == id).SingleOrDefaultAsync();

            if (existingCPU is not null)
            {
                _context.Cpus.Remove(existingCPU);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}