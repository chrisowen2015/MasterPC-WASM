using Api.Data;
using Api.Data.Entities;
using Shared.View_Models;
using Microsoft.EntityFrameworkCore;
using Api.Migrations;

namespace Api.Repositories
{
    public interface IStorageRepository
    {
        public Task<List<StorageVM>> GetStoragesAsync();
        public Task<StorageVM> GetStorageByIdAsync(string id);
        public Task<string> AddStorageAsync(StorageVM Storage);
        public Task<List<string>> AddStoragesAsync(List<StorageVM> Storages);
    }
    public class StorageRepository(MasterPcdbContext applicationDbContext) : IStorageRepository
    {
        private readonly MasterPcdbContext _context = applicationDbContext;

        public async Task<List<StorageVM>> GetStoragesAsync()
        {
            List<StorageVM> resultStorages = new List<StorageVM>();

            List<Storage> Storages = await _context.Storages.ToListAsync();

            foreach (var Storage in Storages)
            {
                StorageVM StorageVM = new StorageVM
                {
                    Id = Storage.Id,
                    PCPId = Storage.PCPId,
                    Name = Storage.Name,
                    ImgUrl = Storage.ImgUrl,
                    Manufacturer = Storage.Manufacturer,
                    Price = Storage.Price,
                    Type = Storage.Type,
                    Capacity = Storage.Capacity,
                    Interface = Storage.Interface,
                    FormFactor = Storage.FormFactor,
                    Cache = Storage.Cache,
                    PricePerGB = Storage.PricePerGB,
                    IsNVMe = Storage.IsNVMe,
                };

                resultStorages.Add(StorageVM);
            }

            return resultStorages;
        }

        public async Task<StorageVM> GetStorageByIdAsync(string id)
        {
            StorageVM? Storage = await _context.Storages.Where(p => p.Id == id).Select(p => new StorageVM
            {
                Id = p.Id,
                PCPId = p.PCPId,
                Name = p.Name,
                ImgUrl = p.ImgUrl,
                Manufacturer = p.Manufacturer,
                Price = p.Price,
                Type = p.Type,
                Capacity = p.Capacity,
                Interface = p.Interface,
                FormFactor = p.FormFactor,
                Cache = p.Cache,
                PricePerGB = p.PricePerGB,
                IsNVMe = p.IsNVMe,
            }).FirstOrDefaultAsync();

            if (Storage is not null)
            {
                return Storage;
            }
            else
            {
                throw new Exception("Storage not found");
            }
        }

        public async Task<string> AddStorageAsync(StorageVM Storage)
        {
            Storage newStorage = new Storage
            {
                Id = Guid.NewGuid().ToString(),
                PCPId = Storage.PCPId,
                Name = Storage.Name,
                ImgUrl = Storage.ImgUrl,
                Manufacturer = Storage.Manufacturer,
                Price = Storage.Price,
                Type = Storage.Type,
                Capacity = Storage.Capacity,
                Interface = Storage.Interface,
                FormFactor = Storage.FormFactor,
                Cache = Storage.Cache,
                PricePerGB = Storage.PricePerGB,
                IsNVMe = Storage.IsNVMe,
            };

            await _context.Storages.AddAsync(newStorage);
            await _context.SaveChangesAsync();

            return newStorage.Id;
        }

        public async Task<List<string>> AddStoragesAsync(List<StorageVM> Storages)
        {
            List<Storage> newStorages = Storages.Select(Storage => new Storage
            {
                Id = Guid.NewGuid().ToString(),
                PCPId = Storage.PCPId,
                Name = Storage.Name,
                ImgUrl = Storage.ImgUrl,
                Manufacturer = Storage.Manufacturer,
                Price = Storage.Price,
                Type = Storage.Type,
                Capacity = Storage.Capacity,
                Interface = Storage.Interface,
                FormFactor = Storage.FormFactor,
                Cache = Storage.Cache,
                PricePerGB = Storage.PricePerGB,
                IsNVMe = Storage.IsNVMe,
            }).ToList();

            await _context.Storages.AddRangeAsync(newStorages);
            await _context.SaveChangesAsync();

            List<string> newStorageIds = newStorages.Select(storage => storage.Id).ToList();

            return newStorageIds;
        }
    }
}
