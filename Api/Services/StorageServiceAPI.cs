using Api.Repositories;
using Shared.View_Models;

namespace Api.Services
{
    public interface IStorageService
    {
        public Task<List<StorageVM>> GetStoragesAsync();
        public Task<StorageVM> GetStorageByIdAsync(string id);
        public Task<string> AddStorageAsync(StorageVM storage);
        public Task<List<string>> AddStoragesAsync(List<StorageVM> storages);
    }
    public class StorageServiceAPI(IStorageRepository storageRepository) : IStorageService
    {
        private readonly IStorageRepository _storageRepository = storageRepository;

        public async Task<List<StorageVM>> GetStoragesAsync()
        {
            return await _storageRepository.GetStoragesAsync();
        }

        public async Task<StorageVM> GetStorageByIdAsync(string id)
        {
            return await _storageRepository.GetStorageByIdAsync(id);
        }

        public async Task<string> AddStorageAsync(StorageVM storage)
        {
            return await _storageRepository.AddStorageAsync(storage);
        }

        public async Task<List<string>> AddStoragesAsync(List<StorageVM> storages)
        {
            return await _storageRepository.AddStoragesAsync(storages);
        }
    }
}
