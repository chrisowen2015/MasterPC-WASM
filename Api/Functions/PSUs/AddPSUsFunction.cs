using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Shared.View_Models;
using System.Text.Json;

namespace Api.Functions.PSUs
{
    public class AddPSUsFunction(IPSUService psuService)
    {
        private readonly IPSUService _psuService = psuService;

        [Function("AddPSUsFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "psus/{userId}")] HttpRequest req, string userId)
        {
            if (userId == Environment.GetEnvironmentVariable("Admin_Id"))
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                var psus = JsonSerializer.Deserialize<List<PSUVM>>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                List<string> psuIds = await _psuService.AddPSUsAsync(psus);

                return new CreatedResult(string.Empty, psuIds);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }
    }
}
