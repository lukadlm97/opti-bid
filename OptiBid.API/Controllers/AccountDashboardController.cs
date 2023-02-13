using Microsoft.AspNetCore.Mvc;
using OptiBid.Microservices.Contracts.Services;
using OptiBid.Microservices.Services.Services;

namespace OptiBid.API.Controllers
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class AccountDashboardController:ControllerBase
    {
        private readonly IAccountDashboardService _accountDashboardService;

        public AccountDashboardController(IAccountDashboardService accountDashboardService)
        {
            _accountDashboardService = accountDashboardService;
        }

        [HttpGet("countries")]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _accountDashboardService.GetCountries(default);
            return Ok(countries);
        }
    }
}
