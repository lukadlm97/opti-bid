using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetCountries(CancellationToken cancellationToken=default)
        {
            var operationResult = await _accountDashboardService.GetCountries(default);

            return operationResult.Status switch
            {
                OperationResultStatus.Success => Ok(operationResult.Collection),
                OperationResultStatus.NotFound => NotFound(operationResult.ErrorMessage),
                OperationResultStatus.BadRequest or _=> BadRequest(operationResult.ErrorMessage)
            };
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
        public async Task<IActionResult> GetProfessions()
        {
            var operationResult = await _accountDashboardService.GetProfessions(default);

            return operationResult.Status switch
            {
                OperationResultStatus.Success => Ok(operationResult.Collection),
                OperationResultStatus.NotFound => NotFound(operationResult.ErrorMessage),
                OperationResultStatus.BadRequest or _ => BadRequest(operationResult.ErrorMessage)
            };
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
        public async Task<IActionResult> GetContentTypes()
        {
            var operationResult = await _accountDashboardService.GetContactTypes(default);

            return operationResult.Status switch
            {
                OperationResultStatus.Success => Ok(operationResult.Collection),
                OperationResultStatus.NotFound => NotFound(operationResult.ErrorMessage),
                OperationResultStatus.BadRequest or _ => BadRequest(operationResult.ErrorMessage)
            };
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
        public async Task<IActionResult> GetUserRoles()
        {
            var operationResult = await _accountDashboardService.GetUserRoles(default);

            return operationResult.Status switch
            {
                OperationResultStatus.Success => Ok(operationResult.Collection),
                OperationResultStatus.NotFound => NotFound(operationResult.ErrorMessage),
                OperationResultStatus.BadRequest or _ => BadRequest(operationResult.ErrorMessage)
            };
        }
    }
}
