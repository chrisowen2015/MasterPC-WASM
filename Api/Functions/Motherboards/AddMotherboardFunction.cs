using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;
using System.Text.Json;
using Shared.View_Models;

namespace Api.Functions.Motherboards
{
    public class AddMotherboardFunction(IMotherboardService motherboardService)
    {
        private readonly IMotherboardService _motherboardService = motherboardService;

        [Function("AddMotherboardFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "motherboard/{userId}")] HttpRequest req, string userId)
        {
            if (userId == Environment.GetEnvironmentVariable("Admin_Id"))
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                var motherboardVM = JsonSerializer.Deserialize<MotherboardVM>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                string motherboardId = await _motherboardService.AddMotherboardAsync(motherboardVM);

                return new CreatedResult(string.Empty, motherboardId);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }
    }
}
