using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Shared.View_Models;
using System.Text.Json;

namespace Api.Functions.RAMs
{
    public class AddRAMsFunction(IRamService ramService)
    {
        private readonly IRamService _ramService = ramService;

        [Function("AddRAMsFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "rams/{userId}")] HttpRequest req, string userId)
        {
            if (userId == Environment.GetEnvironmentVariable("Admin_Id"))
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                var rams = JsonSerializer.Deserialize<List<RAMVM>>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                List<string> ramIds = await _ramService.AddRamsAsync(rams);

                return new CreatedResult(string.Empty, ramIds);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }
    }
}
