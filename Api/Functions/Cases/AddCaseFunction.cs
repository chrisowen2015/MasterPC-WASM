using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;
using System.Text.Json;
using Shared.View_Models;

namespace Api.Functions.Cases
{
    public class AddCaseFunction(ICaseService caseService)
    {
        private readonly ICaseService _caseService = caseService;

        [Function("AddCaseFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "case/{userId}")] HttpRequest req, string userId)
        {
            if(userId == Environment.GetEnvironmentVariable("Admin_Id"))
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                var caseVM = JsonSerializer.Deserialize<CaseVM>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                string caseId = await _caseService.AddCaseAsync(caseVM);

                return new CreatedResult(string.Empty, caseId);
            } else
            {
                return new UnauthorizedResult();
            }            
        }
    }
}
