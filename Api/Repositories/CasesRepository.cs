using Api.Data;
using Api.Data.Entities;
using Shared.View_Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public interface ICasesRepository
    {
        public Task<List<CaseVM>> GetCasesAsync();
        public Task<CaseVM> GetCaseByIdAsync(string id);
        public Task<string> AddCaseAsync(CaseVM Case);
        public Task<List<string>> AddCasesAsync(List<CaseVM> Cases);
    }
    public class CasesRepository(MasterPcdbContext applicationDbContext) : ICasesRepository
    {
        private readonly MasterPcdbContext _context = applicationDbContext;

        public async Task<List<CaseVM>> GetCasesAsync()
        {
            List<CaseVM> resultCases = new List<CaseVM>();

            List<Case> cases = await _context.Cases.ToListAsync();

            foreach (var Case in cases)
            {
                CaseVM CaseVM = new CaseVM
                {
                    Id = Case.Id,
                    PCPId = Case.PCPId,
                    Name = Case.Name,
                    ImgUrl = Case.ImgUrl,
                    Manufacturer = Case.Manufacturer,
                    Price = Case.Price,
                    Type = Case.Type,
                    Color = Case.Color,
                    Psu = Case.Psu,
                    SidePanel = Case.SidePanel,
                    ExternalVolume = Case.ExternalVolume,
                    InternalBays = Case.InternalBays,
                };
                resultCases.Add(CaseVM);
            }

            return resultCases;
        }

        public async Task<CaseVM> GetCaseByIdAsync(string id)
        {
            CaseVM? Case = await _context.Cases.Where(c => c.Id == id).Select(c => new CaseVM
            {
                Id = c.Id,
                PCPId = c.PCPId,
                Name = c.Name,
                ImgUrl = c.ImgUrl,
                Manufacturer = c.Manufacturer,
                Price = c.Price,
                Type = c.Type,
                Color = c.Color,
                Psu = c.Psu,
                SidePanel = c.SidePanel,
                ExternalVolume = c.ExternalVolume,
                InternalBays = c.InternalBays,
            }).SingleOrDefaultAsync();

            if (Case is not null)
            {
                return Case;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> AddCaseAsync(CaseVM Case)
        {
            Case newCase = new Case
            {
                Id = Guid.NewGuid().ToString(),
                PCPId = Case.PCPId,
                Name = Case.Name,
                ImgUrl = Case.ImgUrl,
                Manufacturer = Case.Manufacturer,
                Price = Case.Price,
                Type = Case.Type,
                Color = Case.Color,
                Psu = Case.Psu,
                SidePanel = Case.SidePanel,
                ExternalVolume = Case.ExternalVolume,
                InternalBays = Case.InternalBays,
            };

            _context.Cases.Add(newCase);
            await _context.SaveChangesAsync();

            return newCase.Id;
        }

        public async Task<List<string>> AddCasesAsync(List<CaseVM> Cases)
        {
            List<string> newCasesIds = new List<string>();

            List<Case> newCases = Cases.Select(caseVM => new Case
            {
                Id = Guid.NewGuid().ToString(),
                PCPId = caseVM.PCPId,
                Name = caseVM.Name,
                ImgUrl = caseVM.ImgUrl,
                Manufacturer = caseVM.Manufacturer,
                Price = caseVM.Price,
                Type = caseVM.Type,
                Color = caseVM.Color,
                Psu = caseVM.Psu,
                SidePanel = caseVM.SidePanel,
                ExternalVolume = caseVM.ExternalVolume,
                InternalBays = caseVM.InternalBays,
            }).ToList();

            _context.Cases.AddRange(newCases);
            await _context.SaveChangesAsync();

            return newCases.Select(c => c.Id).ToList();
        }
    }
}
