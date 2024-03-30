using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Shared.View_Models;
using System.Text.Json;

namespace Api.Functions.Storage
{
    public class AddStoragesFunction(IStorageService storageService)
    {
        private readonly IStorageService _storageService = storageService;

        [Function("AddStoragesFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "storages/{userId}")] HttpRequest req, string userId)
        {
            if (userId == Environment.GetEnvironmentVariable("Admin_Id"))
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                var storages = JsonSerializer.Deserialize<List<StorageVM>>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                List<string> storageIds = await _storageService.AddStoragesAsync(storages);

                return new CreatedResult(string.Empty, storageIds);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }
    }
}
