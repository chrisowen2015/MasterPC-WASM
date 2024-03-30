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
        public Task<List<string>> AddCPUsAsync(List<CPUVM> cpus);
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
                    PCPId = cpu.PCPId,
                    Name = cpu.Name,
                    ImgUrl = cpu.ImgUrl,
                    Manufacturer = cpu.Manufacturer,
                    Socket = cpu.Socket,
                    HasCooler = cpu.HasCooler,
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
                PCPId = c.PCPId,
                Name = c.Name,
                ImgUrl = c.ImgUrl,
                Manufacturer = c.Manufacturer,
                Socket = c.Socket,
                HasCooler = c.HasCooler,
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
                PCPId = cpu.PCPId,
                Name = cpu.Name,
                ImgUrl = cpu.ImgUrl,
                Manufacturer = cpu.Manufacturer,
                Socket = cpu.Socket,
                HasCooler = cpu.HasCooler,
                Price = cpu.Price,
                CoreCount = cpu.CoreCount,
                CoreClock = cpu.CoreClock,
                BoostClock = cpu.BoostClock,
                Tdp = cpu.TDP,
                Graphics = cpu.Graphics,
                Smt = cpu.SMT
            };

            _context.Cpus.Add(newCPU);
            await _context.SaveChangesAsync();

            return newCPU.Id;
        }

        public async Task<List<string>> AddCPUsAsync(List<CPUVM> cpus)
        {
            List<string> newCasesIds = new List<string>();

            List<Cpu> newCPUs = cpus.Select(cpu => new Cpu
            {
                Id = Guid.NewGuid().ToString(),
                PCPId = cpu.PCPId,
                Name = cpu.Name,
                ImgUrl = cpu.ImgUrl,
                Manufacturer = cpu.Manufacturer,
                Socket = cpu.Socket,
                HasCooler = cpu.HasCooler,
                Price = cpu.Price,
                CoreCount = cpu.CoreCount,
                CoreClock = cpu.CoreClock,
                BoostClock = cpu.BoostClock,
                Tdp = cpu.TDP,
                Graphics = cpu.Graphics,
                Smt = cpu.SMT
            }).ToList();

            _context.Cpus.AddRange(newCPUs);
            await _context.SaveChangesAsync();

            return newCPUs.Select(c => c.Id).ToList();
        }
    }
}