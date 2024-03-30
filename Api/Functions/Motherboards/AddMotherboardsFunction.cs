using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Shared.View_Models;
using System.Text.Json;

namespace Api.Functions.Motherboards
{
    public class AddMotherboardsFunction(IMotherboardService motherboardService)
    {
        private readonly IMotherboardService _motherboardService = motherboardService;

        [Function("AddMotherboardsFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "motherboards/{userId}")] HttpRequest req, string userId)
        {
            if (userId == Environment.GetEnvironmentVariable("Admin_Id"))
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                var motherboards = JsonSerializer.Deserialize<List<MotherboardVM>>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                List<string> motherboardIds = await _motherboardService.AddMotherboardsAsync(motherboards);

                return new CreatedResult(string.Empty, motherboardIds);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }
    }
}
