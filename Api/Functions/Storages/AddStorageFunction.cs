using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;
using System.Text.Json;
using Shared.View_Models;

namespace Api.Functions.Storage
{
    public class AddStorageFunction(IStorageService storageService)
    {
        private readonly IStorageService _storageService = storageService;

        [Function("AddStorageFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "storage/{userId}")] HttpRequest req, string userId)
        {
            if (userId == Environment.GetEnvironmentVariable("Admin_Id"))
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                var storageVM = JsonSerializer.Deserialize<StorageVM>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                string storageId = await _storageService.AddStorageAsync(storageVM);

                return new CreatedResult(string.Empty, storageId);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }
    }
}
