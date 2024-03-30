using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Api.Services;
using System.Text.Json;
using Shared.View_Models;

namespace Api.Functions.Cases
{
    public class AddCasesFunction(ICaseService caseService)
    {
        private readonly ICaseService _caseService = caseService;

        [Function("AddCasesFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "cases/{userId}")] HttpRequest req, string userId)
        {
            if (userId == Environment.GetEnvironmentVariable("Admin_Id"))
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                var caseVMList = JsonSerializer.Deserialize<List<CaseVM>>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                List<string> caseIds = await _caseService.AddCasesAsync(caseVMList);

                return new CreatedResult(string.Empty, caseIds);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }
    }
}
