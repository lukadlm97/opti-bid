using Microsoft.AspNetCore.Mvc;
using OptiBid.API.Utilities;
using OptiBid.Microservices.Contracts.Domain.Output;
using OptiBid.Microservices.Contracts.Services;

namespace OptiBid.API.Controllers
{
    /// <summary>
    /// Controller which hold enumerations data related to account service
    /// </summary>
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
        /// <summary>
        /// Returns all countries supported at system.
        /// </summary>
        /// 
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for fetching currently available countries
        ///
        ///     GET /countries
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that resources is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<EnumItem>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("countries")]
        public async Task<ActionResult<IEnumerable<EnumItem>?>> GetCountries(CancellationToken cancellationToken=default)
        {
            return await _accountDashboardService.GetCountries(default)
                .ToCollectionActionResult();
        }
        /// <summary>
        /// Returns all professions supported at system.
        /// </summary>
        /// 
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for fetching currently available professions
        ///
        ///     GET /professions
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that resources is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<EnumItem>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("professions")]
        public async Task<ActionResult<IEnumerable<EnumItem>?>> GetProfessions()
        {
            return await _accountDashboardService.GetProfessions(default)
                .ToCollectionActionResult();
        }
        /// <summary>
        /// Returns all contact types supported at system.
        /// </summary>
        /// 
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for fetching currently available contact types
        ///
        ///     GET /contactTypes
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that resources is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<EnumItem>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("contactTypes")]
        public async Task<ActionResult<IEnumerable<EnumItem>?>> GetContentTypes()
        {
            return await _accountDashboardService.GetContactTypes(default)
                .ToCollectionActionResult();
        }
        /// <summary>
        /// Returns all user roles supported at system.
        /// </summary>
        /// 
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for fetching currently available user roles 
        ///
        ///     GET /userRoles
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that resources is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<EnumItem>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("userRoles")]
        public async Task<ActionResult<IEnumerable<EnumItem>?>> GetUserRoles()
        {
            return await _accountDashboardService.GetUserRoles(default)
                .ToCollectionActionResult();
        }
    }
}
