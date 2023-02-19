using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OptiBid.API.Utilities;
using OptiBid.Microservices.Contracts.Services;
using System.Security.Claims;
using OptiBid.Microservices.Contracts.Domain.Input;

namespace OptiBid.API.Controllers
{
    /// <summary>
    /// Controller which hold enumerations data related to account service
    /// </summary>
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Allow user to sign in to system.
        /// </summary>
        ///
        /// <param name="userRequest"> represents sign-in user model (username and password)</param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for fetching currently available countries
        ///
        ///     POST /signIn
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that user is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(Microservices.Contracts.Domain.Output.User.SignInResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("info")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<Microservices.Contracts.Domain.Output.User.UserResult>> Get( CancellationToken cancellationToken = default)
        {
            if (HttpContext.User.HasClaim(claim => claim.Type == ClaimTypes.Name))
            {
                var userName = HttpContext.User.FindFirst(ClaimTypes.Name)!.Value;
                return await _accountService.GetDetails(userName, cancellationToken)
                    .ToActionResult();
            }

            return Unauthorized();
        }

        /// <summary>
        /// Allow user to sign in to system.
        /// </summary>
        ///
        /// <param name="userRequest"> represents sign-in user model (username and password)</param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for fetching currently available countries
        ///
        ///     POST /signIn
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that user is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(Microservices.Contracts.Domain.Output.User.SignInResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "User")]
        [HttpGet("{id}/details")]
        public async Task<ActionResult<Microservices.Contracts.Domain.Output.User.UserResult>> GetById(int id,CancellationToken cancellationToken = default)
        {
            if (HttpContext.User.HasClaim(claim => claim.Type == ClaimTypes.Name))
            {
                return await _accountService.GetDetails(id, cancellationToken)
                    .ToActionResult();
            }

            return Unauthorized();
        }

        /// <summary>
        /// Allow user to sign in to system.
        /// </summary>
        ///
        /// <param name="userRequest"> represents sign-in user model (username and password)</param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for fetching currently available countries
        ///
        ///     POST /signIn
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that user is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(Microservices.Contracts.Domain.Output.User.SignInResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "User")]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable< Microservices.Contracts.Domain.Output.User.SingleUserResult>>> GetAll(CancellationToken cancellationToken = default)
        {
            if (HttpContext.User.HasClaim(claim => claim.Type == ClaimTypes.Name))
            {
                return await _accountService.Get(cancellationToken)
                    .ToCollectionActionResult();
            }

            return Unauthorized();
        }



        /// <summary>
        /// Allow user to sign in to system.
        /// </summary>
        ///
        /// <param name="userRequest"> represents sign-in user model (username and password)</param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request for fetching currently available countries
        ///
        ///     POST /signIn
        ///     
        ///     
        /// </remarks>
        /// <response code="400">Indicates that request parameters are bad</response>
        /// <response code="404">Indicates that user is not found</response>
        ///
        /// <returns></returns>
        [ProducesResponseType(typeof(Microservices.Contracts.Domain.Output.User.SignInResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "User")]
        [HttpPut()]
        public async Task<ActionResult<bool>> Update([FromBody] UserRequest userRequest,CancellationToken cancellationToken = default)
        {
            if (HttpContext.User.HasClaim(claim => claim.Type == ClaimTypes.Name))
            {
                var userName = HttpContext.User.FindFirst(ClaimTypes.Name)!.Value;
                return await _accountService.UpdateProfile(userName,userRequest,cancellationToken).ToActionResult();
            }

            return Unauthorized();
        }
    }
}
