using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;
using System.Text.Json;
using Shared.View_Models;

namespace Api.Functions.RAMs
{
    public class AddRAMFunction(IRamService ramService)
    {
        private readonly IRamService _ramService = ramService;

        [Function("AddRAMFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "ram/{userId}")] HttpRequest req, string userId)
        {
            if (userId == Environment.GetEnvironmentVariable("Admin_Id"))
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                var ramVM = JsonSerializer.Deserialize<RAMVM>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                string ramId = await _ramService.AddRamAsync(ramVM);

                return new CreatedResult(string.Empty, ramId);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }
    }
}
