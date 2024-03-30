using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Shared.View_Models;

namespace Api.Functions.Storage
{
    public class GetStorageFunction(IStorageService storageService)
    {
        private readonly IStorageService _storageService = storageService;

        [Function("GetStorageFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "storage/{id}")] HttpRequest req, string id)
        {
            StorageVM storageVM = await _storageService.GetStorageByIdAsync(id);

            return new OkObjectResult(storageVM);
        }
    }
}
