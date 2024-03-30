using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace Api.Functions.Storage
{
    public class GetStoragesFunction(IStorageService storageService)
    {
        private readonly IStorageService _storageService = storageService;

        [Function("GetStoragesFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "storages")] HttpRequest req)
        {
            var storages = await _storageService.GetStoragesAsync();

            return new OkObjectResult(storages);
        }
    }
}
