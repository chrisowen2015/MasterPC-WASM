using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;
using System.Text.Json;
using Shared.View_Models;

namespace Api.Functions.PSUs
{
    public class AddPSUFunction(IPSUService psuService)
    {
        private readonly IPSUService _psuService = psuService;

        [Function("AddPSUFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "psu/{userId}")] HttpRequest req, string userId)
        {
            if (userId == Environment.GetEnvironmentVariable("Admin_Id"))
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                var psuVM = JsonSerializer.Deserialize<PSUVM>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                string psuId = await _psuService.AddPSUAsync(psuVM);

                return new CreatedResult(string.Empty, psuId);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }
    }
}
